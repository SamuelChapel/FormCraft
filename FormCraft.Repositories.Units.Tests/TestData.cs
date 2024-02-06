using FormCraft.Entities;

namespace FormCraft.Repositories.Units.Tests;

public static class TestData
{
    public static IEnumerable<Form> FormTestData()
    {
        yield return new Form
        {
            Id = "1",
            Label = "Form 1",
            FormType = new FormType { Id = FormTypeEnum.Survey, Label = "Survey" },
            Status = new Status { Id = StatusEnum.InProgress, Label = "InProgress" },
            Creator = new AppUser { Id = "1", UserName = "user1" },
            CreatedAt = DateTime.UtcNow
        };

        yield return new Form
        {
            Id = "2",
            Label = "Form 2",
            FormType = new FormType { Id = FormTypeEnum.Evaluation, Label = "Evalution" },
            Status = new Status { Id = StatusEnum.Validated, Label = "Validated" },
            Creator = new AppUser { Id = "2", UserName = "user2" },
            CreatedAt = DateTime.UtcNow
        };

        yield return new Form
        {
            Id = "3",
            Label = "Form 3",
            FormType = new FormType { Id = FormTypeEnum.Comment, Label = "Comment" },
            Status = new Status { Id = StatusEnum.Closed, Label = "Closed" },
            Creator = new AppUser { Id = "3", UserName = "user3" },
            CreatedAt = DateTime.UtcNow
        };
    }
}
