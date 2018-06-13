using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlackJackDAL.Entities;

namespace BlackJackDAL.Interfaces
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);
        void Create(TEntity item);
        Task CreateAsync(TEntity item);
        void Remove(TEntity item);
        Task RemoveAsync(TEntity item);
        void Update(TEntity item);
        Task UpdateAsync(TEntity item);
    }
}
