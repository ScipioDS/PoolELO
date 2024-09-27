using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PoolPlayersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PoolPlayers
        public ActionResult Index()
        {
            List<PoolPlayer> list = db.PoolPlayers.ToList();
            list.Sort();
            return View(list);
        }

        // GET: PoolPlayers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoolPlayer poolPlayer = db.PoolPlayers.Find(id);
            if (poolPlayer == null)
            {
                return HttpNotFound();
            }

            List<Game> Games = db.Games.ToList();
            List<Game> PlayerGames = new List<Game>();
            foreach (var item in Games)
            {
                if(poolPlayer.Id.Equals(item.Player1_id) || poolPlayer.Id.Equals(item.Player2_id))
                {
                    PlayerGames.Add(item);
                }
            }
            if(PlayerGames.Count < 5)
            {
                ViewBag.Games = PlayerGames;
            }
            else
            {
                List<Game> Last5 = new List<Game>();
                for (int i = 0; i<5; i++)
                {
                    Last5.Add(PlayerGames[PlayerGames.Count - 1 -  i]);
                }
                ViewBag.Games = Last5;
            }
            
            return View(poolPlayer);
        }

        // GET: PoolPlayers/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            PoolPlayer poolPlayer = new PoolPlayer();
            poolPlayer.Elo = 800;
            poolPlayer.Age = 18;
            return View(poolPlayer);
        }

        // POST: PoolPlayers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Age,Elo,Img,Description")] PoolPlayer poolPlayer)
        {
            if (ModelState.IsValid)
            {
                db.PoolPlayers.Add(poolPlayer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(poolPlayer);
        }
        [Authorize(Roles = "Admin,Editor")]
        // GET: PoolPlayers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoolPlayer poolPlayer = db.PoolPlayers.Find(id);
            if (poolPlayer == null)
            {
                return HttpNotFound();
            }
            return View(poolPlayer);
        }

        // POST: PoolPlayers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,Elo,Img,Description")] PoolPlayer poolPlayer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poolPlayer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(poolPlayer);
        }
        [Authorize(Roles = "Admin")]
        // GET: PoolPlayers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoolPlayer poolPlayer = db.PoolPlayers.Find(id);
            if (poolPlayer == null)
            {
                return HttpNotFound();
            }
            return View(poolPlayer);
        }

        // POST: PoolPlayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PoolPlayer poolPlayer = db.PoolPlayers.Find(id);
            db.PoolPlayers.Remove(poolPlayer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AllGames(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoolPlayer poolPlayer = db.PoolPlayers.Find(id);
            if (poolPlayer == null)
            {
                return HttpNotFound();
            }

            List<Game> Games = db.Games.ToList();
            List<Game> PlayerGames = new List<Game>();
            foreach (var item in Games)
            {
                if (poolPlayer.Id.Equals(item.Player1_id) || poolPlayer.Id.Equals(item.Player2_id))
                {
                    PlayerGames.Add(item);
                }
            }

            ViewBag.Games = PlayerGames;

            return View(poolPlayer);
        }

        public ActionResult Search()
        {
            SearchPlayerModel spm = new SearchPlayerModel();
            return View(spm);
        }
        [HttpPost]
        public ActionResult Search(SearchPlayerModel searchPlayerModel)
        {
            List<PoolPlayer> Players = db.PoolPlayers.ToList();
            var filtered = Players.Where(p => p.Name.ToLower().Contains(searchPlayerModel.Name.ToLower())).ToList();
            FilteredPlayersModel model = new FilteredPlayersModel(filtered);
            return View("Searched", model);
        }
    }
}
