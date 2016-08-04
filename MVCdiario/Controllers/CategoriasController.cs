using MVCdiario.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCdiario.Controllers
{
    public class CategoriasController : Controller
    {
        private BDdiario db = new BDdiario();

        public ActionResult Categorias()
        {
            return View(db.categorias.ToList());
        }

        public ActionResult NuevaCategoria()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuevaCategoria(categorias categoria)
        {
            if (ModelState.IsValid)
            {
                db.categorias.Add(categoria);
                db.SaveChanges();
                return RedirectToAction("Categorias");
            }

            return View();
        }

        public ActionResult EditarCategoria(int id)
        {
            categorias categoria = db.categorias.Find(id);
            return View(categoria);
        }

        [HttpPost]
        public ActionResult EditarCategoria(categorias categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Categorias");
            }
            return View(categoria);
        }

        public ActionResult EliminarCategoria(int id)
        {
            categorias categoria = db.categorias.Find(id);
            db.categorias.Remove(categoria);
            db.SaveChanges();
            return RedirectToAction("Categorias");

        }

    }
}
