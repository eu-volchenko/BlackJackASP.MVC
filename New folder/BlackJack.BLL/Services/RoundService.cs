using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlackJack.BLL.Common;
using BlackJack.BLL.DTO;
using BlackJack.BLL.Interfaces;
using BlackJack.BLL.Mapper;
using BlackJackDAL.EF;
using BlackJackDAL.Entities;
using BlackJackDAL.Interfaces;
using BlackJackDAL.Repositories;
using ViewModel.Round;
using ViewModel.StartGame;

namespace BlackJack.BLL.Services
{
    public class RoundService : IRoundService
    {
        private readonly string _connectionString =
            System.Configuration.ConfigurationManager.ConnectionStrings["ContextDB"].ConnectionString;

        private readonly GameRepository _gameRepository;
        private readonly UserRepository _userRepository;
        private readonly IGenericRepository<Card> _cardRepository;
        private readonly IGenericRepository<Round> _roundRepository;
        private readonly IGenericRepository<UserCard> _userCardRepository;

        public RoundService()
        {
            _gameRepository = new GameRepository(_connectionString);
            _userRepository = new UserRepository(_connectionString);
            _cardRepository = new CardRepository(_connectionString);
            _userCardRepository = new DpGenericRepository<UserCard>(_connectionString, "UserCards");
            _roundRepository = new DpGenericRepository<Round>(_connectionString, "Rounds");
        }


