using FormCraft.Business.Contracts.Common;
using FormCraft.Entities;

namespace FormCraft.Business.Contracts.Requests.Form
{
    public record SearchFormRequest(
        string? Label = null,
        StatusEnum? StatusId = null,
        FormTypeEnum? FormTypeId = null,
        int? Order = null) : IRequest;
}