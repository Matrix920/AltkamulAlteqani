using BaseRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities;

namespace BaseRepository.UnitOfWork
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, ITrackable;
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
        Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters);
    }
}
