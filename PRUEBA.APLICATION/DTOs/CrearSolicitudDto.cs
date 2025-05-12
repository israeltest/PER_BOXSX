using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA.APLICATION.DTOs
{
    public class CrearSolicitudDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string DireccionSolicitante { get; set; }
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; }
        [Required]
        public int EstadoSolicitudId { get; set; }
        [Required]
        public int TipoCompraId { get; set; }
    }
}
