using Microsoft.EntityFrameworkCore;

namespace One_Vision.Utils
{
    public class Paginacion<T>
    {
        public List<T> Items { get; set; }
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }

        public bool TienePaginaAnterior => PaginaActual > 1;
        public bool TienePaginaSiguiente => PaginaActual < TotalPaginas;

        public Paginacion(List<T> items, int conteoTotal, int paginaActual, int tamanioPagina)
        {
            PaginaActual = paginaActual;
            TotalPaginas = (int)Math.Ceiling(conteoTotal / (double)tamanioPagina);
            Items = items;
        }

        public static async Task<Paginacion<T>> CrearAsync(IQueryable<T> fuente, int paginaActual, int tamanioPagina)
        {
            var conteo = await fuente.CountAsync();
            var items = await fuente.Skip((paginaActual - 1) * tamanioPagina).Take(tamanioPagina).ToListAsync();
            return new Paginacion<T>(items, conteo, paginaActual, tamanioPagina);
        }
    }
}
