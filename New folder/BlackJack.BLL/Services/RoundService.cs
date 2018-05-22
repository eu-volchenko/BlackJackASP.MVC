using System;
using System.Collections.Generic;
using System.Linq;
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
    class RoundService:IRoundService
    {
        private readonly string _connectionString = System.Configuration.ConfigurationManager.
            ConnectionStrings["ContextDB"].ConnectionString;
        private readonly GameRepository _gameRepository;
        private readonly IGenericRepository<User> _userRepository;

        public RoundService()
        {
            _gameRepository  = new GameRepository(_connectionString);
            _userRepository = new UserRepository(_connectionString);
        }

        public InnerGameModel GetGameModel(int id)
        {
            //EntitiesToDTO entitiesToDto = new EntitiesToDTO();
            //Task<Game> game = _gameRepository.GetAsync(x => x.Id == id);
            //GameDTO gameDto = entitiesToDto.GameToGameDto(game.GetAwaiter().GetResult());
            return new InnerGameModel();
        }
    }
}
