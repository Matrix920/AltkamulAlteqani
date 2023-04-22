using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltkamulAlteqani.Entities.Models
{
    public class BookModel
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string BookType { get; set; }

        public string Author { get; set; }

        public int BookTypeId { get; set; }

        public int AuthorId { get; set; }
    }
}
