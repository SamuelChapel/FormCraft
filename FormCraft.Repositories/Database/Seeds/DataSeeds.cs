using Bogus;
using FormCraft.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormCraft.Repositories.Database.Seeds;

public static class DataSeeds
{
    public static ModelBuilder AddSeeds(this ModelBuilder builder)
    {
        var formTypes = FormTypesSeeds();
        var questionTypes = QuestionTypesSeeds();
        var formStatus = FormStatusSeeds();

        var appUserIds = Enumerable.Range(0, 10).Select(i =>  Guid.NewGuid().ToString()).Distinct().ToList();

        var forms = FormsSeeds(appUserIds, formTypes, questionTypes, formStatus, 50);
        var questions = forms.SelectMany(f => f.Questions).ToList();
        var answers = questions.SelectMany(q => q.Answers).ToList();

        var appUsers = AppUserSeeds(appUserIds, forms.Where(f => f.StatusId != 1).ToList());
        var appUserAnswers = appUsers.SelectMany(a => a.AppUserAnswers).ToList();

        // Remove collections fields for ef core
        forms.ForEach(f => f.Questions = []);
        questions.ForEach(q => q.Answers = []);
        appUsers.ForEach(a => a.AppUserAnswers = []);

        builder.Entity<QuestionType>().HasData(questionTypes);
        builder.Entity<FormType>().HasData(formTypes);
        builder.Entity<Status>().HasData(formStatus);

        builder.Entity<Form>().HasData(forms);
        builder.Entity<Question>().HasData(questions);
        builder.Entity<Answer>().HasData(answers);

        builder.Entity<AppUser>().HasData(appUsers);
        builder.Entity<AppUserAnswer>().HasData(appUserAnswers);

        return builder;
    }

    private static List<FormType> FormTypesSeeds()
    {
        return
        [
            new() { Id = 1, Label = "Survey" },
            new() { Id = 2, Label = "Comment" },
            new() { Id = 3, Label = "Evaluation" }
        ];
    }

    private static List<QuestionType> QuestionTypesSeeds()
    {
        return
        [
            new() { Id = QuestionTypeEnum.Open, Label = "Open" },
            new() { Id = QuestionTypeEnum.RadioButton, Label = "RadioButton" },
            new() { Id = QuestionTypeEnum.Checkbox, Label = "Checkbox" },
            new() { Id = QuestionTypeEnum.Dropdown, Label = "Dropdown" },
        ];
    }

    private static List<FormType> FormQuestionTypesSeeds()
    {
        return
        [
            new() { Id = 1, Label = "Survey" },
            new() { Id = 2, Label = "Comment" },
            new() { Id = 3, Label = "Evaluation" }
        ];
    }

    private static List<Status> FormStatusSeeds()
    {
        return
        [
            new() { Id = 1, Label = "InProgress" },
            new() { Id = 2, Label = "Validated" },
            new() { Id = 3, Label = "Closed" }
        ];
    }

    private static List<Form> FormsSeeds(List<string> appUserIds, List<FormType> formTypes, List<QuestionType> questionTypes, List<Status> formStatus, int count = 200)
    {
        return Enumerable.Range(0, count).Select(i => new Faker<Form>()
            .RuleFor(q => q.Id, Guid.NewGuid().ToString())
            .RuleFor(f => f.Label, f => f.Random.Words(4))
            .RuleFor(f => f.CreatorId, f => f.Random.CollectionItem(appUserIds))
            .RuleFor(f => f.FormTypeId, f => f.Random.CollectionItem(formTypes).Id)
            .RuleFor(f => f.StatusId, f => f.Random.CollectionItem(formStatus).Id)
            .RuleFor(a => a.CreatedAt, f => f.Date.Past(2))
            .RuleFor(a => a.UpdatedAt, (f, current) => f.Date.Between(current.CreatedAt, DateTime.UtcNow))
            .RuleFor(a => a.Questions, (f, current) =>
                Enumerable.Range(1, 20)
                          .Select(i => QuestionsSeed(current.Id, i, questionTypes))
                          .ToList())
            .Generate()).ToList();
    }

    private static Question QuestionsSeed(string formId, int number, List<QuestionType> questionTypes)
    {
        return new Faker<Question>()
            .RuleFor(q => q.Id, Guid.NewGuid().ToString())
            .RuleFor(q => q.Label, f => f.Random.Words(f.Random.Number(3, 8)) + " ?")
            .RuleFor(q => q.QuestionTypeId, f => f.Random.CollectionItem(questionTypes).Id)
            .RuleFor(q => q.FormId, formId)
            .RuleFor(q => q.Number, number)
            .RuleFor(a => a.CreatedAt, f => f.Date.Past(2))
            .RuleFor(a => a.UpdatedAt, (f, current) => f.Date.Between(current.CreatedAt, DateTime.UtcNow))
            .RuleFor(a => a.Answers, (f, current) =>
                Enumerable.Range(0, current.QuestionTypeId == QuestionTypeEnum.Open ? 1 : f.Random.Number(2, 5))
                          .Select(i => AnswersSeed(current.Id)).ToList())
            .Generate();
    }

    private static Answer AnswersSeed(string questionId)
    {
        return new Faker<Answer>()
            .RuleFor(a => a.Id, Guid.NewGuid().ToString())
            .RuleFor(a => a.Label, f => f.Random.Word())
            .RuleFor(a => a.QuestionId, questionId)
            .RuleFor(a => a.CreatedAt, f => f.Date.Past())
            .RuleFor(a => a.UpdatedAt, (f, current) => f.Date.Between(current.CreatedAt, DateTime.UtcNow))
            .Generate();
    }

    private static List<AppUser> AppUserSeeds(List<string> appUserIds, List<Form> formsValidated)
    {
        return Enumerable.Range(0, appUserIds.Count).Select(i => new Faker<AppUser>()
            .RuleFor(a => a.Id, appUserIds[i])
            .RuleFor(a => a.UserName, f => f.Person.FullName)
            .RuleFor(a => a.Address, f => f.Person.Address.Street)
            .RuleFor(a => a.Email, f => f.Person.Email)
            .RuleFor(a => a.PhoneNumber, f => f.Person.Phone)
            .RuleFor(a => a.CreatedAt, f => f.Date.Past(2))
            .RuleFor(a => a.UpdatedAt, (f, current) => f.Date.Between(current.CreatedAt, DateTime.UtcNow))
            .RuleFor(a => a.AppUserAnswers, (f, current) =>
            {
                var formsNotOwned = formsValidated.Where(f => f.CreatorId != current.Id).ToList();
                var formsToAnswer = formsNotOwned.OrderBy(_ => Random.Shared.Next()).Take(Random.Shared.Next(0, formsNotOwned.Count /2)).ToList();

                var answers = formsToAnswer.SelectMany(form => form.Questions.Select(q => f.Random.CollectionItem(q.Answers).Id)).ToList();

                return answers.Select(a =>
                {
                    var date = f.Date.Between(current.CreatedAt, DateTime.UtcNow);

                    return new AppUserAnswer()
                    {
                        AnswerId = a,
                        AppUserId = current.Id,
                        CreatedAt = date,
                        UpdatedAt = date
                    };
                }).ToList();
            })
            .Generate()).ToList();
    }
}
