using PRUEBA.APLICATION.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA.APLICATION.INTERFACE
{
    public interface ISolicitudService
    {
        Task<List<SolicitudDto>> ObtenerSolicitudesAsync();
        Task<SolicitudDto> ObtenerSolicitudPorIdAsync(int id);
        Task CrearSolicitudAsync(CrearSolicitudDto solicitud);
        Task ActualizarSolicitudAsync(ActualizarSolicitudDto solicitud);
    }
}
