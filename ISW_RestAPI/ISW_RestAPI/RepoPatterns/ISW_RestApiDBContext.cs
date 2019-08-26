using ISW_RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ISW_RestAPI.RepoPatterns
{
    public class ISW_RestApiDBContext : DbContext
    {
        public ISW_RestApiDBContext()
           : base("name=ISW_RestApiDBContext")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Deployment> Deployments { get; set; }
    }
}