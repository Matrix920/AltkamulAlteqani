using BaseRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackableEntities;

namespace BaseRepository.UnitOfWork
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        int ExecuteSqlCommand(string sql, params object[] parameters);
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, ITrackable;
        int? CommandTimeout { get; set; }
        void BeginTransaction(IsolationLevel isolationlevel = IsolationLevel.Unspecified);
        bool Commit();
        void Rollback();
    }
}
