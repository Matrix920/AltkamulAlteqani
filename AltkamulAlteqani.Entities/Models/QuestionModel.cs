using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AltkamulAlteqani.Entities.Models
{
    public class QuestionModel
    {
        public int QuestionId { get; set; }

        [Display(Name = "Created Date")]
        public string CreatedDate { set; get; }

        [Display(Name = "Created Time")]
        public string CreatedTime { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }


        [Display(Name = "Is Answered")]
        public string IsAnswered { set; get; }

        [Display(Name = "View Count")]
        public int ViewCount { set; get; }

        [Display(Name = "Answer Count")]
        public int AnswerCount { get; set; }

        public string Score { set; get; }
        [Display(Name = "Last Activity Date")]
        public string LastActivityDate { set; get; }
    }
}
