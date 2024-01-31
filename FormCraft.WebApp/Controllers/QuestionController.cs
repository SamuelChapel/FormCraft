using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Requests.Question;
using FormCraft.WebApp.ViewComponents;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.Controllers;

    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<ActionResult> Details(string id)
        {
            return View(await _questionService.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateQuestionRequest request)
        {
        var question = await _questionService.Create(request);

        return ViewComponent("Question", new { question, QuestionViewEnum.Create });
        }

        [HttpPost]
        public async Task<ActionResult> Update(UpdateQuestionRequest request)
        {
            try
            {
                return Json(await _questionService.Update(request));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string Id)
        {
            try
            {

                await _questionService.Delete(new DeleteQuestionRequest(Id));
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
