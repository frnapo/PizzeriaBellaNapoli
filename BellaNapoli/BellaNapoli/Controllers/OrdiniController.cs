using BellaNapoli.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BellaNapoli.Controllers
{
    [Authorize]
    public class OrdiniController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Ordini
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var ordini = db.Ordini.Include(o => o.Utenti).OrderByDescending(o => o.DataOrdine);
            return View(ordini.ToList());
        }
        [Authorize]
        public PartialViewResult Details(int? id)
        {
            if (id == null)
            {
                return PartialView();
            }
            else
            {
                var Dettagli = db.Dettagli.Include(o => o.Ordini)
                    .Include(o => o.Prodotti)
                    .Where(o => o.FK_idOrdine == id);

                return PartialView("_Details", Dettagli.ToList());
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult isEvaso(int id)
        {
            Ordini ordine = db.Ordini.Find(id);
            ordine.isEvaso = true;
            db.Entry(ordine).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult noEvaso(int id)
        {
            Ordini ordine = db.Ordini.Find(id);
            ordine.isEvaso = false;
            db.Entry(ordine).State = EntityState.Modified;
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


        public ActionResult OrdiniUtente()
        {
            var userId = db.Utenti.FirstOrDefault(u => u.Email == User.Identity.Name).idUtente;
            var ordini = db.Ordini.Include(o => o.Utenti)
                .Include(o => o.Dettagli)
                .Where(o => o.FK_idUtente == userId)
                .OrderByDescending(o => o.DataOrdine);
            return View(ordini.ToList());
        }
    }
}
