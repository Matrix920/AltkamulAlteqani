using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities;

namespace BaseRepository.Repositories
{
    public interface IRepositoryAsync<TEntity> : IRepository<TEntity> where TEntity : class, ITrackable
    {
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<IEnumerable<TEntity>> SelectQueryAsync(string query, params object[] parameters);
        Task<IEnumerable<TEntity>> SelectQueryAsync(string query, CancellationToken cancellationToken, params object[] parameters);

    }
}
