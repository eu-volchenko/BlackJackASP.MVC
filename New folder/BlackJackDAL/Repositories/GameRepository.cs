using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackDAL.EF;
using BlackJackDAL.Entities;
using Dapper.Contrib.Extensions;

namespace BlackJackDAL.Repositories
{
    public class GameRepository : DpGenericRepository<Game>
    {
        private readonly IDbConnection _connection;
        private static string _tableName = "Games";
        public GameRepository(string connectionString) : base(connectionString, _tableName)
        {
            _connection = new SqlConnection(connectionString);
        }

        public int CreateAndKnowId(Game itemGame)
        {
            try
            {
                using (_connection)
                {
                    _connection.Insert(itemGame);
                }

                int id = itemGame.Id;
                return id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
    }
}
