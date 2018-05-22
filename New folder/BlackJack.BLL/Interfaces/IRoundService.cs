using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackDAL.Entities;
using ViewModel.Round;
using ViewModel.StartGame;

namespace BlackJack.BLL.Interfaces
{
    interface IRoundService
    {
        InnerGameModel GetGameModel(int id);
    }
}
