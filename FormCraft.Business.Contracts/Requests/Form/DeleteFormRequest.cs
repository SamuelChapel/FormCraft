using FormCraft.Business.Contracts.Common;

namespace FormCraft.Business.Contracts.Requests.Form
{
    public record DeleteFormRequest(string Id): IRequest;
}