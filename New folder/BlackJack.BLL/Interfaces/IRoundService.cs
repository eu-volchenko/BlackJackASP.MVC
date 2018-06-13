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
    public interface IRoundService
    {
        Task<InnerGameViewModel> GetGameInfo(int id);

        Task<UserCardsModelView> GetCardsForStartGame(int gameId, string userName, int idRound);

        Task<RoundModelView> CreateRound(int gameId);

        UserViewModel GetUser(int idGame, string userName);

        Task<int> GetCard(UserViewModel userModelView, int roundId);

        Task<WinnerModelView> LearnTheWinner(InnerRoundViewModel model);

        Task GetCardsForBots(UserViewModel userViewModel, int roundId);
    }
}
