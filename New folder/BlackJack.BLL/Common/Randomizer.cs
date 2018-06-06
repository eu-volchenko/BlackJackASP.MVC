using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.Common
{
    static class Randomizer
    {

        public static int RandomId()
        {
            Random random = new Random();
            int randomId = random.Next(1, 48);
            return randomId;
        }
    }
}
