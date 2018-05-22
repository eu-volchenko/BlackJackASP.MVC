using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.DTO;
using ViewModel.StartGame;

namespace BlackJack.BLL.Mapper
{
    public class ModelViewToDTO
    {
        public GameDTO GetGameDto(InnerGameModel innerGameModel, GameDTO gameDto)
        {
            gameDto.NumberOfPlayers = innerGameModel.NumberOfBots + 2;
            return gameDto;
        }

        public UserDTO GetBotDto(string name, UserDTO userDto)
        {
            userDto.Name = name;
            return userDto;
        }

        public UserDTO GetPlayerDto(InnerGameModel innerGameModel, UserDTO userDto)
        {
            userDto.TypeId = 3;
            userDto.Name = innerGameModel.PlayerName;
            return userDto;
        }

        public UserDTO GetDealerDto(InnerGameModel innerGameModel, UserDTO userDto)
        {
            userDto.TypeId = 1;
            userDto.Name = innerGameModel.DealerName;
            return userDto;
        }
    }
}