        public async Task<InnerGameModel> GetGameInfo(int id)
        {
            try
            {
                InnerGameModel gameModel = new InnerGameModel();
                EntitiesToDTO entitiesToDto = new EntitiesToDTO();
                DTOToModelView dtoToModelView = new DTOToModelView();
                var listOfUserDto = new List<UserDTO>();
                var userList = await _userRepository.GetSomeEntitiesAsync(id);
                //                string [] botsName = new string[userList.Count()-2];
                foreach (var item in userList)
                {
                    if (item.TypeId == 1) gameModel.DealerName = item.Name;
                    if (item.TypeId == 2) gameModel.NameOfBots.Add(item.Name);
                    if (item.TypeId == 3) gameModel.PlayerName = item.Name;
                }

                //        gameModel.NameOfBots = botsName;
                gameModel.NumberOfBots = gameModel.NameOfBots.Count;
                return gameModel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<UserCardsModelView> GetCardsForStartGame(int gameId, string userName, int idRound)
        {
            
            var user = _userRepository.GetUserByNameAndGame(gameId, userName);
            UserCardsModelView userCards = new UserCardsModelView { UserId = user.Id };
            for (int i = 0; i < 2; i++)
            {
                var random = Randomizer.RandomId();
                var cardForUser = _cardRepository.Get(random);
                userCards.UserCardsId.Add(cardForUser.Id);
                var userCard = new UserCard()
                {
                    CardId = cardForUser.Id,
                    UserId = user.Id,
                    RoundId = idRound
                };
                await _userCardRepository.CreateAsync(userCard);
                Thread.Sleep(100);
            }
            return userCards;   
        }

        public async Task<RoundModelView> CreateRound(int gameId)
        {
            var rounds = _roundRepository.GetAll().Where(x => x.GameId == gameId);
            int roundsCount = rounds.Count();
            var createRound = new Round()
            {
                GameId = gameId,
                RoundInGame = roundsCount++,
                UserId = 4
            };
            await _roundRepository.CreateAsync(createRound);
            RoundModelView roundModelView = new RoundModelView()
            {
                Id = createRound.Id,
                GameId = createRound.GameId,
                RoundInGame = createRound.RoundInGame
            };
            return roundModelView;
        }

       

        private string[] ReturnBotsNames(IEnumerable<dynamic> users)
        {
            int i = 0;
            string[] names = new string[users.Count()];
            foreach (var user in users)
            {
                names[i] = user.Name;
                i++;
            }

            return names;
        }

        public UserViewModel GetUser(int idGame, string userName)
        {
            User user = _userRepository.GetUserByNameAndGame(idGame, userName);
            UserViewModel userModelView = new UserViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Type = user.TypeId
            };
            return userModelView;
        }

        public async Task<int> GetCard(UserViewModel userModelView,int roundId)
        {
            int cardId = Randomizer.RandomId();
            UserCard userCard = new UserCard()
            {
                CardId = cardId,
                UserId = userModelView.Id,
                RoundId = roundId
            };
            await _userCardRepository.CreateAsync(userCard);
            return cardId;
        }

        public async Task<WinnerModelView> LearnTheWinner(InnerRoundViewModel model)
        {
            try
            {
                var botsScore = new List<int>();
                var botsCards = new List<IEnumerable<UserCard>>();
                var player = _userRepository.GetUserByNameAndGame(model.Id, model.PlayerName);
                var playerCards = _userCardRepository.GetAll()
                    .Where(x => x.RoundId == model.RoundId && x.UserId == player.Id);
                var dealer =  _userRepository.GetUserByNameAndGame(model.Id, model.DealerName);
                var dealerCards = _userCardRepository.GetAll()
                    .Where(x => x.RoundId == model.RoundId && x.UserId == dealer.Id);
                for (int i = 0; i < model.NumberOfBots; i++)
                {
                    var bot =_userRepository.GetUserByNameAndGame(model.Id, model.NameOfBots[i]);
                    var botCards = _userCardRepository.GetAll()
                        .Where(x => x.RoundId == model.RoundId && x.UserId == bot.Id);
                    botsCards.Add(botCards);
                }
                int max = 0;
                string winnerName = ";";
                IEnumerable<UserCard> winnerCards = null;
                var playerScore = PointCount(playerCards);
                var dealerScore = PointCount(dealerCards);
                for (int i = 0; i < model.NumberOfBots; i++)
                {
                    botsScore.Add(PointCount(botsCards[i]));
                }

                for (int i = 0; i < model.NumberOfBots; i++)
                {
                    if (botsScore[i] > max && botsScore[i] <= 21)
                    {
                        max = botsScore[i];
                        winnerName = model.NameOfBots[i];
                        winnerCards = botsCards[i];
                    }
                }

                if (playerScore > max && playerScore <= 21)
                {
                    max = playerScore;
                    winnerName = model.PlayerName;
                    winnerCards = playerCards;
                }

                if (dealerScore > max && dealerScore <= 21)
                {
                    max = dealerScore;
                    winnerName = model.DealerName;
                    winnerCards = dealerCards;
                }

                WinnerModelView winner = new WinnerModelView()
                {
                    Name = winnerName,
                    Score = max,
                    UserId = _userRepository.GetUserByNameAndGame(model.Id, winnerName).Id,
                };
                winner.Cards = getWinnerCards(winnerCards);
                var round = await  _roundRepository.GetAsync(model.RoundId);
                round.UserId = winner.UserId;
                await _roundRepository.UpdateAsync(round);
                return winner;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task GetCardsForBots(UserViewModel userViewModel, int roundId)
        {
            var userCards = _userCardRepository.GetAll()
                .Where(x => x.RoundId == roundId && x.UserId == userViewModel.Id);
            var userPoints = PointCount(userCards);
            while (userPoints < 17)
            {
                var randomCard = Randomizer.RandomId();
                var userCard = new UserCard()
                {
                    CardId = randomCard,
                    UserId = userViewModel.Id,
                    RoundId = roundId
                };
                await _userCardRepository.CreateAsync(userCard);
                userPoints +=  _cardRepository.Get(randomCard).Cost;
            }
        }

        private List<int> getWinnerCards(IEnumerable<UserCard> userCards)
        {
            List<int> cards = new List<int>();
            foreach (var userCard in userCards)
            {
                cards.Add(userCard.CardId);
            }
            return cards;
        }

        private int PointCount(IEnumerable<UserCard> userCards)
        {
            int sum = 0;
            foreach (var userCard in userCards)
            {
                Card card = _cardRepository.Get(userCard.CardId);
                sum += card.Cost;
            }
            return sum;
        }

    }

}

