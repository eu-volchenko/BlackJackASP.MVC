using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace BlackJack.Controllers
{
    public class GameController : Controller
    {
        
        public ActionResult Game()
        {
            return View();
       }
    }
}
