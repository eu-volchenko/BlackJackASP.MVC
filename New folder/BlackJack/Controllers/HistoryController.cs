using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlackJack.Controllers
{
    public class HistoryController:Controller
    {
        public ActionResult History()
        {
            return View();
        }
    }
}