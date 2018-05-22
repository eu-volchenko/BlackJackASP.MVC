using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BLL.DTO
{
    class HistoryDTO
    {
        public int Id { get; set; }

        public DateTime LogDateTime { get; set; }

        public int GameId { get; set; }
    }
}
