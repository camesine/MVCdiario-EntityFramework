using MVCdiario.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCdiario.Controllers
{
    public class ImagenesController : Controller
    {
        private BDdiario db = new BDdiario();
        //
        // GET: /Imagenes/

        public ActionResult Imagenes()
        {
            
            return View(db.imagenes.ToList());
        }

        public ActionResult NuevaImagen()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NuevaImagen(imagenes imagen, HttpPostedFileBase archivo)
        {
            String Nombre = archivo.FileName;
            String path = Path.Combine(Server.MapPath("~/Imagenes"), Nombre);
            archivo.SaveAs(path);

            imagen.nombre = Nombre;
            db.imagenes.Add(imagen);
            db.SaveChanges();

            return RedirectToAction("Imagenes");
        }

        public ActionResult EditarImagen(int id)
        {
            imagenes imagen = db.imagenes.Find(id);
            return View(imagen);
        }

        [HttpPost]
        public ActionResult EditarImagen(imagenes imagen,HttpPostedFileBase archivo)
        {
            if (ModelState.IsValid)
            {

                var img = db.imagenes.Find(imagen.id_imagen);
                String ruta = Path.Combine(Server.MapPath("/Imagenes/"), img.nombre);
                System.IO.File.Delete(ruta);

                String Nombre = archivo.FileName;
                String path = Path.Combine(Server.MapPath("~/Imagenes"), Nombre);
                archivo.SaveAs(path);


                img.id_imagen = imagen.id_imagen;
                img.nombre = Nombre;
                img.id_noticia = imagen.id_noticia;

                db.Entry(img).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Imagenes");
        }

        public ActionResult EliminarImagen(int id)
        {
            imagenes img = db.imagenes.Find(id);
            String ruta = Path.Combine(Server.MapPath("/Imagenes/"), img.nombre);
            System.IO.File.Delete(ruta);

            db.imagenes.Remove(img);
            db.SaveChanges();
            return RedirectToAction("Imagenes");
        }

    }
}
