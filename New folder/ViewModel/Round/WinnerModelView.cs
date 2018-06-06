using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Round
{
    public class WinnerModelView
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public IEnumerable<int> Cards { get; set; }
    }
}
