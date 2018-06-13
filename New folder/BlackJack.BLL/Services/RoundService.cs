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
using BlackJackDAL.Enums;
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
        private readonly int maxCountOfPoints = 21;

        public RoundService(IGenericRepository<Card> cardRepository, IGenericRepository<UserCard> userCardRepository, IGenericRepository<Round> roundRepository)
        {
            _gameRepository = new GameRepository();
            _userRepository = new UserRepository();
            _cardRepository = cardRepository;
            _userCardRepository = userCardRepository;
            _roundRepository = roundRepository;
        }


        public async Task<InnerGameViewModel> GetGameInfo(int id)
        {
            try
            {
                InnerGameViewModel gameModel = new InnerGameViewModel();
                EntitiesToDTO entitiesToDto = new EntitiesToDTO();
                var listOfUserDto = new List<UserDTO>();
                var userList = await _userRepository.GetSomeEntitiesAsync(id);
                foreach (var item in userList)
                {
                    if (item.TypeId == (int)PlayerTypeEnum.Dealer) gameModel.DealerName = item.Name;
                    if (item.TypeId == (int)PlayerTypeEnum.Bot) gameModel.NameOfBots.Add(item.Name);
                    if (item.TypeId == (int)PlayerTypeEnum.Player) gameModel.PlayerName = item.Name;
                }
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
            var userCards = new UserCardsModelView();
            userCards.UserId = user.Id;
            for (int i = 0; i < 2; i++)
            {
                var random = Randomizer.RandomId();
                var cardForUser = _cardRepository.Get(random);
                userCards.UserCardsId.Add(cardForUser.Id);
                var userCard = new UserCard();
                userCard.CardId = cardForUser.Id;
                userCard.UserId = user.Id;
                userCard.RoundId = idRound;
                await _userCardRepository.CreateAsync(userCard);
                Thread.Sleep(100);
            }
            return userCards;   
        }

        public async Task<RoundModelView> CreateRound(int gameId)
        {
            var rounds = _roundRepository.GetAll().Where(x => x.GameId == gameId).ToList();
            int roundsCount = rounds.Count();
            var createRound = new Round();
            createRound.GameId = gameId;
            createRound.RoundInGame = ++roundsCount;
            createRound.UserId = 4;
            await _roundRepository.CreateAsync(createRound);
            var roundModelView = new RoundModelView();
            roundModelView.Id = createRound.Id;
            roundModelView.GameId = createRound.GameId;
            roundModelView.RoundInGame = createRound.RoundInGame;
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
            var user = _userRepository.GetUserByNameAndGame(idGame, userName);
            var userModelView = new UserViewModel();
            userModelView.Id = user.Id;
            userModelView.Name = user.Name;
            userModelView.Type = user.TypeId;
            return userModelView;
        }

        public async Task<int> GetCard(UserViewModel userModelView,int roundId)
        {
            var cardId = Randomizer.RandomId();
            var userCard = new UserCard();
            userCard.CardId = cardId;
            userCard.UserId = userModelView.Id;
            userCard.RoundId = roundId;
            await _userCardRepository.CreateAsync(userCard);
            return cardId;
        }

        public async Task<WinnerModelView> LearnTheWinner(InnerRoundViewModel model)
        {
            try
            {
                int maxCountOfPointAmongPlayers = 0;
                var botsScore = new List<int>();
                var botsCards = new List<IEnumerable<UserCard>>();
                var player = _userRepository.GetUserByNameAndGame(model.Id, model.PlayerName);
                var playerCards = _userCardRepository.GetAll()
                    .Where(x => x.RoundId == model.RoundId && x.UserId == player.Id).ToList();
                var dealer =  _userRepository.GetUserByNameAndGame(model.Id, model.DealerName);
                var dealerCards = _userCardRepository.GetAll()
                    .Where(x => x.RoundId == model.RoundId && x.UserId == dealer.Id).ToList();
                for (int i = 0; i < model.NumberOfBots; i++)
                {
                    var bot =_userRepository.GetUserByNameAndGame(model.Id, model.NameOfBots[i]);
                    var botCards = _userCardRepository.GetAll()
                        .Where(x => x.RoundId == model.RoundId && x.UserId == bot.Id).ToList();
                    botsCards.Add(botCards);
                }
                
                string winnerName = "";
                IEnumerable<UserCard> winnerCards = null;
                var playerScore = PointCount(playerCards);
                var dealerScore = PointCount(dealerCards);
                for (int i = 0; i < model.NumberOfBots; i++)
                {
                    botsScore.Add(PointCount(botsCards[i]));
                }

                for (int i = 0; i < model.NumberOfBots; i++)
                {
                    if (botsScore[i] > maxCountOfPointAmongPlayers && botsScore[i] <= maxCountOfPoints)
                    {
                        maxCountOfPointAmongPlayers = botsScore[i];
                        winnerName = model.NameOfBots[i];
                        winnerCards = botsCards[i];
                    }
                }

                if (playerScore > maxCountOfPointAmongPlayers && playerScore <= maxCountOfPoints)
                {
                  maxCountOfPointAmongPlayers = playerScore;
                    winnerName = model.PlayerName;
                    winnerCards = playerCards;
                }

                if (dealerScore > maxCountOfPointAmongPlayers && dealerScore <= maxCountOfPoints)
                {
                    maxCountOfPointAmongPlayers = dealerScore;
                    winnerName = model.DealerName;
                    winnerCards = dealerCards;
                }

                var winner = new WinnerModelView();
                winner.Name = winnerName;
                winner.Score = maxCountOfPointAmongPlayers;
                winner.UserId = _userRepository.GetUserByNameAndGame(model.Id, winnerName).Id;
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
                .Where(x => x.RoundId == roundId && x.UserId == userViewModel.Id).ToList();
            var userPoints = PointCount(userCards);
            while (userPoints < 17)
            {
                var randomCard = Randomizer.RandomId();
                var userCard = new UserCard();
                userCard.CardId = randomCard;
                userCard.UserId = userViewModel.Id;
                userCard.RoundId = roundId;
                await _userCardRepository.CreateAsync(userCard);
                userPoints +=  _cardRepository.Get(randomCard).Cost;
            }
        }

        private List<int> getWinnerCards(IEnumerable<UserCard> userCards)
        {
            var cards = new List<int>();
            foreach (var userCard in userCards)
            {
                cards.Add(userCard.CardId);
            }
            return cards;
        }

        private int PointCount(IEnumerable<UserCard> userCards)
        {
            int sumOfPointsCard = 0;
            foreach (var userCard in userCards)
            {
                var card = _cardRepository.Get(userCard.CardId);
                sumOfPointsCard += card.Cost;
            }
            return sumOfPointsCard;
        }

    }

}

