using FormCraft.Business.Contracts.Common;

namespace FormCraft.Business.Contracts.Requests.Form
{
    public record UpdateFormRequest(
        string Id,
        string? Label = null,
        int? StatusId = null,
        int? FormTypeId = null) : IRequest;
}