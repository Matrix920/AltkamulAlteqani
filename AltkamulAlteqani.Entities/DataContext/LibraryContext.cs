using AltkamulAlteqani.Entities.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace AltkamulAlteqani.Entities.DataContext
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
            : base(ConfigurationManager.ConnectionStrings["LibraryContext"].ConnectionString)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<BookType> BookTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithRequired(b => b.Author)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<BookType>()
                .HasMany(t => t.Books)
                .WithRequired(b => b.BookType)
                .HasForeignKey(b => b.BookTypeId);
        }
    }
}
