using ShoppingMart.Domain.Base;
using ShoppingMart.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMart.Infastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ShoppingMartDbContext DbContext;
        public string _name;
        public BaseRepository(string message)
        {
            _name = message;
        }
        public BaseRepository(ShoppingMartDbContext context)
        {
            DbContext = context;
        }
        public void AddEntity(T entity) =>
            DbContext.Set<T>().Add(entity);
        public async Task<T> FindEntityAsync(params object[] keys) =>
            await DbContext.Set<T>().FindAsync(keys);


        public void UpdateEntity(T entity) =>
            DbContext.Set<T>().Update(entity);

        public void DisposeContext()
        {
            ClearChangeTracker();
            DbContext.Dispose();
        }

        private void ClearChangeTracker()
        {
            foreach (var entry in DbContext.ChangeTracker.Entries())
            {
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
        }
    }
}
