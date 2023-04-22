using Repository.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltkamulAlteqani.Entities.Core
{
    [Table("BookTypes")]
    public class BookType : Entity
    {
        [Key]
        public int BookTypeId { get; set; }

        [Required]
        [StringLength(20)]
        public string BookTypeName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
