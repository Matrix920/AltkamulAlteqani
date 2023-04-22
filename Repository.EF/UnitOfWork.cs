using BaseRepository.Repositories;
using BaseRepository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrackableEntities;
using CommonServiceLocator;

namespace Repository.EF
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        private readonly DbContext _context;
        protected DbTransaction Transaction;
        protected Dictionary<string, dynamic> Repositories;

        public UnitOfWork(DbContext context)
        {
            _context = context;
            Repositories = new Dictionary<string, dynamic>();
        }

        public int? CommandTimeout
        {
            get => _context.Database.CommandTimeout;
            set => _context.Database.CommandTimeout = value;
        }

        public void BeginTransaction(IsolationLevel isolationlevel = IsolationLevel.Unspecified)
        {
            var objectContext = ((IObjectContextAdapter)_context).ObjectContext;
            if (objectContext.Connection.State != ConnectionState.Open)
            {
                objectContext.Connection.Open();
            }

            Transaction = objectContext.Connection.BeginTransaction(isolationlevel);
        }

        public virtual bool Commit()
        {
            Transaction.Commit();
            return true;
        }

        public virtual int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _context.Database.ExecuteSqlCommand(sql, parameters);
        }

        public virtual Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return _context.Database.ExecuteSqlCommandAsync(sql, parameters);
        }

        public virtual Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters)
        {
            return _context.Database.ExecuteSqlCommandAsync(sql, parameters, cancellationToken);
        }

        public virtual void Rollback()
        {
            Transaction.Rollback();
        }

        public virtual int SaveChanges() => _context.SaveChanges();

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public virtual IRepository<TEntity> Repository<TEntity>() where TEntity : class, ITrackable
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepository<TEntity>>();
            }

            return RepositoryAsync<TEntity>();
        }

        public virtual IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, ITrackable
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepositoryAsync<TEntity>>();
            }

            if (Repositories == null)
            {
                Repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;

            if (Repositories.ContainsKey(type))
            {
                return (IRepositoryAsync<TEntity>)Repositories[type];
            }

            var repositoryType = typeof(Repository<>);

            Repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context, this));

            return Repositories[type];
        }
    }
}
