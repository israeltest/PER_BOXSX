namespace ClientWeb.DTOs
{
    public class ActualizarSolicitudDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string DireccionSolicitante { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int EstadoSolicitudId { get; set; }
        public int TipoCompraId { get; set; }
    }
}
