using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.DTO;
using BlackJackDAL.Entities;

namespace BlackJack.BLL.Mapper
{
    public class DTOToEntities
    {
        public Game GetGame(GameDTO gameDTO)
        {
            Game game = new Game()
            {
                NumberOfPlayers = gameDTO.NumberOfPlayers
            };
            return game;
        }

        public User GetBot(UserDTO userDto)
        {
            User user = new User()
            {
                Name = userDto.Name,
                TypeId = userDto.TypeId,
                GameId = userDto.GameId
            };
            return user;
            }

        public User GetPlayer(UserDTO userDto)
        {
            User user = new User()
            {
                Name = userDto.Name,
                TypeId = userDto.TypeId,
                GameId = userDto.GameId
            };
            return user;
        }

        public User GetDealer(UserDTO userDto)
        {
            User user = new User()
            {
                Name = userDto.Name,
                TypeId = userDto.TypeId,
                GameId = userDto.GameId
            };
            return user;
        }
    }
}
