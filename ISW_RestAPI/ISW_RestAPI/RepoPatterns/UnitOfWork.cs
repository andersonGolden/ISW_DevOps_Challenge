using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISW_RestAPI.RepoPatterns
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISW_RestApiDBContext _context = new ISW_RestApiDBContext();
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<TEntity> GetInstance<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(_context);
        }

        public UnitOfWork(ISW_RestApiDBContext context)
        {
            _context = context;

        }
    }
}