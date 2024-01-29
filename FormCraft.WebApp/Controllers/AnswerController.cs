using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Requests.Answer;
using FormCraft.Business.Contracts.Responses.Answer;
using Microsoft.AspNetCore.Mvc;

namespace FormCraft.WebApp.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IAnswerBusiness _answerBusiness;

        public AnswerController(IAnswerBusiness answerBusiness)
        {
            _answerBusiness = answerBusiness;
        }

        // GET: AnswerController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            //return View(await _answerBusiness.GetById(id));

            return View("Test", new AnswerResponse("adada", "La reponse d biboubar dzojjjoaoaoa de maatyj", "addadad"));
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
        public async Task<ActionResult> Delete(string Id)
        {
            try
            {

                await _answerBusiness.Delete(new DeleteAnswerRequest(Id));
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
