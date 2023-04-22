using AltkamulAlteqani.Entities.Core;
using BaseRepository.Repositories;
using BaseRepository.UnitOfWork;
using Repository.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace AltkamulAlteqani.Repository.Repositories
{
    public interface IBookRepository : IRepositoryAsync<Book>
    {
        IEnumerable<Book> GetByTypeId(int? bookTypeId);
    }

    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DbContext context, IUnitOfWorkAsync unitOfWork) : base(context, unitOfWork)
        {
        }

        public IEnumerable<Book> GetByTypeId(int? bookTypeId)
        {
            var bookTypeIdParameter = bookTypeId.HasValue ?
                new SqlParameter("@BookTypeId", bookTypeId) :
                new SqlParameter("@BookTypeId", typeof(int));

            var x = SelectQuery("BooksByType @BookTypeId", bookTypeIdParameter).ToList();

            return x;
        }
    }
}
