using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    public class Disco
    {
        public int Id { get; set; }
        [DisplayName("Título")]
        public string Titulo { get; set; }
        [DisplayName("Fecha de Lanzamiento")]
        public DateTime FechaLanzamiento { get; set; }
        [DisplayName("Cantidad de Canciones")]
        public int CantidadDeCanciones { get; set; }
        public string UrlImagenCover { get; set; }
        public Estilos Estilo { get; set; }
        public TipoEdicion Edicion { get; set; }
        public bool Activo { get; set; }

        public string EstiloDescripcion => Estilo.Descripcion;
        public string EdicionDescripcion => Edicion.Description;
    }
}
