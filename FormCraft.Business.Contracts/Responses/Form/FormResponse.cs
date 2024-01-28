using FormCraft.Business.Contracts.Common;

namespace FormCraft.Business.Contracts.Responses.Form;

public record FormResponse(
    string Id,
    string CreatorId,
    int FormTypeId,
    int StatusId) : IRequest;
