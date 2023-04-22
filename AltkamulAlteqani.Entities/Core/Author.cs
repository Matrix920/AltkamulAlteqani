using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.EF;

namespace AltkamulAlteqani.Entities.Core
{
    [Table("Authors")]
    public class Author : Entity
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; }

        public DateTime BirthDate { get; set; }

        public string ShortBio { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
