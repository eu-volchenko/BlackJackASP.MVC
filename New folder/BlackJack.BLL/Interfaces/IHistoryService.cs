using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.History;
using ViewModel.HistoryViewModels;
using ViewModel.Round;
using ViewModel.RoundViewModels;

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
