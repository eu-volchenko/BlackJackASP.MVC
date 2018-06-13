using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.BLL.DTO;
using BlackJackDAL.Enums;
using ViewModel.StartGame;

namespace BlackJack.BLL.Mapper
{
    public class ModelViewToDTO
    {
        public GameDTO GetGameDto(InnerGameViewModel innerGameModel, GameDTO gameDto)
        {
            gameDto.NumberOfPlayers = innerGameModel.NumberOfBots + 2;
            return gameDto;
        }

        public UserDTO GetBotDto(string name, UserDTO userDto)
        {
            userDto.Name = name;
            return userDto;
        }

        public UserDTO GetPlayerDto(InnerGameViewModel innerGameModel, UserDTO userDto)
        {
            userDto.TypeId = (int)PlayerTypeEnum.Player;
            userDto.Name = innerGameModel.PlayerName;
            return userDto;
        }

        public UserDTO GetDealerDto(InnerGameViewModel innerGameModel, UserDTO userDto)
        {
            userDto.TypeId = (int)PlayerTypeEnum.Dealer;
            userDto.Name = innerGameModel.DealerName;
            return userDto;
        }
    }
}
