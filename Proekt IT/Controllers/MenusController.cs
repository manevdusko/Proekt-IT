using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Aspose.Email.Clients;
using Aspose.Email.Clients.Smtp;
using Proekt_IT.Models;

namespace Proekt_IT.Controllers
{
    public class MenusController : Controller
    {
        private RestaurantContext db = null;

        private string smtpFrom = "dushkomanev@outlook.com";
        private string SMTPhost = "smtp.office365.com";
        private int SMTPport = 587;

        private string username = "dushkomanev@outlook.com";
        private string password = "TestPassword1!";

        public MenusController()
        {
            this.db = new RestaurantContext();
        }

        public ActionResult Index()
        {
            return View(db.menu.ToList());
        }

        public ActionResult ShoppingCart()
        {
            List<Menu> temp = new List<Menu>();
            foreach(ShoppingCart sc in db.shoppingCart.ToList())
            {
                temp.Add(db.menu.Find(sc.productId));
            }
            return View(temp);
        }


        // GET: Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,imeNaJadenje,sostojki,cena,imgUrl")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.menu.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,imeNaJadenje,sostojki,cena,imgUrl")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        public ActionResult Meni()
        {
            return View(db.menu.ToList());
        }

        private void sendMail(string to, string title, string body)
        {
            try
            {
                //nova poraka
                Aspose.Email.MailMessage EmailMessage = new Aspose.Email.MailMessage();

                //popolnuvanje na porakata
                EmailMessage.Subject = title;
                EmailMessage.To = to;
                EmailMessage.Body = body;
                EmailMessage.From = smtpFrom;

                //Inicijalizacija na smtp klient
                SmtpClient SMTPEmailClient = new SmtpClient();

                //postavuvanje na postavkite na smtp
                SMTPEmailClient.Host = SMTPhost;
                SMTPEmailClient.Username = username;
                SMTPEmailClient.Password = password;
                SMTPEmailClient.Port = SMTPport;
                SMTPEmailClient.SecurityOptions = SecurityOptions.SSLExplicit;

                //prakjanje na mailot
                SMTPEmailClient.Send(EmailMessage);
            }
            catch(Exception e)
            {
                Debug.Write(e.Message);
            }
        }

        [Authorize]
        public ActionResult OnlineNaracki()
        {
            return View(db.onlineNaracki.ToList());
        }


        public ActionResult Naracaj()
        {
            Debug.WriteLine(Request.Form["email"]);
            int c = 0;
            StringBuilder sb = new StringBuilder();

            foreach(ShoppingCart m in db.shoppingCart.ToList())
            {
                Menu tmp = db.menu.Find(m.productId);
                c += tmp.cena;
                sb.Append(tmp.imeNaJadenje + ", ");
                db.shoppingCart.Remove(m);
            }
            db.onlineNaracki.Add(new OnlineNaracki(sb.ToString(), Request.Form["address"], c));
            sendMail(Request.Form["email"], "Успешна нарачка од нашиот ресторант", "Вашата нарачка е успешна и ќе биде доставена на адреса " + Request.Form["address"] + ". Имате вкупно: " + c + " денари за наплата а нарачавте: " + sb.ToString());
            db.SaveChanges();
            return RedirectToAction("Index");

        }
            public ActionResult AddToCart(int id)
        {
            ShoppingCart sc = new ShoppingCart();
            sc.productId = id;
            db.shoppingCart.Add(sc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Potvrdi(int? id)
        {
                OnlineNaracki naracka = db.onlineNaracki.Find(id);
                db.onlineNaracki.Remove(naracka);
                db.SaveChanges();
                return RedirectToAction("OnlineNaracki");
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.menu.Find(id);
            db.menu.Remove(menu);
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
    }
}
