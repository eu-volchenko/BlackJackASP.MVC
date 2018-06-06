using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackDAL.EF;
using BlackJackDAL.Entities;


namespace BlackJackDAL.Repositories
{
    public class TypeRepository:DpGenericRepository<Type>
    {
        private static string _tableName = "Types";
        public TypeRepository(string connectionString) : base(connectionString, _tableName)
        {
        }
    }
}
