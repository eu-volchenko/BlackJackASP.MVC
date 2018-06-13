using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Round
{
    public class PlayerCardModelView
    {
        public int PlayerId { get; set; }
        private List<CardModelView> CardsOfPlayer { get; set; }
    }
}
