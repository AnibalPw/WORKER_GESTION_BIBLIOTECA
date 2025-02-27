﻿
using WORKER_GESTION_BIBLIOTECA.Models;

namespace WORKER_GESTION_BIBLIOTECA.Repositories
{
    public interface IPrestamoRepository
    {
        Task<IEnumerable<Prestamo>> ObtenerPrestamosVencidosAsync();
        Task<bool> ActualizarEstadoPrestamoAsync(long idPrestamo, char estado);
        Task<bool> RegistrarHistorialAsync(long idPrestamo, string tipoOperacion, string detalles);
    }

}
