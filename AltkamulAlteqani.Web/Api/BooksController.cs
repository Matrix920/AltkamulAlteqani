using AltkamulAlteqani.Entities.Models;
using AltkamulAlteqani.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AltkamulAlteqani.Web.Api
{
    public class BooksController : ApiController
    {
        private readonly IBookRepository _repository;

        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<BookModel> Get(int typeId)
        {
            int bookTypeId = typeId;
            return _repository.GetByTypeId(bookTypeId)
                .Where(b => b.BookTypeId == typeId)
                .Select(b => new BookModel
                {
                    Title = b.Title,
                    BookId = b.BookId,
                    BookTypeId = b.BookTypeId
                });
        }
    }
}
