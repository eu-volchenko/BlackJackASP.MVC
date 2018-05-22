using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackDAL.EF;
using BlackJackDAL.Entities;

namespace BlackJackDAL.Repositories
{
    class UserCardRepository:DpGenericRepository<UserCard>
    {
        public UserCardRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
