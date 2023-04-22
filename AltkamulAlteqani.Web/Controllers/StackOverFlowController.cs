using AltkamulAlteqani.Entities.Models;
using AltkamulAlteqani.Invokers.StackOverFlowInvokers;
using AltkamulAlteqani.Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AltkamulAlteqani.Web.Controllers
{
    public class StackOverFlowController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> QuestionList()
        {
            try
            {
                int pageSize = 50;
                var questions = await QuestionInvoker.GetCompressedQuesions(pageSize: pageSize);

                var itemList = questions.Select(x => new QuestionModel
                {
                    QuestionId = x.question_id,
                    Title = x.title,
                    Link = x.link,
                    CreatedDate = Helper.GetDate(x.creation_date).ToShortDateString(),
                    CreatedTime = Helper.GetDate(x.creation_date).ToShortTimeString()
                });

                return Json(new { @data = itemList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ActionResult> QuestionDetails(int questionId)
        {
            var questions = await QuestionInvoker.GetCompressedQuestion(questionId);

            if (questions != null)
            {
                var question = questions
                    .Select(x => new QuestionModel
                    {
                        QuestionId = x.question_id,
                        Title = x.title,
                        Link = x.link,
                        CreatedDate = Helper.GetDate(x.creation_date).ToShortDateString(),
                        CreatedTime = Helper.GetDate(x.creation_date).ToShortTimeString(),
                        AnswerCount = x.answer_count,
                        IsAnswered = x.is_answered ? "Yes" : "No",
                        LastActivityDate = Helper.GetDate(x.last_activity_date).ToShortTimeString(),
                        Score = x.score,
                        ViewCount = x.view_count
                    })
                    .FirstOrDefault();

                return View(question);
            }

            return null;
        }

    }
}