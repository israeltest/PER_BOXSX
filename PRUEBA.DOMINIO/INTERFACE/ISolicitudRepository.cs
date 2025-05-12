using PRUEBA.DOMINIO.ENTITIES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA.DOMINIO.INTERFACE
{
    public interface ISolicitudRepository
    {
        Task<IEnumerable<Solicitud>> ObtenerSolicitudesAsync();
        Task<Solicitud> ObtenerSolicitudPorIdAsync(int id);
        Task CrearSolicitudAsync(Solicitud solicitud);
        Task ActualizarSolicitudAsync(Solicitud solicitud);
    }
}
