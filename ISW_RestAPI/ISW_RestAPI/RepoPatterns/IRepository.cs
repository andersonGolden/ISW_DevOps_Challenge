using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ISW_RestAPI.RepoPatterns
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        int Save();
        void UpdateByWhereClause(Expression<Func<TEntity, bool>> wherePredict, Action<TEntity> forEachPredict);
        IEnumerable<TEntity> GetItemsByPage(int pageIndex, int pageSize = 10);

        DbRawSqlQuery<T> ExecuteProcedure<T>(string ProcedureWithParameters, params object[] parameters);
        void ExecuteStoreprocedure(string storedProcedureNameAndParameterPlaceholder, params object[] parameters);

        IQueryable<TEntity> GetQueryable();
    }
}
