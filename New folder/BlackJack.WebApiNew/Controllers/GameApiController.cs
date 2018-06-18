using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using BlackJack.BLL.Interfaces;
using BlackJack.Utility.Utilities;
using Newtonsoft.Json;
using ViewModel.CreateGameViewModels;
using ViewModel.Round;

namespace BlackJack.WebApiNew.Controllers
{
    [RoutePrefix("api/Game")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GameApiController : ApiController
    {

        private readonly IRoundService _roundService;

        public GameApiController(IRoundService roundService)
        {
            this._roundService = roundService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<string> GetJsonResult(int id)
        {
            try
            {
                InnerGameViewModel gameModel = await _roundService.GetGameInfo(id);
                return JsonConvert.SerializeObject(gameModel);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "GameApiController");
                throw;
            }
        }

        [HttpGet]
        [Route("CreateRound")]
        public async Task<string> CreateRound(int gameId)
        {
            try
            {
                var round = await _roundService.CreateRound(gameId);
                return JsonConvert.SerializeObject(round);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "GameApiController");
                throw;
            }
           
        }

        [Route("GetCards")]
        public async Task<string> GetCardsForPlayers(int idGame, string nameUser, int roundId)
        {
            try
            {
                var userCards = await _roundService.GetCardsForStartGame(idGame, nameUser, roundId);
                return JsonConvert.SerializeObject(userCards);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "GameApiController");
                throw;
            }
        }

        [HttpGet]
        [Route("OneMore")]
        public async Task<string> OneMoreCard(int gameId, string userName, int roundId)
        {
            try
            {
                var player = _roundService.GetUser(gameId, userName);
                int idCard = await _roundService.GetCard(player, roundId);
                return JsonConvert.SerializeObject(idCard);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "GameApiController");
                throw;
            }
            
        }

        [HttpPost]
        [Route("FinishRound")]
        public async Task<string> FinishRound(InnerRoundViewModel model)
        {
            try
            {
                var winner = await _roundService.LearnTheWinner(model);
                return JsonConvert.SerializeObject(winner);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "GameApiController");
                throw;
            }     
        }

        [HttpGet]
        [Route("OneMoreBots")]
        public async Task<HttpResponseMessage> OneMoreCardBots(int gameId, string userName, int roundId)
        {
            try
            {
                var user = _roundService.GetUser(gameId, userName);
                await _roundService.GetCardsForBots(user, roundId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "GameApiController");
                throw;
            }
           
        }
    }
}
