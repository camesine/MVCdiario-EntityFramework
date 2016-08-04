using MVCdiario.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCdiario.Controllers
{
    public class ArticulosController : Controller
    {
        private BDdiario db = new BDdiario();


        public ActionResult Articulos()
        {
            return View(db.articulos.ToList());
        }

        public ActionResult NuevoArticulo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuevoArticulo(articulos articulo)
        {
            if (ModelState.IsValid)
            {
                db.articulos.Add(articulo);
                db.SaveChanges();
                return RedirectToAction("Articulos");
            }
            return View();
        }

        public ActionResult EditarArticulo(int id)
        {
            articulos articulo = db.articulos.Find(id);
            return View(articulo);
        }



        [HttpPost]
        public ActionResult EditarArticulo(articulos articulo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articulo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Articulos");
            }
            return View(articulo);
        }

        public ActionResult EliminarArticulo(int id)
        {
            articulos articulo = db.articulos.Find(id);
            db.articulos.Remove(articulo);
            db.SaveChanges();
            return RedirectToAction("Articulos");
        }


    }
}
