using AltkamulAlteqani.Entities.Core;
using AltkamulAlteqani.Entities.Models;
using BaseRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AltkamulAlteqani.Web.Api
{
    public class BookTypesController : ApiController
    {
        private readonly IRepositoryAsync<BookType> _repository;
        public BookTypesController(IRepositoryAsync<BookType> repository)
        {
            _repository = repository;
        }

        public IEnumerable<BookTypeModel> Get()
        {
            return _repository.Queryable()
                .Select(t => new BookTypeModel
                {
                    Id = t.BookTypeId,
                    Name = t.BookTypeName
                });
        }
    }
}
