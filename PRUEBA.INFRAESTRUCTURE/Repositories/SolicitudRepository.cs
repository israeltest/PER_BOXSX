using Dapper;
using PRUEBA.DOMINIO.ENTITIES;
using PRUEBA.DOMINIO.INTERFACE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRUEBA.INFRAESTRUCTURE.Repositories
{
    public class SolicitudRepository : ISolicitudRepository
    {
        private readonly DapperContext _context;

        public SolicitudRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Solicitud>> ObtenerSolicitudesAsync()
        {
            var query = "SP_ObtenerSolicitudes";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Solicitud, TipoCompra, EstadoSolicitud, Solicitud>(
                    query,
                    (solicitud, tipoCompra, estado) =>
                    {
                        solicitud.TipoCompra = tipoCompra;
                        solicitud.EstadoSolicitud = estado;
                        return solicitud;
                    },
                    splitOn: "TipoCompraId,EstadoId",
                    commandType: CommandType.StoredProcedure
                );

                return result;
            }
        }

        public async Task<Solicitud> ObtenerSolicitudPorIdAsync(int id)
        {
            var query = "SP_ObtenerSolicitudPorId";
            var parameters = new { SolicitudId = id };

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Solicitud, TipoCompra, EstadoSolicitud, Solicitud>(
                    query,
                    (solicitud, tipoCompra, estado) =>
                    {
                        solicitud.TipoCompra = tipoCompra;
                        solicitud.EstadoSolicitud = estado;
                        return solicitud;
                    },
                    param: parameters,
                    splitOn: "TipoCompraId,EstadoId",
                    commandType: CommandType.StoredProcedure
                );

                return result.FirstOrDefault(); // 👈 importante
            }
        }

        public async Task CrearSolicitudAsync(Solicitud solicitud)
        {
            var query = "sp_RegistrarSolicitud";

            var parameters = new
            {
                solicitud.Nombre,
                solicitud.DireccionSolicitante,
                solicitud.TipoCompraId,
                solicitud.Descripcion,
                solicitud.EstadoId
            };

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task ActualizarSolicitudAsync(Solicitud solicitud)
        {
            var query = "SP_ActualizarSolicitud";

            var parameters = new
            {
                solicitud.Id,
                solicitud.Nombre,
                solicitud.DireccionSolicitante,
                solicitud.Descripcion,
                solicitud.EstadoId,
                solicitud.TipoCompraId
            };

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
