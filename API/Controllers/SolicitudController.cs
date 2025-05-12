using Microsoft.AspNetCore.Mvc;
using PRUEBA.APLICATION.DTOs;
using PRUEBA.APLICATION.INTERFACE;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudController : ControllerBase
    {
        private readonly ISolicitudService _solicitudService;

        public SolicitudController(ISolicitudService solicitudService)
        {
            _solicitudService = solicitudService;
        }

        [HttpGet("ObtenerSolicitudes")]
        public async Task<IActionResult> ObtenerSolicitudes()
        {
            var solicitudes = await _solicitudService.ObtenerSolicitudesAsync();
            return Ok(solicitudes);
        }

        [HttpGet("obtener/{id}")]
        public async Task<IActionResult> ObtenerSolicitudPorId(int id)
        {
            var solicitud = await _solicitudService.ObtenerSolicitudPorIdAsync(id);
            if (solicitud == null)
                return NotFound(new { mensaje = "Solicitud no encontrada" });

            return Ok(solicitud);
        }

        [HttpPost("CrearSolicitud")]
        public async Task<IActionResult> CrearSolicitud([FromBody] CrearSolicitudDto solicitudDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _solicitudService.CrearSolicitudAsync(solicitudDto);
            return Ok(new { mensaje = "Solicitud creada con éxito" });
        }

        /*[HttpPut("{id}", Name = "ActualizarSolicitud")]*/
        [HttpPut("ActualizarSolicitud/{id}")]
        public async Task<IActionResult> ActualizarSolicitud(int id, [FromBody] ActualizarSolicitudDto solicitudDto)
        {
            //if (id != solicitudDto.Id)
            //    return BadRequest(new { mensaje = "El ID de la URL no coincide con el del cuerpo de la solicitud" });
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _solicitudService.ActualizarSolicitudAsync(solicitudDto);
            return Ok(new { mensaje = "Solicitud actualizada con éxito" });
        }
    }
}
