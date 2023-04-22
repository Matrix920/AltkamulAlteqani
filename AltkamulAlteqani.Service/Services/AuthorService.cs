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
    public interface IAuthorService : IService<Author>
    {

    }
    public class AuthorService : Service<Author>, IAuthorService
    {
        private readonly IRepositoryAsync<Author> _repository;
        protected AuthorService(IRepositoryAsync<Author> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
