using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using BlackJack.BLL.Interfaces;
using BlackJack.Utility.Utilities;
using Newtonsoft.Json;
using ViewModel.History;
using ViewModel.HistoryViewModels;

namespace BlackJack.WebApiNew.Controllers
{
    [RoutePrefix("api/History")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HistoryApiController : ApiController
    {
        private readonly IHistoryService _historyService;

        public HistoryApiController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        [Route("GetGames")]
        public async Task<string> GetAllGames()
        {
            try
            {
                List<GameHistoriesModelView> innerGameModels = await _historyService.GetGames();
                return JsonConvert.SerializeObject(innerGameModels);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "HistoryApiController");
                throw;
            }
           
        }


        [HttpGet]
        [Route("GetRounds")]
        public string GetRounds(int gameId)
        {
            try
            {
                var rounds = _historyService.GetRounds(gameId);
                return JsonConvert.SerializeObject(rounds);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "HistoryApiController");
                throw;
            }
        
        }


        [HttpGet]
        [Route("GetPlayers")]
        public string GetPlayers(int gameId)
        {
            try
            {
                var playersId = _historyService.GetPlayers(gameId);
                return JsonConvert.SerializeObject(playersId);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "HistoryApiController");
                throw;
            }
            
        }

        [HttpGet]
        [Route("GetPlayers")]
        public async Task<string> GetPlayersCards(int roundId, int userId)
        {
            try
            {
                var playersCards = await _historyService.GetPlayersCards(roundId, userId);
                return JsonConvert.SerializeObject(playersCards);
            }
            catch (Exception e)
            {
                LogWriter.WriteLog(e.Message, "HistoryApiController");
                throw;
            }
            
        }
    }
}