using MVCdiario.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCdiario.Controllers
{
    public class NoticiasController : Controller
    {
        private BDdiario db = new BDdiario();

        public ActionResult Reciente()
        {
            
            IEnumerable<noticias> n = from noticias in db.noticias orderby noticias.id_noticia descending select noticias;
            
                return View(n);
        }

        public ActionResult Acceder()
        {
            ViewBag.titulo = "ACCEDER";
            return View();
        }

        [HttpPost]
        public ActionResult Acceder(string correo, string contrasena)
        {
            IEnumerable<usuarios> usuario = from usuarios in db.usuarios where usuarios.correo == correo && usuarios.contrasena == contrasena select usuarios;

            if (usuario.Count() > 0)
            {
                return RedirectToAction("Panel","Usuarios");
            }
            ViewBag.titulo = "ERROR AL INICIAR SESION";
            return View();
        }

        public ActionResult Noticia(int id)
        {
            noticias noticia = db.noticias.Find(id);
            return View(noticia);
        }

        public ActionResult Nosotros()
        {                               
            return View();
        }

        public ActionResult Noticias()
        {
            return View(db.noticias.ToList());
        }

        public ActionResult NuevaNoticia()
        {
            return View();
        }


        [HttpPost]
        public ActionResult NuevaNoticia(noticias noticia,HttpPostedFileBase imagen)
        {
            db.noticias.Add(noticia);
            db.SaveChanges();

            var max = db.noticias.Select(noticias => noticia.id_noticia).Max();

            String Nombre = imagen.FileName;
            String path = Path.Combine(Server.MapPath("~/Imagenes"), Nombre);
            imagen.SaveAs(path);

            imagenes img = new imagenes();
            img.nombre = Nombre;
            img.id_noticia = max;

            db.imagenes.Add(img);
            db.SaveChanges();

            return RedirectToAction("Noticias");

        }
     

        public ActionResult EliminarNoticia(int id)
        {
            noticias noticia = db.noticias.Find(id);
            int n = noticia.id_noticia;
            db.noticias.Remove(noticia);
            db.SaveChanges();

            var imagenes = db.imagenes.ToList();
            
            int idImagen = 0;
            
            foreach (var i in imagenes)
            {
                if (i.id_noticia == n)
                {

                    idImagen = i.id_imagen;
                }
            }
            
            imagenes img = db.imagenes.Find(idImagen);
            if (img != null)
            {
                db.imagenes.Remove(img);
                db.SaveChanges();
                String ruta = Path.Combine(Server.MapPath("/Imagenes/"), img.nombre);
                System.IO.File.Delete(ruta);
            }
            
            
            

            return RedirectToAction("Noticias");
        }



        public ActionResult EditarNoticia(int id)
        {
            noticias noticia = db.noticias.Find(id);
            return View(noticia);

        }

        [HttpPost]
        public ActionResult EditarNoticia(noticias noticia, HttpPostedFileBase imagen)
        {

           
                db.Entry(noticia).State = EntityState.Modified;
                db.SaveChanges();


                var imagenes = db.imagenes.ToList();
            
            int id = 0;

            foreach(var i in imagenes){
                if (i.id_noticia == noticia.id_noticia)
                {
                    id = i.id_imagen;
                }
            }

            imagenes img = new imagenes();

            img = db.imagenes.Find(id);
            String ruta = Path.Combine(Server.MapPath("/Imagenes/"), img.nombre);
            System.IO.File.Delete(ruta);


            String Nombre = imagen.FileName;
            String path = Path.Combine(Server.MapPath("~/Imagenes"), Nombre);
            imagen.SaveAs(path);

            img.id_noticia = noticia.id_noticia;
            img.nombre = Nombre;
            img.id_imagen = id;

            db.Entry(img).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Noticias");


        }

        public ActionResult NoticiaCategoria(int id)
        {

            IEnumerable<noticias> noticia = from noticias in db.noticias orderby noticias.id_noticia descending
                          where noticias.id_categoria == id
                          select noticias;
                                           

            return View(noticia);
        }

    }
}
