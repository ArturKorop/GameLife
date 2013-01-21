using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.GameLife;

namespace WebUI.Controllers
{
    public class MainController : Controller
    {
        public  ModelGameLife Model = new ModelGameLife();
        public ActionResult Index()
        {
            
            return View(Model);
        }

    }
}
