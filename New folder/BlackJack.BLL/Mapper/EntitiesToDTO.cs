﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.DTO;
using BlackJackDAL.Entities;

namespace BlackJack.BLL.Mapper
{
    public class EntitiesToDTO
    {
        public GameDTO GameToGameDto(Game game)
        {
            GameDTO gameDto = new GameDTO()
            {
                NumberOfPlayers = game.NumberOfPlayers,
                Id = game.Id
            };
            return gameDto;
        }

        public UserDTO UserToUserDto(User user)
        {
            UserDTO userDto = new UserDTO()
            {
                GameId = user.GameId,
                Id = user.Id,
                TypeId = user.TypeId,
                Name = user.Name
            };
            return userDto;
        }
    }
}
