using AutoMapper;
using ShoppingMart.Domain.Base;
using ShoppingMart.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMart.Infastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ShoppingMartDbContext DbContext;
       
        
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


        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default, bool clearChangeTracker = false)
        {
            var result = await DbContext.SaveChangesAsync(cancellationToken);
            if (clearChangeTracker)
                ClearChangeTracker();
            return result;
        }

        //Not neccessary.
        //But better than calling detached on each entity
        private void ClearChangeTracker()
        {
            foreach (var entry in DbContext.ChangeTracker.Entries())
            {
                Console.WriteLine(entry);
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
        }

        public void DisposeContext()
        {
            ClearChangeTracker();
            DbContext.Dispose();
        }


    }
}
