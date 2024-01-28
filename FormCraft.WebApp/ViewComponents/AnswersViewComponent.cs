using FormCraft.Business.Contracts.Responses.Answer;
using FormCraft.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.ViewComponents;

public class AnswersViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(QuestionTypeEnum questionTypeId, params AnswerResponse[] answers)
    {
        return questionTypeId switch
        {
            QuestionTypeEnum.Open => View("TextBoxAnswer", answers),
            QuestionTypeEnum.RadioButton => View("RadioButtonAnswer", answers),
            QuestionTypeEnum.Checkbox => View("CheckboxAnswer", answers),
            QuestionTypeEnum.Dropdown => View("DropDownAnswer", answers),
            _ => View(),
        };

    }
}
