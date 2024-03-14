using BellaNapoli.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BellaNapoli.Controllers
{
    [Authorize]
    public class UtentiController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // prendo tutti gli utenti tranne l'utente loggato
        // per evitare che l'utente si tolga l'admin da solo
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var userId = db.Utenti.FirstOrDefault(u => u.Email == User.Identity.Name).idUtente;
            var users = db.Utenti.Where(u => u.idUtente != userId).ToList();
            return View(users);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeAdmin(int? id)
        {
            var user = db.Utenti.Find(id);
            user.isAdmin = true;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveAdmin(int? id)
        {
            var user = db.Utenti.Find(id);
            user.isAdmin = false;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
