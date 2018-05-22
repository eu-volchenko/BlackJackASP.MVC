using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackDAL.Entities;

namespace BlackJackDAL.Interfaces
{
    public interface IDpGenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity:class
    {
        
    }
}
