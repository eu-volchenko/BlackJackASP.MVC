using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using BlackJack.BLL.Interfaces;
using Newtonsoft.Json;
using ViewModel.History;

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
            List<GameHistoriesModelView> innerGameModels = await _historyService.GetGames();
            return JsonConvert.SerializeObject(innerGameModels);
        }


        [HttpGet]
        [Route("GetRounds")]
        public string GetRounds(int gameId)
        {
            var rounds = _historyService.GetRounds(gameId);
            return JsonConvert.SerializeObject(rounds);
        }


        [HttpGet]
        [Route("GetPlayers")]
        public string GetPlayers(int gameId)
        {
            var playersId = _historyService.GetPlayers(gameId);
            return JsonConvert.SerializeObject(playersId);
        }

        [HttpGet]
        [Route("GetPlayers")]
        public async Task<string> GetPlayersCards(int roundId, int userId)
        {
            var playersCards = await _historyService.GetPlayersCards(roundId, userId);
            return JsonConvert.SerializeObject(playersCards);
        }
    }
}