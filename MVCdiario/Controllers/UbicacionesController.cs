using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCdiario.Models;

namespace MVCdiario.Controllers
{
    public class UbicacionesController : Controller
    {
        private BDdiario db = new BDdiario();


        public ActionResult Ubicaciones()
        {
            return View(db.ubicaciones.ToList());
        }



        public ActionResult NuevaUbicacion()
        {
            return View();
        }



        [HttpPost]
        public ActionResult NuevaUbicacion(ubicaciones ubicacion)
        {
            if (ModelState.IsValid)
            {
                db.ubicaciones.Add(ubicacion);
                db.SaveChanges();
                return RedirectToAction("Ubicaciones");
            }
            return View();
        }
    



        public ActionResult EditarUbicacion(int id)
        {
            ubicaciones ubicacion = db.ubicaciones.Find(id);

            return View(ubicacion);

        }



        [HttpPost]
        public ActionResult EditarUbicacion(ubicaciones ubicacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ubicacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Ubicaciones");
            }
            return View(ubicacion);
        }

        
        public ActionResult EliminarUbicacion(int id)
        {
            ubicaciones ubicacion = db.ubicaciones.Find(id);
            db.ubicaciones.Remove(ubicacion);
            db.SaveChanges();
            return RedirectToAction("Ubicaciones");
        }

    }
}