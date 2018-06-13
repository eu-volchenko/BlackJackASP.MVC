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

        public TypeRepository() : base()
        {
        }
    }
}
