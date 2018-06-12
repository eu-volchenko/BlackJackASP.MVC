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
        private readonly string _connectionString = System.Configuration.ConfigurationManager.
            ConnectionStrings["ContextDB"].ConnectionString;

        private readonly IGenericRepository<Game> _gameRepository;
        private readonly IGenericRepository<History> _historyRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Round> _roundRepository;
        private readonly IGenericRepository<UserCard> _userCardRepository;

        public HistoryService()
        {
            _userCardRepository = new UserCardRepository(_connectionString);
            _userRepository = new UserRepository(_connectionString);
            _gameRepository = new GameRepository(_connectionString);
            _historyRepository  = new HistoryRepository(_connectionString);
            _roundRepository = new RoundRepository(_connectionString);
        }


        public async Task<List<GameHistoriesModelView>> GetGames()
        {
            var gameHistorieses = new List<GameHistoriesModelView>();
            var listOfHistories = await _historyRepository.GetAllAsync();
            foreach (var history in listOfHistories)
            {
                Game game = await _gameRepository.GetAsync(history.GameId);
                gameHistorieses.Add(new GameHistoriesModelView()
                {
                    DateTimeGame = history.LogDateTime,
                    CountOfBots = game.NumberOfPlayers-2,
                    Id = game.Id
                });
            }

            return gameHistorieses;
        }

        public List<RoundModelView> GetRounds(int gameId)
        {
            var roundsInGame = _roundRepository.GetAll().Where(round => round.GameId == gameId);
            List<RoundModelView> roundsList = new List<RoundModelView>();
            foreach (var currentRound in roundsInGame)
            {
                var round = new RoundModelView()
                {
                    Id = currentRound.Id,
                    GameId = gameId,
                    RoundInGame = currentRound.RoundInGame+1,
                    WinnerName = _userRepository.Get(currentRound.UserId).Name
                };
                roundsList.Add(round);
            }

            return roundsList;
        }

        public RoundPlayersModelView GetPlayers(int gameId)
        {
            var playersInGame = _userRepository.GetAll().Where(player => player.GameId == gameId);
            List<int> playersId = new List<int>();
            foreach (var user in playersInGame)
            {
                playersId.Add(user.Id);
            }
            RoundPlayersModelView roundPlayers = new RoundPlayersModelView();
            roundPlayers.PlayersId = playersId;
            return roundPlayers;
        }

        public async Task<PlayerCardHistoryModelView> GetPlayersCards(int roundId, int userId)
        {
            var user = await _userRepository.GetAsync(userId);
            var cards = _userCardRepository.GetAll().Where(player => player.UserId == userId && player.RoundId == roundId);
            List<int> cardsList = new List<int>();
            foreach (var userCard in cards)
            {
                cardsList.Add(userCard.CardId);
            }
            PlayerCardHistoryModelView playerCardHistoryModelView = new PlayerCardHistoryModelView()
            {
                PlayerName = user.Name,
                CardsId = cardsList
            };
            return playerCardHistoryModelView;
        }
    }
}
