using PRUEBA.APLICATION.DTOs;
using PRUEBA.APLICATION.INTERFACE;
using PRUEBA.DOMINIO.ENTITIES;
using PRUEBA.DOMINIO.INTERFACE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA.APLICATION.SERVICES
{
    public class SolicitudService : ISolicitudService
    {
        private readonly ISolicitudRepository _solicitudRepository;

        // Constructor que recibe la interfaz de ISolicitudRepository
        public SolicitudService(ISolicitudRepository solicitudRepository)
        {
            _solicitudRepository = solicitudRepository;
        }

        // Obtener todas las solicitudes
        public async Task<List<SolicitudDto>> ObtenerSolicitudesAsync()
        {
            var solicitudes = await _solicitudRepository.ObtenerSolicitudesAsync();

            return solicitudes
                .Select(s => new SolicitudDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    DireccionSolicitante = s.DireccionSolicitante,
                    Descripcion = s.Descripcion,
                    FechaSolicitud = s.FechaSolicitud,
                    EstadoSolicitudId = s.EstadoSolicitud.EstadoId,
                    TipoCompraId = s.TipoCompraId,
                    TipoCompraDescripcion = s.TipoCompra.TipoCompraDescripcion,
                    EstadoDescripcion = s.EstadoSolicitud.EstadoDescripcion
                })
                .ToList();
        }

        // Obtener solicitud por ID
        public async Task<SolicitudDto> ObtenerSolicitudPorIdAsync(int id)
        {
            var solicitud = await _solicitudRepository.ObtenerSolicitudPorIdAsync(id);
            if (solicitud == null) return null;

            return new SolicitudDto
            {
                Id = solicitud.Id,
                Nombre = solicitud.Nombre,
                DireccionSolicitante = solicitud.DireccionSolicitante,
                Descripcion = solicitud.Descripcion,
                FechaSolicitud = solicitud.FechaSolicitud,
                EstadoSolicitudId = solicitud.EstadoSolicitud.EstadoId,
                TipoCompraId = solicitud.TipoCompraId,
                TipoCompraDescripcion = solicitud.TipoCompra.TipoCompraDescripcion,
                EstadoDescripcion = solicitud.EstadoSolicitud.EstadoDescripcion
            };
        }

        // Crear solicitud
        public async Task CrearSolicitudAsync(CrearSolicitudDto solicitudDto)
        {
            var solicitud = new Solicitud
            {
                Nombre = solicitudDto.Nombre,
                DireccionSolicitante = solicitudDto.DireccionSolicitante,
                Descripcion = solicitudDto.Descripcion,
                EstadoId = solicitudDto.EstadoSolicitudId,
                TipoCompraId = solicitudDto.TipoCompraId
            };

            await _solicitudRepository.CrearSolicitudAsync(solicitud);
        }

        // Actualizar solicitud
        public async Task ActualizarSolicitudAsync(ActualizarSolicitudDto solicitudDto)
        {
            var solicitud = new Solicitud
            {
                Id = solicitudDto.Id,
                Nombre = solicitudDto.Nombre,
                DireccionSolicitante = solicitudDto.DireccionSolicitante,
                Descripcion = solicitudDto.Descripcion,
                EstadoId = solicitudDto.EstadoSolicitudId,
                TipoCompraId = solicitudDto.TipoCompraId
            };

            await _solicitudRepository.ActualizarSolicitudAsync(solicitud);
        }
    }
}
