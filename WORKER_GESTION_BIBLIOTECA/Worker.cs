using Microsoft.Data.SqlClient;
using WORKER_GESTION_BIBLIOTECA.Repositories;

namespace WORKER_GESTION_BIBLIOTECA
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IPrestamoRepository _prestamoRepository;

        public Worker(ILogger<Worker> logger, IPrestamoRepository prestamoRepository)
        {
            _logger = logger;
            _prestamoRepository = prestamoRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Worker ejecutándose a las: {time}", DateTimeOffset.Now);

                    //Se obtiene la lista de prestamos vencidos
                    var prestamosVencidos = await _prestamoRepository.ObtenerPrestamosVencidosAsync();

                    //foreach (var prestamo in prestamosVencidos)
                    //{
                    //    //Se actualiza el estado del prestamo vencido a 'R'
                    //    await _prestamoRepository.ActualizarEstadoPrestamoAsync(prestamo.idPrestamo, 'R');

                    //    string detalles = $"Préstamo marcado como retrasado. Libro: {prestamo.Libro.titulo}, " +
                    //                    $"Usuario: {prestamo.Usuario.nombre} {prestamo.Usuario.primerApellido}";

                    //    //Se registra en la tabla de trazabilidad el prestamo en cuestión
                    //    await _prestamoRepository.RegistrarTrazabilidadAsync(
                    //        prestamo.idPrestamo,
                    //        "Retraso",
                    //        detalles
                    //    );

                    //    _logger.LogInformation("Préstamo {IdPrestamo} marcado como retrasado", prestamo.idPrestamo);
                    //}

                    await Task.Delay(TimeSpan.FromHours(12), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error en el proceso del worker");
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
            }
        }
    }
}
