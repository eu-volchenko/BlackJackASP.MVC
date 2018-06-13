using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using BlackJack.BLL.Interfaces;
using ViewModel.StartGame;

namespace BlackJack.WebApiNew.Controllers
{
    public class CreateGameApiController : ApiController
    {
        [RoutePrefix("api/CreateGame")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public class CreateGameController : ApiController
        {
            private readonly ICreateGameService _createGameService;

            public CreateGameController(ICreateGameService _createGameService) : base()
            {
                this._createGameService = _createGameService;
            }



            [HttpPost]
            [Route("Create")]
            public async Task<HttpResponseMessage> Create([FromBody] InnerGameViewModel gameModel)
            {
                int id = _createGameService.AddGame(gameModel);
                await _createGameService.AddBots(gameModel, id);
                await _createGameService.AddDealer(gameModel, id);
                await _createGameService.AddPlayer(gameModel, id);


                //await Task.WhenAll(taskAddPlayer, taskaAddDealer);
                string url = "http://localhost:50220/Game/Game?id=" + id;
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
        }
    }
}