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
            Session["Model"] = new ModelGameLife(100,50);
            Session["Object"] = new object();
            return View(Session["Model"]);
        }
        /// <summary>
        /// Update next step
        /// </summary>
        /// <returns>JSON data or View</returns>
        public ActionResult UpdateGameModel()
        {
            if ((Session["Model"]) != null)
            {
                lock (Session["Object"])
                {
                    ((ModelGameLife)Session["Model"]).Update();
                }
                if (Request.IsAjaxRequest())
                {
                    return Json(Session["Model"]);
                }
                return View("Index",Session["Model"]);
            }
            return null;
        }
        public ActionResult PreapreGameModel()
        {
            if ((Session["Model"]) != null)
            {
                lock (Session["Object"])
                {
                    ((ModelGameLife)Session["Model"]).Prepare();
                }
                if (Request.IsAjaxRequest())
                {
                    return Json(Session["Model"]);
                }
                return View("Index", Session["Model"]);
            }
            return null;
        }
    }
}
