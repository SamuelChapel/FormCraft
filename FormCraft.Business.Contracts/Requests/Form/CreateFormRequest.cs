using FormCraft.Business.Contracts.Common;
using FormCraft.Entities;

namespace FormCraft.Business.Contracts.Requests.Form
{
    public record CreateFormRequest(
        string Label,
        FormTypeEnum FormTypeId) : IRequest;
}