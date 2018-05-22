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
        public GameRepository(string connectionString) : base(connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public int CreateAndKnowId(Game itemGame)
        {
            using (_connection)
            {
                 _connection.Insert(itemGame);
            }

            int id = itemGame.Id;
            return id;
        }
    }
}
