using BellaNapoli.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BellaNapoli.Controllers
{
    [Authorize]
    public class ProdottiController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Prodotti
        public ActionResult Index()
        {
            return View(db.Prodotti.ToList());
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProdotto,Nome,Foto,Foto2,Foto3,Prezzo,Consegna,Ingredienti")] Prodotti prodotti, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Img"), fileName);
                    if (!Directory.Exists(Server.MapPath("~/Content/Img")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/Img"));
                    }
                    file.SaveAs(path);
                    prodotti.Foto = "/Content/Img/" + fileName;
                }
                else
                {
                    prodotti.Foto = "/Content/Img/Default.jpg";
                }

                if (ModelState.IsValid)
                {
                    db.Prodotti.Add(prodotti);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Errore durante il salvataggio del file: " + ex.Message);
            }
            return View(prodotti);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "idProdotto,Nome,Foto,Foto2,Foto3,Prezzo,Consegna,Ingredienti,File")] Prodotti prodotti, HttpPostedFileBase File)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (File != null && File.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(File.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/Img"), fileName);
                        if (!Directory.Exists(Server.MapPath("~/Content/Img")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/Img"));
                        }
                        File.SaveAs(path);

                        prodotti.Foto = "/Content/Img/" + fileName;
                    }

                    db.Entry(prodotti).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["CreateMess"] = "Prodotto modificato correttamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return View(prodotti);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodotti prodotti = db.Prodotti.Find(id);
            db.Prodotti.Remove(prodotti);
            db.SaveChanges();
            TempData["CreateMess"] = "Prodotto eliminato correttamente";
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

        public ActionResult AddToCart(int id, int quantita)
        {
            var prodotto = db.Prodotti.Find(id);
            if (prodotto != null)
            {
                var cart = Session["cart"] as List<Prodotti> ?? new List<Prodotti>();
                prodotto.Quantita = quantita;
                if (cart.Any(p => p.idProdotto == id))
                {
                    var product = cart.FirstOrDefault(p => p.idProdotto == id);
                    product.Quantita += quantita;
                }
                else
                    cart.Add(prodotto);
                Session["cart"] = cart;
                TempData["CreateMess"] = "Prodotto aggiunto al carrello";
            }
            return RedirectToAction("Index");
        }
    }
}

