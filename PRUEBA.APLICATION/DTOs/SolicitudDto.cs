using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA.APLICATION.DTOs
{
    public class SolicitudDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string DireccionSolicitante { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaSolicitud { get; set; }

        public int EstadoSolicitudId { get; set; }
        public string EstadoDescripcion { get; set; }

        public int TipoCompraId { get; set; }
        public string TipoCompraDescripcion { get; set; }

    }
}
