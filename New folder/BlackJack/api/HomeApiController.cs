using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using BlackJack.BLL.Interfaces;
using BlackJack.BLL.Services;
using ViewModel.StartGame;
using RedirectResult = System.Web.Http.Results.RedirectResult;

namespace BlackJack.api
{
    [System.Web.Http.RoutePrefix("api/Game")]
    public class HomeApiController : ApiController
    {
        private readonly ICreateGameService _createGameService;



        public HomeApiController() : base()
        {
            _createGameService = new CreateGameService();
        }

        [System.Web.Http.Route("Create")]
        public async Task<HttpResponseMessage> Post([FromBody] InnerGameModel gameModel)
        {
            int id = _createGameService.AddGame(gameModel);
            var taskAddBots = _createGameService.AddBots(gameModel);
            var taskaAddDealer = _createGameService.AddDealer(gameModel);
            var taskAddPlayer = _createGameService.AddPlayer(gameModel);
            await Task.WhenAll(taskAddBots, taskAddPlayer, taskaAddDealer);
            string url = "http://localhost:50220//Game/Game?id=" + id;
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }
    }
}
