using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Script.Serialization;
using BlackJack.BLL.Interfaces;
using Newtonsoft.Json;
using ViewModel.Round;
using ViewModel.StartGame;

namespace BlackJack.api
{
    [RoutePrefix("api/Game")]
    public class GameApiController : ApiController
    {
        private readonly IRoundService _roundService;

        public GameApiController(IRoundService _roundService)
        {
            this._roundService = _roundService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<string> GetJsonResult(int id)
        {
            InnerGameModel gameModel = await _roundService.GetGameInfo(id);
//            var gameModelJson = new JavaScriptSerializer().Serialize(model);
            return JsonConvert.SerializeObject(gameModel);
        }

        [HttpGet]
        [Route("CreateRound")]
        public async Task<string> CreateRound(int gameId)
        {
            var round = await _roundService.CreateRound(gameId);
            return JsonConvert.SerializeObject(round);
        }

        [Route("GetCards")]
        public async Task<string> GetCardsForPlayers(int idGame, string nameUser, int roundId)
        {
            UserCardsModelView userCards =  await _roundService.GetCardsForStartGame(idGame, nameUser, roundId);
            return JsonConvert.SerializeObject(userCards);
        }

        [HttpGet]
        [Route("OneMore")]
        public async Task<string> OneMoreCard(int gameId, string userName, int roundId)
        {
            UserViewModel player = _roundService.GetUser(gameId, userName);
            int idCard = await _roundService.GetCard(player, roundId);
            return JsonConvert.SerializeObject(idCard);
        }

        [HttpPost]
        [Route("FinishRound")]
        public async Task<string> FinishRound(InnerRoundViewModel model)
        {
            WinnerModelView winner = await _roundService.LearnTheWinner(model);
            return JsonConvert.SerializeObject(winner);
        }

        [HttpGet]
        [Route("OneMoreBots")]
        public async Task<HttpResponseMessage> OneMoreCardBots(int gameId, string userName, int roundId)
        {
            UserViewModel user = _roundService.GetUser(gameId, userName);
            await _roundService.GetCardsForBots(user, roundId);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}