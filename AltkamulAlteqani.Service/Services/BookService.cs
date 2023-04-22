using AltkamulAlteqani.Entities.Core;
using BaseRepository.Repositories;
using BaseService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltkamulAlteqani.Service.Services
{
    public interface IBookService : IService<Book>
    {
        IQueryable<Book> GetBookList();
    }

    public class BookService : Service<Book>, IBookService
    {
        public BookService(IRepositoryAsync<Book> repository) : base(repository)
        {
        }

        public IQueryable<Book> GetBookList()
        {
            var books = Queryable()
                .Include(b => b.BookType)
                .Include(b => b.Author);

            return books;
        }
    }
}
