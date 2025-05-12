using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using ClientWeb.DTOs;

namespace ClientWeb.Controllers
{
    public class SolicitudController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl;

        public SolicitudController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"];
        }

        // GET: /Solicitudes
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"{_apiBaseUrl}/ObtenerSolicitudes");
            if (!response.IsSuccessStatusCode) return View("Error");

            var content = await response.Content.ReadAsStringAsync();
            var solicitudes = JsonSerializer.Deserialize<IEnumerable<SolicitudDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(solicitudes);
        }

        // GET: /Solicitudes/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: /Solicitudes/Crear
        [HttpPost]
        public async Task<IActionResult> Crear(CrearSolicitudDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_apiBaseUrl}/CrearSolicitud", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al crear la solicitud.");
            return View(dto);
        }

        // GET: /Solicitudes/Editar/{id}
        public async Task<IActionResult> Editar(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}/obtener/{id}");

            if (!response.IsSuccessStatusCode) return NotFound();

            var content = await response.Content.ReadAsStringAsync();
            var solicitud = JsonSerializer.Deserialize<ActualizarSolicitudDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(solicitud);
        }

        // POST: /Solicitudes/Editar
        [HttpPost]
        public async Task<IActionResult> Editar(ActualizarSolicitudDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{_apiBaseUrl}/ActualizarSolicitud/{dto.Id}", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Error al actualizar la solicitud.");
            return View(dto);
        }
    }
}
