
using WORKER_GESTION_BIBLIOTECA.Models;

namespace WORKER_GESTION_BIBLIOTECA.Repositories
{
    public interface IPrestamoRepository
    {
        Task<IEnumerable<Prestamo>> ObtenerPrestamosVencidosAsync();

        Task<bool> ActualizarEstadoPrestamoAsync(long idPrestamo, char estado);

        Task<bool> RegistrarTrazabilidadAsync(long idPrestamo, string tipoOperacion, string detalles);

        Task<TrazabilidadResumen> VerificarTrazabilidadAsync(string tipoOperacion);

        Task RegistrarDevolucionesAsync(long idPrestamo);
        
        Task<Dictionary<string, long>> GenerarReporteDiarioAsync(DateTime? fechaReporte = null);
    }

}
