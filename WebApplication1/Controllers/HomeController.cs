using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<PoolPlayer> Players = db.PoolPlayers.ToList();
            Players.Sort();
            var highest_rated = Players[0];
            var lowest_rated = Players[Players.Count - 1];

            ViewBag.Player = highest_rated;
            ViewBag.LowestPlayer = lowest_rated;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}