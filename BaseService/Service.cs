using BaseRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities;

namespace BaseService
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class, ITrackable
    {
        private readonly IRepositoryAsync<TEntity> _repository;

        protected Service(IRepositoryAsync<TEntity> repository)
        {
            _repository = repository;
        }

        public string CurrentSessionUserId { get; set; }

        public virtual void ApplyChanges(TEntity entity)
        {
            _repository.ApplyChanges(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _repository.Delete(entity);
        }

        public virtual Task<bool> DeleteAsync(params object[] keyValues)
        {
            return _repository.DeleteAsync(keyValues);
        }

        public virtual Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return _repository.DeleteAsync(cancellationToken, keyValues);
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return _repository.Find(keyValues);
        }

        public virtual Task<TEntity> FindAsync(params object[] keyValues)
        {
            return _repository.FindAsync(keyValues);
        }

        public virtual Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return _repository.FindAsync(cancellationToken, keyValues);
        }

        public virtual void Insert(TEntity entity)
        {
            _repository.Insert(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            _repository.InsertRange(entities);
        }
        public virtual IQueryable<TEntity> Queryable()
        {
            return _repository.Queryable();
        }

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return _repository.SelectQuery(query, parameters);
        }

        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
        }
    }
}
