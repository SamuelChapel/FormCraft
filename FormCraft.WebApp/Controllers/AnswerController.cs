using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Requests.Answer;
using FormCraft.Business.Contracts.Responses.Answer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.Controllers
{
    public class AnswerController : Controller
    {
        private IAnswerBusiness _answerBusiness;

        public AnswerController(IAnswerBusiness answerBusiness)
        {
            this._answerBusiness = answerBusiness;
        }

        // GET: AnswerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AnswerController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            return View(await _answerBusiness.GetById(id));

        }
        
        // POST: AnswerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAnswerRequest request)
        {
            try
            {
                return Json(await _answerBusiness.Create(request));
            }
            catch
            {
                return View();
            }
        }

        // POST: AnswerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateAnswerRequest request)
        {
            try
            { 
                return Json(await _answerBusiness.Update(request));
            }
            catch
            {
                return View();
            }
        }

        // POST: AnswerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(DeleteAnswerRequest request)
        {
            try
            {
                await _answerBusiness.Delete(request);
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
