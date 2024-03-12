using BellaNapoli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BellaNapoli.Controllers
{
    public class CarrelloController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Carrello
        public ActionResult Index()
        {
            var cart = Session["cart"] as List<Prodotti>;
            if (cart == null || !cart.Any())
            {
                TempData["CartMessage"] = "Il carrello è vuoto";
                return RedirectToAction("Index", "Prodotti");
            }
            return View(cart);
        }

        public ActionResult Delete(int? id)
        {
            var cart = Session["cart"] as List<Prodotti>;
            if (cart != null)
            {
                var productToRemove = cart.FirstOrDefault(p => p.idProdotto == id);
                if (productToRemove != null)
                {
                    cart.Remove(productToRemove);
                }
            }

            return RedirectToAction("Index");
        }

        //action per pushare i prodotti nel db su Ordini
        [HttpPost]
        public ActionResult Ordina(string note, string indirizzo)
        {
            ModelDbContext db = new ModelDbContext();
            var userId = db.Utenti.FirstOrDefault(u => u.Email == User.Identity.Name).idUtente;

            var cart = Session["cart"] as List<Prodotti>;
            if (cart != null && cart.Any())
            {
                Ordini newOrder = new Ordini();
                newOrder.DataOrdine = DateTime.Now;
                newOrder.isEvaso = false;
                newOrder.FK_idUtente = userId;
                newOrder.Indirizzo = indirizzo;
                newOrder.Totale = cart.Sum(p => p.Prezzo);
                newOrder.Note = note;

                db.Ordini.Add(newOrder);
                db.SaveChanges();

                foreach (var product in cart)
                {
                    Dettagli newDetail = new Dettagli();
                    newDetail.FK_idOrdine = newOrder.idOrdine;
                    newDetail.FK_idProdotto = product.idProdotto;
                    newDetail.Quantita = 1;

                    db.Dettagli.Add(newDetail);
                    db.SaveChanges();
                }
                cart.Clear();
            }

            TempData["CreateMess"] = "L'ordine è stato inviato correttamente";
            return RedirectToAction("Index", "Prodotti");
        }
    }
}
