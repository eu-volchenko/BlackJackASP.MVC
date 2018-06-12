using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackDAL.EF;
using BlackJackDAL.Entities;
using Dapper;

namespace BlackJackDAL.Repositories
{
    public class UserRepository:DpGenericRepository<User>
    {
        private static string _tableName = "Users";
        private IDbConnection _connection;
        private readonly string _connectionString;
        public UserRepository(string connectionString) : base(connectionString, _tableName)
        {
            _connectionString = connectionString;
           _connection = new SqlConnection(connectionString);
        }


        public async Task<IEnumerable<dynamic>> GetSomeEntitiesAsync(int gameId)
        {
            try
            {
                using (_connection)
                {
                    _connection.Open();
                    var listOfUsers = await _connection.QueryAsync("SELECT * FROM " + _tableName + " WHERE GameId=@game", new { game = gameId});
                    _connection.Close();
                    return listOfUsers;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
            
        }

        public User GetUserByNameAndGame(int gameId, string userName)
        {
            try
            {
                User user = new User();
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    user =  _connection.QuerySingle<User>(
                        "SELECT * FROM " + _tableName + " WHERE GameId=@game AND Name=@Name",
                        new {game = gameId, Name = userName});
                    _connection.Close();
                }

                return user;
            }
            catch (Exception exception)
            {
                Console.Write(exception);
                throw;
            }
        }
    }
}
