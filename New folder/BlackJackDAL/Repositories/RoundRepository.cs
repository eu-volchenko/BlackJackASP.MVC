using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackDAL.EF;
using BlackJackDAL.Entities;

namespace BlackJackDAL.Repositories
{
    public class RoundRepository:DpGenericRepository<Round>
    {
        private readonly static string _tableName = "Rounds";
        public RoundRepository(string connectionString) : base(connectionString, _tableName)
        {
        }
    }
}
