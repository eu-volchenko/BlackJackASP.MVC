using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BlackJack.api
{
    public class GameApiController : ApiController
    {
        [System.Web.Http.Route("GET")]
        public JsonResult GetJsonResult(int id)
        {
            return new JsonResult();
        }
    }
}