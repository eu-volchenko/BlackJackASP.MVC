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
using NLog;


namespace BlackJackDAL.EF
{
    public class DpGenericRepository<TEntity> : IDpGenericRepository<TEntity> where TEntity : class
    {
        private  IDbConnection _connection;
        private readonly string _connectionString;
        private readonly string _tableName;
        public DpGenericRepository(string connectionString, string tableName)
        {
            _tableName = tableName;
            _connectionString = connectionString;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public IEnumerable<TEntity> GetAll()
        {
            IEnumerable<TEntity> listOfAll;
            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                listOfAll = _connection.GetAll<TEntity>();
                _connection.Close();
            }

            return listOfAll;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                IEnumerable<TEntity> listOfAll;
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    listOfAll = await _connection.QueryAsync<TEntity>("SELECT * FROM " + _tableName);
                    _connection.Close();
                }

                return listOfAll;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public TEntity Get(int id)
        {
            TEntity entity;
            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                entity = _connection.Get<TEntity>(id);
                _connection.Close();
            }

            return entity;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            try
            {
                TEntity entity;
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    entity = await _connection.GetAsync<TEntity>(id);
                    _connection.Close();
                }

                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public void Create(TEntity item)
        {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    _connection.Insert(item);
                    _connection.Close();
                }
        }

        public async Task CreateAsync(TEntity item)
        {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    await _connection.InsertAsync(item);
                    _connection.Close();
                }
        }

        public void Remove(TEntity item)
            {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    _connection.Delete(item);
                    _connection.Open();
                }
            }

            public async Task RemoveAsync(TEntity item)
            {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    await _connection.DeleteAsync(item);
                    _connection.Close();
                }
            }

            public void Update(TEntity item)
            {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    _connection.Update(item);
                    _connection.Close();
                }
            }

            public async Task UpdateAsync(TEntity item)
            {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    await _connection.UpdateAsync(item);
                _connection.Close();
                }
            }

            public IEnumerable<TEntity> GetSomeEntities(Func<TEntity, bool> predicat)
            {
                //using (_connection)
                //{
                //    Task<IEnumerable<TEntity>> listOfAll = _connection.GetAllAsync<TEntity>();
                //    Task<IEnumerable<TEntity>> listEntities = SortAllEntities(predicat, listOfAll);
                //    return listEntities.Result;
                //}
                throw new NotImplementedException();
            }

            

            //private async Task<IEnumerable<TEntity>> SortAllEntities(Func<TEntity, bool> predicat,
            //    Task<IEnumerable<TEntity>> allEntities)
            //{
            //    IEnumerable<TEntity> listOfAll = allEntities.Result;
            //    IEnumerable<TEntity> needEntities = listOfAll.Where(predicat).ToList();
            //    return needEntities;
            //}
        }

    }
