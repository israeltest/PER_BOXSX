namespace ClientWeb.DTOs
{
    public class SolicitudDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string DireccionSolicitante { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int EstadoSolicitudId { get; set; }
        public int TipoCompraId { get; set; }
        public string EstadoDescripcion { get; set; } = string.Empty;
        public string TipoCompraDescripcion { get; set; } = string.Empty;
    }
}
