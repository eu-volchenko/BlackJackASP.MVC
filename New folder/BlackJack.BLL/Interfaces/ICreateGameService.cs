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
        Task AddBots(InnerGameModel gameData, int id);

        Task AddPlayer(InnerGameModel gameData, int id);

        int AddGame(InnerGameModel gamedata);

        Task AddDealer(InnerGameModel gameData, int id);
    }
}
