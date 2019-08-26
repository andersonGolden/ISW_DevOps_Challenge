using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ISW_RestAPI.RepoPatterns
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
           params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            return includeExpressions.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
           (Context.Set<TEntity>(), (current, expression) => current.Include(expression)).Where(predicate).ToList();

        }
        public void UpdateByWhereClause(Expression<Func<TEntity, bool>> wherePredict, Action<TEntity> forEachPredict)
        {
            Context.Set<TEntity>().Where(wherePredict).ToList().ForEach(forEachPredict);
        }
        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {

            Context.Set<TEntity>().Attach(entity);
            Context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public IEnumerable<TEntity> GetItemsByPage(int pageIndex, int pageSize = 10)
        {
            return Context.Set<TEntity>().Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public DbRawSqlQuery<T> ExecuteProcedure<T>(string ProcedureWithParameters, params object[] parameters)
        {
            return Context.Database.SqlQuery<T>(ProcedureWithParameters, parameters);
        }
        public void ExecuteStoreprocedure(string storedProcedureNameAndParameterPlaceholder, params object[] parameters)
        {
            Context.Database.ExecuteSqlCommand("EXEC " + storedProcedureNameAndParameterPlaceholder, parameters);
        }
        public void Dispose(bool disposing)
        {
            if (disposing && Context != null)
            {
                Context.Dispose();
                //Context = null;
            }

            this.Dispose(disposing);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return Context.Set<TEntity>();
        }
    }
}