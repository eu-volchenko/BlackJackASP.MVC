using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.History;
using ViewModel.Round;
using ViewModel.StartGame;

namespace BlackJack.BLL.Interfaces
{
    public interface IHistoryService
    {
        Task<List<GameHistoriesModelView>> GetGames();
        List<RoundModelView> GetRounds(int gameId);
        RoundPlayersModelView GetPlayers(int gameId);
        Task<PlayerCardHistoryModelView> GetPlayersCards(int roundId, int userId);
    }
}
