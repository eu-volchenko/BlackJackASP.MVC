using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BlackJack.Utility.Utilities;
using BlackJackDAL.Interfaces;
using Dapper.Contrib.Extensions;


namespace BlackJackDAL.EF
{
    public class DpGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private IDbConnection _connection;
        private readonly string _connectionString = System.Configuration.ConfigurationManager.
            ConnectionStrings["ContextDB"].ConnectionString;
        public DpGenericRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
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
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "DpGenericRepository");
                return null;
            }

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                IEnumerable<TEntity> listOfAll;
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    listOfAll = await _connection.GetAllAsync<TEntity>();
                    _connection.Close();
                }

                return listOfAll;
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "DpGenericRepository");
                return null;
            }

        }

        public TEntity Get(int id)
        {
            try
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
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "DpGenericRepository");
                return null;
            }

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
                LogWriter.WriteLog(e.Message, "DpGenericRepository");
                return null;
            }

        }

        public void Create(TEntity item)
        {
            try
            {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    _connection.Insert(item);
                    _connection.Close();
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "DpGenericRepository");
            }

        }

        public async Task CreateAsync(TEntity item)
        {
            try
            {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    await _connection.InsertAsync(item);
                    _connection.Close();
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "DpGenericRepository");
                
            }

        }
        public void Remove(TEntity item)
        {
            try
            {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    _connection.Delete(item);
                    _connection.Open();
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "DpGenericRepository");
                
            }

        }

        public async Task RemoveAsync(TEntity item)
        {
            try
            {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    await _connection.DeleteAsync(item);
                    _connection.Close();
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "DpGenericRepository");
                
            }

        }

        public void Update(TEntity item)
        {
            try
            {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    _connection.Update(item);
                    _connection.Close();
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "DpGenericRepository");
                
            }

        }

        public async Task UpdateAsync(TEntity item)
        {
            try
            {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    await _connection.UpdateAsync(item);
                    _connection.Close();
                }
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "DpGenericRepository");
                
            }
            
        }
    }

}
