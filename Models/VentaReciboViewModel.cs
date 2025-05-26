namespace One_Vision.Models
{
    public class VentaReciboViewModel
    {
        public int ID_Venta { get; set; }
        public int ID_Paciente { get; set; }

        // ✅ Datos del paciente
        public string NombrePaciente { get; set; }
        public string TelefonoPaciente { get; set; }
        public string DireccionPaciente { get; set; }

        // ✅ Datos del usuario que realizó la venta
        public string NombreUsuario { get; set; }

        public decimal Total { get; set; }
        public decimal Abonado { get; set; }
        public DateTime Fecha { get; set; }

        public List<VentaProductoDetalle> Productos { get; set; }

        public class VentaProductoDetalle
        {
            public string Nombre { get; set; }
            public int Cantidad { get; set; }
            public decimal Precio { get; set; }
        }
    }
}
