using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMart.Domain.Base
{
    public interface IBaseRepository<T>
        where T : class
    {
        /// <summary>
        /// Find an entity with a given set of keys
        /// </summary>
        /// <params name = "keys"></params>
        /// <returns>A matching T instance or null</returns>

        Task<T> FindEntityAsync(params object[] keys);

        void AddEntity(T entity);
        void UpdateEntity(T entity);

        void DisposeContext();
    }
}
