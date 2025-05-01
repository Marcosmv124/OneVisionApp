using One_Vision.Utils;

namespace One_Vision.Models
{
    public class PacienteProductoViewModel
    {
        public Paginacion<Paciente> Pacientes { get; set; }
        public Paginacion<Producto> Productos { get; set; }

    }
}
