using FormCraft.Entities;

namespace FormCraft.Tests.Commons.Builders.Entities;

public class FormBuilder
{
    private readonly Form _form = new();

    public static FormBuilder AForm => new();

    public FormBuilder WithLabel(string label)
    {
        _form.Label = label;
        return this;
    }

    public FormBuilder CreatedBy(AppUser creator)
    {
        _form.Creator = creator;
        return this;
    }

    public FormBuilder WithFormTypeId(FormTypeEnum formTypeId)
    {
        _form.FormTypeId = formTypeId;
        return this;
    }

    public FormBuilder WithFormType(FormType formType)
    {
        _form.FormType = formType;
        return this;
    }

    public FormBuilder WithStatusId(StatusEnum status)
    {
        _form.StatusId = status;
        return this;
    }

    public FormBuilder WithStatus(Status status)
    {
        _form.Status = status;
        return this;
    }

    public FormBuilder WithQuestions(List<Question> questions)
    {
        _form.Questions = questions;
        return this;
    }

    public FormBuilder CreatedAt(DateTime createdAt)
    {
        _form.CreatedAt = createdAt;
        return this;
    }

    public FormBuilder UpdatedAt(DateTime updatedAt)
    {
        _form.UpdatedAt = updatedAt;
        return this;
    }

    public Form Build()
    {
        return _form;
    }
}
