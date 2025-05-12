using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA.DOMINIO.ENTITIES
{
    public class Solicitud
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string DireccionSolicitante { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int EstadoId { get; set; }
        public int TipoCompraId { get; set; }

        public TipoCompra TipoCompra { get; set; }
        public EstadoSolicitud EstadoSolicitud { get; set; }
    }

    public class TipoCompra
    {
        public int TipoCompraId { get; set; }
        public string TipoCompraDescripcion { get; set; }
    }

    public class EstadoSolicitud
    {
        public int EstadoId { get; set; }
        public string EstadoDescripcion { get; set; }
    }
}
