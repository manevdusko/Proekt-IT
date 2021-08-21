using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Proekt_IT.Models;

namespace Proekt_IT.Controllers
{
    public class TablesController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        [Authorize]
        public ActionResult Index()
        {
            try
            {
                if(db.vraboteni.Find(User.Identity.Name) != null)
                Debug.WriteLine(db.vraboteni.Find(User.Identity.Name).promet);
            }
            catch { }
                return View(db.tables.ToList());
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.tables.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        [Authorize]
        public ActionResult Create()
        {
            Table table = new Table();
            table.brojNaGosti = -1;
            table.sum = 0;
            db.tables.Add(table);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Plata()
        {
            List<Vraboten> vraboteni = db.vraboteni.ToList();
            
            if (Request.Form["saatnina"] != null && Request.Form["saatnina"] != "")
            {
                foreach(Vraboten v in vraboteni)
                {
                    Debug.WriteLine(v.raboteno.Seconds + " " + v.raboteno.Minutes);
                    double minutes = v.raboteno.Minutes / 60.0;
                    Debug.WriteLine(minutes);
                    double saati = v.raboteno.Days * 24.0 + v.raboteno.Minutes / 60.0 + v.raboteno.Seconds / 3600.0;
                    Debug.WriteLine(saati);
                    v.plata = saati * (double)Int32.Parse(Request.Form["saatnina"]);
                    Debug.WriteLine("PLATA " + v.plata);
                }
            }
                return View(vraboteni);
        }

        [Authorize]
        public ActionResult Naracaj(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.tables.Find(id);

            Naracka naracka = new Naracka();

            if (table == null)
            {
                return HttpNotFound();
            }

            if (table.brojNaGosti == -1)
            {
                table.brojNaGosti = 0;
                table.sum = 0;
            }

            naracka.table = table;
            naracka.menu = db.menu.ToList();
           
            return View(naracka);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Naracaj([Bind(Include = "id,brojNaGosti,sum")] Table table)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(table).State = EntityState.Modified;
                Table t1 = db.tables.Find(table.id);
                if(!(Request.Form["brojNaGosti"] == "" || Request.Form["brojNaGosti"] == null))
                {
                    t1.brojNaGosti = Int32.Parse(Request.Form["brojNaGosti"]);
                }
                if (!(Request.Form["naracka"] == "" || Request.Form["naracka"] == null)) {
                    foreach (string id in Request.Form["naracka"].Split(',').ToList())
                    {
                        StringBuilder sb = new StringBuilder(t1.orders);
                        sb.Append(db.menu.Find(Int32.Parse(id)).imeNaJadenje + ", ");
                        t1.sum += db.menu.Find(Int32.Parse(id)).cena;
                        t1.orders = sb.ToString();

                        if (db.vraboteni.FirstOrDefault(i => i.email.CompareTo(User.Identity.Name) == 0) == null)
                        {
                            db.vraboteni.Add(new Vraboten(User.Identity.Name, DateTime.UtcNow.TimeOfDay, DateTime.UtcNow.TimeOfDay, new TimeSpan(0,0,0,0), db.menu.Find(Int32.Parse(id)).cena));
                        }
                        else
                        {
                            db.vraboteni.FirstOrDefault(i => i.email.CompareTo(User.Identity.Name) == 0).dodajPromet(db.menu.Find(Int32.Parse(id)).cena);
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table);
        }

        [Authorize]
        public ActionResult Smetka(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.tables.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        [Authorize]
        [HttpPost, ActionName("Smetka")]
        [ValidateAntiForgeryToken]
        public ActionResult SmetkaConfirmed(int id)
        {
            Table table = db.tables.Find(id);
            table.orders = "";
            table.brojNaGosti = -1;
            table.sum = 0;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.tables.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            return View(table);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table table = db.tables.Find(id);
            db.tables.Remove(table);
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
