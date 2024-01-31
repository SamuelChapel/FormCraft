using FormCraft.Business.Contracts.Common;
using FormCraft.Business.Contracts.Responses.Question;
using FormCraft.Entities;

namespace FormCraft.WebApp.ViewModels.FormViewModels
{
    public record FormSearchViewModel(
        FormTypeEnum FormTypeId,
        StatusEnum StatusId,
        List<QuestionResponse> Questions) : IRequest;
}
