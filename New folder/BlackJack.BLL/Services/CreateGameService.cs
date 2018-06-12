using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.DTO;
using BlackJack.BLL.Interfaces;
using BlackJack.BLL.Mapper;
using BlackJackDAL.Entities;
using BlackJackDAL.Interfaces;
using BlackJackDAL.Repositories;
using ViewModel.StartGame;

namespace BlackJack.BLL.Services
{
    public class CreateGameService : ICreateGameService
    {
        private readonly string _connectionString = System.Configuration.ConfigurationManager.
        ConnectionStrings["ContextDB"].ConnectionString;
        private readonly GameRepository _gameRepository;
        private readonly IGenericRepository<History> _historyRepository;
        private readonly IGenericRepository<User> _userRepository;
        public CreateGameService()
        {
            _gameRepository = new GameRepository(_connectionString);
            _historyRepository = new HistoryRepository(_connectionString);
            _userRepository = new UserRepository(_connectionString);
        }

        public async Task AddBots(InnerGameModel gameData, int id)
        {
            var dtoToEntities = new DTOToEntities();
            for (int i = 0; i < gameData.NameOfBots.Count; i++)
            {
                UserDTO bot = new UserDTO()
                {
                    TypeId = 2,
                    GameId = id
                };
                var modelViewToDto = new ModelViewToDTO();
                bot = modelViewToDto.GetBotDto(gameData.NameOfBots[i], bot);
                var botEntity = dtoToEntities.GetBot(bot);
                var task = _userRepository.CreateAsync(botEntity);
                await task;
            }
        }

        public async Task AddPlayer(InnerGameModel gameData, int id)
        {
            var dtoToEntities = new DTOToEntities();
            var modelViewToDto = new ModelViewToDTO();
            var playerDto = new UserDTO()
            {
                GameId = id
            };
            modelViewToDto.GetPlayerDto(gameData, playerDto);
             await _userRepository.CreateAsync(dtoToEntities.GetPlayer(playerDto));


        }

        public int AddGame(InnerGameModel gamedata)
        {

            var gameDto = new GameDTO();
            var modelViewToDto = new ModelViewToDTO();
            var dtoToEntities = new DTOToEntities();
            modelViewToDto.GetGameDto(gamedata, gameDto);
            var game = dtoToEntities.GetGame(gameDto);
            int id = _gameRepository.CreateAndKnowId(game);
            var historyGame = new History()
            {
                LogDateTime = DateTime.Now,
                GameId = id
            };
            _historyRepository.Create(historyGame);
            return id;
        }

        public async Task AddDealer(InnerGameModel gameData, int id)
        {
            var dealerDto = new UserDTO()
            {
                GameId = id
            };
            var modelViewToDto = new ModelViewToDTO();
            var dtoToEntities = new DTOToEntities();
            dealerDto = modelViewToDto.GetDealerDto(gameData, dealerDto);
            await _userRepository.CreateAsync(dtoToEntities.GetDealer(dealerDto));
        }
    }
}
