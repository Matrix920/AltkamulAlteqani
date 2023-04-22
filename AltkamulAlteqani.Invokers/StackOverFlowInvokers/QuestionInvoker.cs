using AltkamulAlteqani.Entities.ApiModels;
using AltkamulAlteqani.Invokers.Invokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltkamulAlteqani.Invokers.StackOverFlowInvokers
{
    public class QuestionInvoker
    {

        public static async Task<List<Question>> GetCompressedQuesions(int page = 1, int pageSize = 10, string sort = "creation", string order = "desc", string site = "stackoverflow")
        {
            return await BaseInvoker<Question, Question>.Get($"questions?page={page}&pagesize={pageSize}&sort={sort}&order={order}&site={site}");

        }

        public static async Task<List<Question>> GetCompressedQuestion(int questionId)
        {
            return await BaseInvoker<Question, Question>.Get($"questions/{questionId}?site=stackoverflow");
        }
    }
}
