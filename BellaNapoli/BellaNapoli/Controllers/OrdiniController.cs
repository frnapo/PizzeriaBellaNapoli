using BellaNapoli.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BellaNapoli.Controllers
{
    [Authorize]
    public class OrdiniController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // Index per l'admin per la vista degli ordini
        // Ordina per data decrescente in base alla data
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var ordini = db.Ordini.Include(o => o.Utenti).OrderByDescending(o => o.DataOrdine);
            return View(ordini.ToList());
        }
        [Authorize]
        // Restituisce la vista parziale con i dettagli dell'ordine

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
        //Action per settare isEvaso a true
        //Solo l'admin può accedere a questa action
        [Authorize(Roles = "Admin")]
        public ActionResult isEvaso(int id)
        {
            Ordini ordine = db.Ordini.Find(id);
            ordine.isEvaso = true;
            db.Entry(ordine).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // Action per resettare isEvaso, quindi a false
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


        //Action per la vista degli ordini dell'utente
        // Prende gli ordini confrontando Email con l'utente loggato
        // Ordina per data decrescente in base alla data
        public ActionResult OrdiniUtente()
        {
            var userId = db.Utenti.FirstOrDefault(u => u.Email == User.Identity.Name).idUtente;
            var ordini = db.Ordini.Include(o => o.Utenti)
                .Include(o => o.Dettagli)
                .Where(o => o.FK_idUtente == userId)
                .OrderByDescending(o => o.DataOrdine);
            return View(ordini.ToList());
        }


        // Action asincrona per il totale degli ordini evasi
        public async Task<ActionResult> GetNumeroOrdini()
        {
            int totale = await db.Ordini.Where(o => o.isEvaso == true).CountAsync();
            return Json(totale, JsonRequestBehavior.AllowGet);
        }

        // Action asincrona l'incasso in una data specifica
        public async Task<ActionResult> IncassatoPerGiorno(DateTime data)
        {
            decimal incasso = await db.Ordini
                .Where(o => o.DataOrdine.Year == data.Year && o.DataOrdine.Month == data.Month && o.DataOrdine.Day == data.Day)
                .SumAsync(o => o.Totale);
            return Json(incasso, JsonRequestBehavior.AllowGet);
        }
    }
}