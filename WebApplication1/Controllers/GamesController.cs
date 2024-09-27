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
    [Authorize(Roles = "Admin,Editor")]
    public class GamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Games
        public ActionResult Index()
        {
            return View(db.Games.ToList());
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.Players = db.PoolPlayers.ToList();
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Player1_id,Player2_id,Winner")] Game game)
        {
            if (ModelState.IsValid)
            {
                if (game.Player1_id.Equals(game.Player2_id))
                {
                    return RedirectToAction("Index", "Home");
                }
                PoolPlayer player1 = db.PoolPlayers.Find(game.Player1_id);
                PoolPlayer player2 = db.PoolPlayers.Find(game.Player2_id);

                game.Player1_name = player1.Name;
                game.Player2_name = player2.Name;

                if (game.Winner)
                {
                    int Elo = player1.Elo;
                    player1.updatePlayerELO(true, player2.Elo);
                    player2.updatePlayerELO(false, Elo);
                }
                else
                {
                    int Elo = player1.Elo;
                    player1.updatePlayerELO(false, player2.Elo);
                    player2.updatePlayerELO(true, Elo);
                }

                db.Games.Add(game);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Player1_id,Player2_id,Winner")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
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
        // GET: Games/Create
        public ActionResult CreateVia(int? id)
        {
            
            ViewBag.Players = db.PoolPlayers.ToList();
            Game game = new Game();
            game.Player1_id = (int)id;
            return View(game);
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateVia([Bind(Include = "Id,Player1_id,Player2_id,Winner")] Game game)
        {
            if (ModelState.IsValid)
            {
                if (game.Player1_id.Equals(game.Player2_id))
                {
                    return RedirectToAction("Index", "Home");
                }

                PoolPlayer player1 = db.PoolPlayers.Find(game.Player1_id);
                PoolPlayer player2 = db.PoolPlayers.Find(game.Player2_id);

                game.Player1_name = player1.Name;
                game.Player2_name = player2.Name;

                if (game.Winner)
                {
                    int Elo = player1.Elo;
                    player1.updatePlayerELO(true, player2.Elo);
                    player2.updatePlayerELO(false, Elo);
                }
                else
                {
                    int Elo = player1.Elo;
                    player1.updatePlayerELO(false, player2.Elo);
                    player2.updatePlayerELO(true, Elo);
                }
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(game);
        }
    }

}
