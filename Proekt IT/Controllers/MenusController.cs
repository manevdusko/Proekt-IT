using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proekt_IT.Models;

namespace Proekt_IT.Controllers
{
    public class MenusController : Controller
    {
        private RestaurantContext db = null;

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

        public ActionResult AddToCart(int id)
        {
            ShoppingCart sc = new ShoppingCart();
            sc.productId = id;
            db.shoppingCart.Add(sc);
            db.SaveChanges();
            return RedirectToAction("Meni");
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
