using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using WORKER_GESTION_BIBLIOTECA.Repositories;

namespace TEST_GESTION_BIBLIOTECA;

[TestClass]
public class BibliotecaTest
{

    private readonly IConfiguration _configurationString;
    private CancellationTokenSource _tokenSource;

    private ILogger<PrestamoRepository> _logger;
    private readonly IPrestamoRepository _prestamoRepository;

    private string _connectionString;


    public BibliotecaTest()
    {

        _configurationString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Establece la ruta base
                .AddJsonFile("appsettings.json") // Carga la configuración desde el archivo
                .Build();

        // Obtener la cadena de conexión directamente
        _connectionString = _configurationString.GetConnectionString("WCS_CONEXION"); //WCS_CONEXION

        var logger = new Mock<ILogger<PrestamoRepository>>().Object;
        _tokenSource = new CancellationTokenSource();
        _prestamoRepository = new PrestamoRepository(_configurationString, logger);

        // Limpiar y preparar datos de prueba
        PrepararDatosDePrueba();
    }


    #region --Datos de prueba --
    private void PrepararDatosDePrueba()
    {
        // Limpiar tablas relevantes y crear datos de prueba
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            // Limpiar tablas
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                        DELETE FROM TRAZABILIDAD;
                        DELETE FROM PRESTAMO;
                        DELETE FROM USUARIO;
                        DELETE FROM LIBRO;

                        -- Reiniciar identity si es necesario
                        DBCC CHECKIDENT ('PRESTAMO', RESEED, 0);
                        DBCC CHECKIDENT ('USUARIO', RESEED, 0);
                        DBCC CHECKIDENT ('LIBRO', RESEED, 0);
                        DBCC CHECKIDENT ('TRAZABILIDAD', RESEED, 0);
                    ";
                command.ExecuteNonQuery();

                // Insertar datos de prueba (usuarios y libros)
                command.CommandText = @"
                        -- Insertar usuarios de prueba
                        INSERT INTO USUARIO (cedula, nombre, primerApellido, segundoApellido, correo, nombreUsuario, contrasena, esEmpleado, estado, fechaRegistro) VALUES 
                        ('303330333', 'Carlos', 'Rodríguez', 'López', 'carlos.rl@mail.com', 'crodriguez', '123456', 0, 'A', GETDATE()),
                        ('404440444', 'Ana', 'Martínez', 'Vargas', 'ana.mv@mail.com', 'amartinez', '123456', 0, 'A', GETDATE());

                        -- Insertar libros de prueba
                        INSERT INTO LIBRO (Titulo, Autor, CopiasDisponibles, estado, fechaRegistro, fechaModificacion) VALUES 
                        ('Libro Test 1', 'Autor 1', 5, 'A', GETDATE(), NULL),
                        ('Libro Test 2', 'Autor 2', 3, 'A', GETDATE(), NULL),
                        ('Libro Test 3', 'Autor 3', 2, 'A', GETDATE(), NULL);
                    ";
                command.ExecuteNonQuery();

                // Insertar préstamos con diferentes fechas
                var fechaActual = DateTime.Now.Date;
                var fechaVencida1 = fechaActual.AddDays(-5);
                var fechaVencida2 = fechaActual.AddDays(-3);
                var fechaVencida3 = fechaActual.AddDays(-1);
                var fechaVencida4 = fechaActual.AddDays(-10);
                var fechaFutura1 = fechaActual.AddDays(3);
                var fechaFutura2 = fechaActual.AddDays(5);
                var fechaFutura3 = fechaActual.AddDays(10);
                var fechaFutura4 = fechaActual.AddDays(1);

                command.CommandText = $@"
                        -- Insertar préstamos vencidos (4)
                        INSERT INTO PRESTAMO (idLibro, idUsuario, fechaPrestamo, fechaDevolucionEsperada, estado) VALUES 
                        (1, 1, '{fechaVencida1.AddDays(-15):yyyy-MM-dd}', '{fechaVencida1:yyyy-MM-dd}', 'A'),
                        (2, 1, '{fechaVencida2.AddDays(-10):yyyy-MM-dd}', '{fechaVencida2:yyyy-MM-dd}', 'A'),
                        (1, 2, '{fechaVencida3.AddDays(-7):yyyy-MM-dd}', '{fechaVencida3:yyyy-MM-dd}', 'A'),
                        (3, 2, '{fechaVencida4.AddDays(-20):yyyy-MM-dd}', '{fechaVencida4:yyyy-MM-dd}', 'A');

                        -- Insertar préstamos vigentes (4)
                        INSERT INTO PRESTAMO (idLibro, idUsuario, fechaPrestamo, fechaDevolucionEsperada, estado) VALUES 
                        (3, 1, '{fechaFutura1.AddDays(-5):yyyy-MM-dd}', '{fechaFutura1:yyyy-MM-dd}', 'A'),
                        (2, 2, '{fechaFutura2.AddDays(-3):yyyy-MM-dd}', '{fechaFutura2:yyyy-MM-dd}', 'A'),
                        (1, 1, '{fechaFutura3.AddDays(-2):yyyy-MM-dd}', '{fechaFutura3:yyyy-MM-dd}', 'A'),
                        (3, 2, '{fechaFutura4.AddDays(-1):yyyy-MM-dd}', '{fechaFutura4:yyyy-MM-dd}', 'A');

                        -- Insertar préstamos con fecha de hoy (2)
                        INSERT INTO PRESTAMO (idLibro, idUsuario, fechaPrestamo, fechaDevolucionEsperada, estado) VALUES 
                        (2, 1, '{fechaActual.AddDays(-7):yyyy-MM-dd}', '{fechaActual:yyyy-MM-dd}', 'A'),
                        (1, 2, '{fechaActual.AddDays(-5):yyyy-MM-dd}', '{fechaActual:yyyy-MM-dd}', 'A');
                    ";
                command.ExecuteNonQuery();
            }
        }
    }
    #endregion --Fin Datos de prueba --

    [TestCleanup]
    public void Cleanup()
    {
        _tokenSource.Cancel();
        _tokenSource.Dispose();
    }

    #region -- Métodos --
    /// <summary>
    /// CP-001-PrestamosVencidos -> Debe identificar sólo préstamos vencidos
    /// </summary>
    [TestMethod]
    [Description("CP-001-PrestamosVencidos: Verificación de identificación de préstamos vencidos")]
    public async Task ObtenerPrestamosVencidosTest()
    {

        // Act
        var resultado = await _prestamoRepository.ObtenerPrestamosVencidosAsync();
        var prestamosVencidos = resultado.ToList();
        var fechaActual = DateTime.Now.Date;

        // Assert
        Assert.AreEqual(4, prestamosVencidos.Count, "Deben identificarse exactamente 4 préstamos vencidos");

        // Verificar que todos los préstamos tienen fecha vencida
      
        Assert.AreEqual(4, resultado.Count(), "Deben identificarse exactamente 4 préstamos vencidos");
        Assert.IsTrue(resultado.All(p => p.fechaDevolucionEsperada < fechaActual), "Todos los préstamos deben tener fecha vencida");
        Assert.IsTrue(resultado.All(p => p.estado == 'A'), "Todos los préstamos deben tener estado 'A'");

        #region -- Código cometado --
        //foreach (var prestamo in prestamosVencidos)
        //{
        //    Assert.IsTrue(prestamo.fechaDevolucionEsperada < fechaActual,
        //        $"El préstamo {prestamo.idPrestamo} no debería estar en la lista de vencidos");
        //    Assert.AreEqual('A', prestamo.estado, "Los préstamos deben tener estado 'A' antes de ser procesados");
        //Assert.IsNotNull(prestamo.Libro, "El libro asociado debe ser incluido");
        //Assert.IsNotNull(prestamo.Usuario, "El usuario asociado debe ser incluido");
        //}
        #endregion
    }


    /// <summary>
    ///CP-002-ActualizacionEstado -> Verificar actualización del estado de un préstamo en la base de datos
    /// </summary>
    [TestMethod]
    [Description("CP-002-ActualizacionEstado: Actualización de estado de préstamos vencidos")]
    public async Task ActualizarEstadoPrestamoTest()
    {
        // Arrange
        var prestamosVencidos = await _prestamoRepository.ObtenerPrestamosVencidosAsync();
        Assert.IsTrue(prestamosVencidos.Any(), "Deben existir préstamos vencidos para la prueba");

        // Act
        foreach (var prestamo in prestamosVencidos)
        {
            var resultado = await _prestamoRepository.ActualizarEstadoPrestamoAsync(prestamo.idPrestamo, 'R');
            Assert.IsTrue(resultado, $"La actualización del préstamo {prestamo.idPrestamo} debería ser exitosa");
        }

        // Assert - Verificar en la base de datos que todos los préstamos vencidos ahora tienen estado 'R'
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                        SELECT COUNT(*) 
                        FROM PRESTAMO 
                        WHERE CONVERT(DATE, fechaDevolucionEsperada) < CONVERT(DATE, GETDATE()) AND Estado = 'R'
                    ";
                var contadorActualizados = (int)await command.ExecuteScalarAsync();
                Assert.AreEqual(prestamosVencidos.Count(), contadorActualizados,
                    "Todos los préstamos vencidos deben estar actualizados a estado 'R'");
            }
        }
    }

    #endregion
}
