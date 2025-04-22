namespace One_Vision.Models
{
    public class PacienteProductoViewModel
    {
        public IEnumerable<One_Vision.Models.Paciente> Pacientes { get; set; }
        public IEnumerable<One_Vision.Models.Producto> Productos { get; set; }
        public IEnumerable<One_Vision.Models.Examen> Examenes { get; set; }
    }
}
