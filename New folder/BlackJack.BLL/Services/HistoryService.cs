using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.Interfaces;
using BlackJackDAL.EF;
using BlackJackDAL.Entities;
using BlackJackDAL.Interfaces;
using BlackJackDAL.Repositories;
using ViewModel.History;
using ViewModel.Round;
using ViewModel.StartGame;

namespace BlackJack.BLL.Services
{
    public class HistoryService:IHistoryService
    {
        

        private readonly IGenericRepository<Game> _gameRepository;
        private readonly IGenericRepository<History> _historyRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Round> _roundRepository;
        private readonly IGenericRepository<UserCard> _userCardRepository;
        private int countUsersWithoutBots = 2;


        public HistoryService(IGenericRepository<UserCard> userCardRepository, IGenericRepository<Game> gameRepository, IGenericRepository<User> userRepository, IGenericRepository<History> historyRepository, IGenericRepository<Round> roundRepository)
        {
            _userCardRepository = userCardRepository;
            _userRepository = userRepository;
            _gameRepository = gameRepository;
            _historyRepository = historyRepository;
            _roundRepository = roundRepository;
        }
        

        public async Task<List<GameHistoriesModelView>> GetGames()
        {
            var gameHistorieses = new List<GameHistoriesModelView>();
            var listOfHistories = await _historyRepository.GetAllAsync();
            foreach (var history in listOfHistories)
            {
                var game = await _gameRepository.GetAsync(history.GameId);
                var gameHistory = new GameHistoriesModelView();
                gameHistory.DateTimeGame = history.LogDateTime;
                gameHistory.CountOfBots = game.NumberOfPlayers - countUsersWithoutBots;
                gameHistory.Id = game.Id;
                gameHistorieses.Add(gameHistory);
            }
            return gameHistorieses;
        }

        public List<RoundModelView> GetRounds(int gameId)
        {
            var roundsInGame = _roundRepository.GetAll().Where(round => round.GameId == gameId).ToList();
            var roundsList = new List<RoundModelView>();
            foreach (var currentRound in roundsInGame)
            {
                var round = new RoundModelView();
                round.Id = currentRound.Id;
                round.GameId = gameId;
                round.RoundInGame = currentRound.RoundInGame;
                round.WinnerName = _userRepository.Get(currentRound.UserId).Name;
                roundsList.Add(round);
            }
            return roundsList;
        }

        public RoundPlayersModelView GetPlayers(int gameId)
        {
            var playersInGame = _userRepository.GetAll().Where(player => player.GameId == gameId).ToList();
            var playersId = new List<int>();
            foreach (var user in playersInGame)
            {
                playersId.Add(user.Id);
            }
            var roundPlayers = new RoundPlayersModelView();
            roundPlayers.PlayersId = playersId;
            return roundPlayers;
        }

        public async Task<PlayerCardHistoryModelView> GetPlayersCards(int roundId, int userId)
        {
            var user = await _userRepository.GetAsync(userId);
            var cards = _userCardRepository.GetAll().Where(player => player.UserId == userId && player.RoundId == roundId).ToList();
            var cardsList = new List<int>();
            foreach (var userCard in cards)
            {
                cardsList.Add(userCard.CardId);
            }

            var playerCardHistoryModelView = new PlayerCardHistoryModelView();
            playerCardHistoryModelView.PlayerName = user.Name;
            playerCardHistoryModelView.CardsId = cardsList;
            return playerCardHistoryModelView;
        }
    }
}
