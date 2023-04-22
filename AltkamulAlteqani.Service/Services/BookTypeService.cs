using AltkamulAlteqani.Entities.Core;
using BaseRepository.Repositories;
using BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltkamulAlteqani.Service.Services
{
    public interface IBookTypeService : IService<BookType>
    {
    }

    public class BookTypeService : Service<BookType>, IBookTypeService
    {
        private readonly IRepositoryAsync<BookType> _repository;
        protected BookTypeService(IRepositoryAsync<BookType> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
