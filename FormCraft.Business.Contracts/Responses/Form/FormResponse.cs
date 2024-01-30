using FormCraft.Business.Contracts.Common;
using FormCraft.Entities;

namespace FormCraft.Business.Contracts.Responses.Form;

public record FormResponse(
    string Id,
    string CreatorId,
    FormTypeEnum FormTypeId,
    StatusEnum StatusId) : IRequest;
    string Label) : IRequest;
