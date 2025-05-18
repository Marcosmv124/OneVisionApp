namespace One_Vision.Models
{
    public class VentaReciboViewModel
    {
        public int ID_Venta { get; set; }
        public string ID_Paciente { get; set; }
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
