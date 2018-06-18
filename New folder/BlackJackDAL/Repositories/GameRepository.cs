using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Utility.Utilities;
using BlackJackDAL.EF;
using BlackJackDAL.Entities;
using Dapper.Contrib.Extensions;

namespace BlackJackDAL.Repositories
{
    public class GameRepository : DpGenericRepository<Game>
    {
        private readonly IDbConnection _connection;
        private readonly string _connectionString = System.Configuration.ConfigurationManager.
            ConnectionStrings["ContextDB"].ConnectionString;
        public GameRepository() : base()
        {
            _connection = new SqlConnection(_connectionString);
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
                LogWriter.WriteLog(e.Message, "GameRepository");
                return 0;
            }
           
        }
    }
}
