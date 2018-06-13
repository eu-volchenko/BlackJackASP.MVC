using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Round
{
    public class RoundModelView
    {
        public int Id { get; set; }
        public int RoundInGame { get; set; }
        public int GameId { get; set; }
        public string WinnerName { get; set; }
    }
}
