using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.StartGame;

namespace BlackJack.BLL.Interfaces
{
    public interface ICreateGameService
    {
        Task AddBots(InnerGameViewModel gameData, int id);

        Task AddPlayer(InnerGameViewModel gameData, int id);

        int AddGame(InnerGameViewModel gamedata);

        Task AddDealer(InnerGameViewModel gameData, int id);
    }
}
