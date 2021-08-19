using System;
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

        // GET: Tables
        public ActionResult Index()
        {
            return View(db.tables.ToList());
        }

        // GET: Tables/Details/5
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


        public ActionResult Create()
        {
            Table table = new Table();
            table.brojNaGosti = -1;
            table.sum = 0;
            db.tables.Add(table);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


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
                        Debug.WriteLine("Naracka : " + sb.ToString());
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table);
        }

        // GET: Tables/Delete/5
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

        // POST: Tables/Delete/5
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

        // POST: Tables/Delete/5
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
