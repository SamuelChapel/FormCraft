using FormCraft.Business.Contracts.Responses.Form;
using FormCraft.Business.Contracts.Responses.Question;
using FormCraft.Entities;

namespace FormCraft.Tests.Commons.Builders.Responses;

public class FormWithQuestionsResponseBuilder
{
    private string _id = "123";
    private string _creatorId = "456";
    private FormTypeEnum _formTypeId = FormTypeEnum.Survey;
    private StatusEnum _statusId = StatusEnum.InProgress;
    private string _label = "Sample Form";
    private List<QuestionResponse> _questions = [];

    public static FormWithQuestionsResponseBuilder AFormWithQuestionsResponse => new();

    public FormWithQuestionsResponseBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public FormWithQuestionsResponseBuilder WithCreatorId(string creatorId)
    {
        _creatorId = creatorId;
        return this;
    }

    public FormWithQuestionsResponseBuilder WithFormTypeId(FormTypeEnum formTypeId)
    {
        _formTypeId = formTypeId;
        return this;
    }

    public FormWithQuestionsResponseBuilder WithStatusId(StatusEnum statusId)
    {
        _statusId = statusId;
        return this;
    }

    public FormWithQuestionsResponseBuilder WithLabel(string label)
    {
        _label = label;
        return this;
    }

    public FormWithQuestionsResponseBuilder WithQuestions(List<QuestionResponse> questions)
    {
        _questions = questions;
        return this;
    }

    public FormWithQuestionsResponse Build()
    {
        return new FormWithQuestionsResponse(
            _id,
            _creatorId,
            _formTypeId,
            _statusId,
            _label,
            _questions
        );
    }
}