using Repository.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltkamulAlteqani.Entities.Core
{
    public class Book : Entity
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public decimal Price { get; set; }

        public int BookTypeId { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual BookType BookType { get; set; }
    }
}
