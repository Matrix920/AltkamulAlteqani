using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltkamulAlteqani.Entities.ApiModels
{
    public class Owner
    {
        public int account_id { set; get; }
        public int reputation { get; set; }
        public int user_id { get; set; }
        public string user_type { get; set; }
        public string profile_image { get; set; }
        public string display_name { get; set; }
        public string link { get; set; }
    }
}
