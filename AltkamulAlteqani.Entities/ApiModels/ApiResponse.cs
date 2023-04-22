using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltkamulAlteqani.Entities.ApiModels
{
    public class ApiResponse<TEntity> where TEntity : class
    {
        public List<TEntity> items { set; get; }

        public bool has_more { set; get; }
        public int quota_max { set; get; }
        public int quota_remaining { set; get; }
    }
}
