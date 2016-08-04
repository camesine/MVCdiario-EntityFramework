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
    public class UsuariosController : Controller
    {
        private BDdiario db = new BDdiario();

        public ActionResult Panel()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View(db.usuarios.ToList());
        }


        public ActionResult NuevoUsuario()
        {
            return View();
        }


        public ActionResult EditarUsuario(string id)
        {
            usuarios usuario = db.usuarios.Find(id);
            return View(usuario);
        }



        [HttpPost]
        public ActionResult EditarUsuario(usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Usuarios");
            }
            return View(usuario);
        }



        [HttpPost]
        public ActionResult NuevoUsuario(usuarios a)
        {
            if (ModelState.IsValid)
            {
                db.usuarios.Add(a);
                db.SaveChanges();
                return RedirectToAction("Usuarios");
            }
            return View();
        }



        public ActionResult EliminarUsuario(string id)
        {

            usuarios usuario = db.usuarios.Find(id);
            db.usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Usuarios");
        }

    }
}