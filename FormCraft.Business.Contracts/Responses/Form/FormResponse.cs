using FormCraft.Business.Contracts.Common;
using FormCraft.Entities;

namespace FormCraft.Business.Contracts.Responses.Form
{
    public record FormResponse(
        string Id,
        AppUser Creator,
        FormType FormType,
        Status Status) : IRequest;
}
