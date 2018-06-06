using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackDAL.EF;
using BlackJackDAL.Entities;

namespace BlackJackDAL.Repositories
{
    public class CardRepository:DpGenericRepository<Card>
    {
        private static string _tableName = "Cards";
        public CardRepository(string connectionString):base(connectionString, _tableName)
        {
            
        }
    }
}
