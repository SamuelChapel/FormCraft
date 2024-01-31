using FormCraft.Business.Contracts.Common;
using FormCraft.Business.Contracts.Responses.Question;
using FormCraft.Entities;

namespace FormCraft.WebApp.ViewModels.FormViewModels
{
    public record FormDetailsViewModel(
    string Id,
    string CreatorId,
    FormTypeEnum FormTypeId,
    StatusEnum StatusId,
    string Label,
    List<QuestionResponse> Questions) : IRequest;
}
