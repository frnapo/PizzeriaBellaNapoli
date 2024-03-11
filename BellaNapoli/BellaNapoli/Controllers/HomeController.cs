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
                context.Utenti.Add(utente);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
        }

    }
}
