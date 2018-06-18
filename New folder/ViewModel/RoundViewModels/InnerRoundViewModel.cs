using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Round
{
    public class InnerRoundViewModel
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int NumberOfBots { get; set; }
        public string DealerName { get; set; }
        public List<string> NameOfBots { get; set; } = new List<string>();
        public int RoundId { get; set; }
    }
}
