using FormCraft.Business.Contracts.Common;
using FormCraft.Entities;

namespace FormCraft.Business.Contracts.Requests.Form
{
    public record UpdateFormRequest(
        string Id,
        string? Label = null,
        StatusEnum? StatusId = null,
        FormTypeEnum? FormTypeId = null) : IRequest;
}