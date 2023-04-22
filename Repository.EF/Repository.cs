using BaseRepository.Repositories;
using BaseRepository.UnitOfWork;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities;
using TrackableEntities.EF6;

namespace Repository.EF
{
    public class Repository<TEntity> : IRepositoryAsync<TEntity> where TEntity : class, ITrackable
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> Set;
        protected readonly IUnitOfWorkAsync UnitOfWork;

        public Repository(DbContext context, IUnitOfWorkAsync unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Context = context;
            Set = context.Set<TEntity>();
        }
        public void ApplyChanges(TEntity entity)
        {
            Context.ApplyChanges(entity);
        }

        public void Delete(params object[] keyValues)
        {
            var entity = Set.Find(keyValues);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            entity.TrackingState = TrackingState.Deleted;
            Context.ApplyChanges(entity);
        }

        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = Set;
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query;
        }

        internal async Task<IEnumerable<TEntity>> SelectAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            return await Select(filter, orderBy, includes, page, pageSize).ToListAsync();
        }
        public virtual async Task<bool> DeleteAsync(params object[] keyValues)
        {
            if (await DeleteAsync(CancellationToken.None, keyValues))
                return true;
            return false;
        }

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues);

            if (entity == null)
            {
                return false;
            }

            entity.TrackingState = TrackingState.Deleted;
            Context.ApplyChanges(entity);
            return true;
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return Set.Find(keyValues);
        }

        public virtual async Task<TEntity> FindAsync(params object[] keyValues) => await Set.FindAsync(keyValues);

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues) => await Set.FindAsync(cancellationToken, keyValues);

        public virtual void Insert(TEntity entity, bool traverseGraph = true)
        {
            entity.TrackingState = TrackingState.Added;

            if (traverseGraph)
                Context.ApplyChanges(entity);
            else
                Context.Entry(entity).State = EntityState.Added;
        }

        public virtual void InsertGraphRange(IEnumerable<TEntity> entities) => InsertRange(entities);

        public void InsertOrUpdateGraph(TEntity entity)
        {
            Context.ApplyChanges(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entites, bool traverseGraph = true)
        {
            foreach (var entity in entites)
            {
                Insert(entity, traverseGraph);
            }
        }

        public IQueryable<TEntity> Queryable() => Set;

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return Set.SqlQuery(query, parameters).AsQueryable();
        }

        public virtual async Task<IEnumerable<TEntity>> SelectQueryAsync(string query, params object[] parameters)
        {
            return await Set.SqlQuery(query, parameters).ToArrayAsync();
        }

        public virtual void Update(TEntity entity, bool traverseGraph = true)
        {
            entity.TrackingState = TrackingState.Modified;

            if (traverseGraph)
                Context.ApplyChanges(entity);
            else
                Context.Entry(entity).State = EntityState.Modified;
        }

        IRepository<T> IRepository<TEntity>.GetRepository<T>() => UnitOfWork.Repository<T>();

        public virtual async Task<IEnumerable<TEntity>> SelectQueryAsync(string query, CancellationToken cancellationToken, params object[] parameters)
        {
            return await Set.SqlQuery(query, parameters).ToArrayAsync(cancellationToken);
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return Set.AsNoTracking().Any(predicate);
        }
    }
}
