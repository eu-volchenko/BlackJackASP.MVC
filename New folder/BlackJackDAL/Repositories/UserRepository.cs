using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackDAL.EF;
using BlackJackDAL.Entities;

namespace BlackJackDAL.Repositories
{
    public class UserRepository:DpGenericRepository<User>
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
