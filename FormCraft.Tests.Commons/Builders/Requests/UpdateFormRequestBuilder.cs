using FormCraft.Business.Contracts.Requests.Form;
using FormCraft.Entities;

namespace FormCraft.Tests.Commons.Builders.Requests;

public class UpdateFormRequestBuilder
{
    private string _id = "123";
    private string? _label;
    private StatusEnum? _statusId;
    private FormTypeEnum? _formTypeId;

    public static UpdateFormRequestBuilder AnUpdateFormRequest => new();

    public UpdateFormRequestBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public UpdateFormRequestBuilder WithLabel(string label)
    {
        _label = label;
        return this;
    }

    public UpdateFormRequestBuilder WithStatusId(StatusEnum statusId)
    {
        _statusId = statusId;
        return this;
    }

    public UpdateFormRequestBuilder WithFormTypeId(FormTypeEnum formTypeId)
    {
        _formTypeId = formTypeId;
        return this;
    }

    public UpdateFormRequest Build()
    {
        return new UpdateFormRequest(
            _id,
            _label,
            _statusId,
            _formTypeId
        );
    }
}
