using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using BlackJack.BLL.Interfaces;
using BlackJack.BLL.Services;
using ViewModel.StartGame;
using RedirectResult = System.Web.Http.Results.RedirectResult;

namespace BlackJack.api
{
    [System.Web.Http.RoutePrefix("api/Game")]
    public class HomeApiController : ApiController
    {
       
    }
}
