using BellaNapoli.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace BellaNapoli.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        // metodi per la registrazione e il login
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Utenti utente)
        {
            using (var context = new ModelDbContext())
            {
                var existingUser = context.Utenti.FirstOrDefault(u => u.Email == utente.Email);
                if (existingUser != null)
                {
                    TempData["RegFail"] = "Utente già registrato";
                    return View();
                }

                context.Utenti.Add(utente);
                context.SaveChanges();
            }
            TempData["RegMess"] = "Registrazione effettuata con successo";
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string psw)
        {
            using (var context = new ModelDbContext())
            {
                var user = context.Utenti.FirstOrDefault(u => u.Email == email && u.Psw == psw);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(email, false);
                    TempData["LoginMess"] = "Login effettuato con successo";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["LoginFail"] = "Email o password errati";
                    return View();
                }
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData["LoginMess"] = "Sei stato disconesso";
            return RedirectToAction("Index", "Home");
        }

    }
}
