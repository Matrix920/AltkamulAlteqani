using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltkamulAlteqani.Entities.ApiModels
{
    public class Question
    {
        public List<string> tags { set; get; }

        public Owner owner { set; get; }

        public bool is_answered { set; get; }
        public int view_count { set; get; }
        public int answer_count { get; set; }
        public string score { set; get; }
        public long last_activity_date { set; get; }
        public long creation_date { set; get; }
        public int question_id { set; get; }
        public string content_license { set; get; }
        public string link { set; get; }
        public string title { set; get; }
    }
}
