using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Round
{
    public class UserCardsModelView
    {
        public int UserId { get; set; }
        public List<int> UserCardsId { get; set; }= new List<int>();
    }
}
