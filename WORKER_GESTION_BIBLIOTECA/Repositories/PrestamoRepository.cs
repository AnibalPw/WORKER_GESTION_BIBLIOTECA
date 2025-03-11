using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WORKER_GESTION_BIBLIOTECA.Models;

namespace WORKER_GESTION_BIBLIOTECA.Repositories
{
    // Repositories/PrestamoRepository.cs
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<PrestamoRepository> _logger;

        public PrestamoRepository(IConfiguration configuration, ILogger<PrestamoRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("WCS_CONEXION");
            _logger = logger;
        }

        public async Task<IEnumerable<Prestamo>> ObtenerPrestamosVencidosAsync()
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                return await connection.QueryAsync<Prestamo, Libro, Usuario, Prestamo>(
                    "PA_OBTENER_PRESTAMOS_VENCIDOS",
                    (prestamo, libro, usuario) =>
                    {
                        prestamo.Libro = libro;
                        prestamo.Usuario = usuario;
                        return prestamo;
                    },
                    commandType: CommandType.StoredProcedure,
                    splitOn: "Titulo,Nombre"
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener préstamos vencidos: {Message}", ex.Message);
                return Enumerable.Empty<Prestamo>();
            }

        }


        public async Task<bool> ActualizarEstadoPrestamoAsync(long idPrestamo, char estado)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(
                    "PA_ACTUALIZAR_ESTADO_PRESTAMO",
                    new { IdPrestamo = idPrestamo, Estado = estado },
                    commandType: CommandType.StoredProcedure
                );
                return true;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar estado del préstamo {IdPrestamo}: {Message}",
                    idPrestamo, ex.Message);
                return false;
            }
        }

        
        public async Task<bool> RegistrarTrazabilidadAsync(long idPrestamo, string tipoOperacion, string detalles)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(
                    "PA_REGISTRAR_TRAZABILIDAD",
                    new { IdPrestamo = idPrestamo, TipoOperacion = tipoOperacion, Detalles = detalles },
                    commandType: CommandType.StoredProcedure
                );
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al registrar historial para préstamo {IdPrestamo}: {Message}",
                    idPrestamo, ex.Message);
                return false;
            }
        }
    }
}
