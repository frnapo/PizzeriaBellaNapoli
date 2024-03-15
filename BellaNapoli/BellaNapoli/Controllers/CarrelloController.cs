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


        // Mostra il carrello con i prodotti selezionati dall'utente
        // Se il carrello è vuoto, reindirizza alla pagina dei prodotti con un messaggio
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

        // Rimuove un prodotto dal carrello
        // Se la quantità del prodotto è maggiore di 1, decrementa la quantità
        public ActionResult Delete(int? id)
        {
            var cart = Session["cart"] as List<Prodotti>;
            if (cart != null)
            {
                var productToRemove = cart.FirstOrDefault(p => p.idProdotto == id);
                if (productToRemove != null)
                {
                    if (productToRemove.Quantita > 1)
                    {
                        productToRemove.Quantita--;
                    }
                    else
                    {
                        cart.Remove(productToRemove);
                    }
                }
            }

            return RedirectToAction("Index");
        }


        // Action per pushare prodotti nel carrello nel db
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

                // Aggiunge i dettagli dell'ordine
                // Per ogni prodotto nel carrello, crea un dettaglio
                foreach (var product in cart)
                {
                    Dettagli newDetail = new Dettagli();
                    newDetail.FK_idOrdine = newOrder.idOrdine;
                    newDetail.FK_idProdotto = product.idProdotto;
                    newDetail.Quantita = Convert.ToInt32(product.Quantita);

                    db.Dettagli.Add(newDetail);
                    db.SaveChanges();
                }
                cart.Clear();
            }

            TempData["CreateMess"] = "L'ordine è stato inviato correttamente";
            return RedirectToAction("OrdiniUtente", "Ordini");
        }


        // Svuota il carrello
        public ActionResult CartClear()
        {
            var cart = Session["cart"] as List<Prodotti>;
            if (cart != null)
            {
                cart.Clear();
            }
            TempData["CreateMess"] = "Il carrello è stato svuotato";
            return RedirectToAction("Index", "Prodotti");
        }
    }
}
