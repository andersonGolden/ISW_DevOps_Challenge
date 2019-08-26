using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISW_RestAPI.RepoPatterns
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<TEntity> GetInstance<TEntity>() where TEntity : class;
        int Complete();
    }
}
