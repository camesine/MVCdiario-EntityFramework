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
    
    public partial class imagenes
    {
        public int id_imagen { get; set; }
        public string nombre { get; set; }
        public int id_noticia { get; set; }
    
        public virtual noticias noticias { get; set; }
    }
}
