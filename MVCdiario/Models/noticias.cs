//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCdiario.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class noticias
    {
        public noticias()
        {
            this.imagenes = new HashSet<imagenes>();
        }
    
        public int id_noticia { get; set; }
        public string titulo { get; set; }
        public string contenido { get; set; }
        public int id_categoria { get; set; }
        public string id_usuario { get; set; }
    
        public virtual categorias categorias { get; set; }
        public virtual ICollection<imagenes> imagenes { get; set; }
        public virtual usuarios usuarios { get; set; }
    }
}
