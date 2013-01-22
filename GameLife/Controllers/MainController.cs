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
        /// <summary>
        /// Return start state of game "Life"
        /// </summary>
        /// <returns>View with JSON data</returns>
        public ActionResult Index()
        {
            Session["Model"] = new ModelGameLife(10,10);
            return View(Session["Model"]);
        }
        /// <summary>
        /// Update next step
        /// </summary>
        /// <returns>JSON data</returns>
        public JsonResult UpdateGameModel()
        {
            if ((Session["Model"]) != null)
                ((ModelGameLife)Session["Model"]).Update();
            return Json(Session["Model"]);
        }

    }
}
