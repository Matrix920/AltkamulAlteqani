using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrackableEntities;

namespace BaseRepository.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, ITrackable
    {
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);
        void Insert(TEntity entity, bool traverseGraph = true);
        void ApplyChanges(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entites, bool traverseGraph = true);
        void InsertOrUpdateGraph(TEntity entity);
        void InsertGraphRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity, bool traverseGraph = true);
        void Delete(params object[] keyValues);
        void Delete(TEntity entity);
        IQueryable<TEntity> Queryable();
        IRepository<T> GetRepository<T>() where T : class, ITrackable;
        bool Exists(Expression<Func<TEntity, bool>> predicate);
    }
}