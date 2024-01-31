using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Requests.Question;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        // GET: QuestionController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            return View(await _questionService.GetById(id));

        }

        // POST: QuestionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateQuestionRequest request)
        {
            try
            {
                return Json(await _questionService.Create(request));
            }
            catch
            {
                return View();
            }
        }

        // POST: QuestionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // POST: QuestionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
}
