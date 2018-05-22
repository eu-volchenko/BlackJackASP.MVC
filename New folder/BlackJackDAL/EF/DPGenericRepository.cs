using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BlackJackDAL.Entities;
using BlackJackDAL.Interfaces;
using Dapper;
using Dapper.Contrib.Extensions;


namespace BlackJackDAL.EF
{
    public class DpGenericRepository<TEntity> : IDpGenericRepository<TEntity> where TEntity : class
    {
        private readonly IDbConnection _connection;
        public DpGenericRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public IEnumerable<TEntity> GetAll()
        {
            IEnumerable<TEntity> listOfAll;
            using (_connection)
            {
                listOfAll = _connection.GetAll<TEntity>();
            }

            return listOfAll;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            IEnumerable<TEntity> listOfAll;
            using (_connection)
            {
                listOfAll = await _connection.GetAllAsync<TEntity>();
            }

            return listOfAll;
        }

        public TEntity Get(Func<TEntity, bool> item)
        {
            TEntity entity;
            using (_connection)
            {
                entity = _connection.Get<TEntity>(item);
            }

            return entity;
        }

        public async Task<TEntity> GetAsync(Func<TEntity,bool> item)
        {
            TEntity entity;
            using (_connection)
            {
                entity = await _connection.GetAsync<TEntity>(item);
            }

            return entity;
        }

        public void Create(TEntity item)
        {
            using (_connection)
            {
                _connection.Insert(item);
            }
        }

        public async Task CreateAsync(TEntity item)
        {
            using (_connection)
            {
                await _connection.InsertAsync(item);
            }
        }

        public void Remove(TEntity item)
        {
            using (_connection)
            {
                _connection.Delete(item);
            }
        }

        public async Task RemoveAsync(TEntity item)
        {
            using (_connection)
            {
                await _connection.DeleteAsync(item);
            }
        }

        public void Update(TEntity item)
        {
            using (_connection)
            {
                _connection.Update(item);
            }
        }

        public async Task UpdateAsync(TEntity item)
        {
            using (_connection)
            {
                await _connection.UpdateAsync(item);
            }
        }

        public IEnumerable<TEntity> GetSomeEntities(Func<TEntity, bool> predicat)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetSomeEntitiesAsync(Func<TEntity, bool> predicat)
        {
            throw new NotImplementedException();
        }
    }
    
}
