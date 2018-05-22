using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private readonly IGenericRepository<User> _userRepository;
        public CreateGameService()
        {
            _gameRepository = new GameRepository(_connectionString);
            _userRepository = new UserRepository(_connectionString);
        }


        public async Task AddBots(InnerGameModel gameData)
        {
            var DtoToEntities = new DTOToEntities();
            for (int i = 0; i < gameData.NameOfBots.Length; i++)
            {
                UserDTO bot = new UserDTO()
                {
                    TypeId = 2
                };
                var modelViewToDto = new ModelViewToDTO();
                bot = modelViewToDto.GetBotDto(gameData.NameOfBots[i], bot);
                var botEntity = DtoToEntities.GetBot(bot);
                await _userRepository.CreateAsync(botEntity);
            }
        }

        public async Task AddPlayer(InnerGameModel gameData)
        {
            var dtoToEntities = new DTOToEntities();
            var modelViewToDto = new ModelViewToDTO();
            var playerDto = new UserDTO();
            modelViewToDto.GetPlayerDto(gameData, playerDto);
            await _userRepository.CreateAsync(dtoToEntities.GetPlayer(playerDto));


        }

        public int AddGame(InnerGameModel gamedata)
        {
            try
            {
                var gameDto = new GameDTO();
                var modelViewToDto = new ModelViewToDTO();
                var dtoToEntities = new DTOToEntities();
                modelViewToDto.GetGameDto(gamedata, gameDto);
                int id  = _gameRepository.CreateAndKnowId(dtoToEntities.GetGame(gameDto));
                return id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }

        public async Task AddDealer(InnerGameModel gameData)
        {
            var dealerDto = new UserDTO();
            var modelViewToDto = new ModelViewToDTO();
            var dtoToEntities = new DTOToEntities();
            dealerDto=modelViewToDto.GetDealerDto(gameData, dealerDto);
            await _userRepository.CreateAsync(dtoToEntities.GetDealer(dealerDto));
        }
    }
}
