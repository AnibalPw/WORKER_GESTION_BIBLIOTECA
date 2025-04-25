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
    private readonly IPrestamoRepository _prestamoRepository;
    private string _connectionString;


    public BibliotecaTest()
    {

        _configurationString = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Establece la ruta base
                .AddJsonFile("appsettings.json") // Carga la configuración desde el archivo
                .Build();

        // Cadena de conexión a BD
        _connectionString = _configurationString.GetConnectionString("WCS_CONEXION");

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
                        DELETE FROM WCS_GESTION_BIBLIOTECA..TRAZABILIDAD;
                        DELETE FROM WCS_GESTION_BIBLIOTECA..PRESTAMO;
                        DELETE FROM WCS_GESTION_BIBLIOTECA..USUARIO;
                        DELETE FROM WCS_GESTION_BIBLIOTECA..LIBRO;

                        -- Reiniciar identity si es necesario
                        DBCC CHECKIDENT ('WCS_GESTION_BIBLIOTECA..PRESTAMO', RESEED, 0);
                        DBCC CHECKIDENT ('WCS_GESTION_BIBLIOTECA..USUARIO', RESEED, 0);
                        DBCC CHECKIDENT ('WCS_GESTION_BIBLIOTECA..LIBRO', RESEED, 0);
                        DBCC CHECKIDENT ('WCS_GESTION_BIBLIOTECA..TRAZABILIDAD', RESEED, 0);
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

    //[TestInitialize]
    //public void Setup()
    //{
    //    // Inicializar un nuevo token si fue cancelado
    //    if (_tokenSource.IsCancellationRequested)
    //    {
    //        _tokenSource.Dispose();
    //        _tokenSource = new CancellationTokenSource();
    //    }

    //    // Preparar datos frescos para cada prueba
    //    PrepararDatosDePrueba();
    //}

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
    [DoNotParallelize] //evita la ejecución paralela
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
    [DoNotParallelize]
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


    /// <summary>
    /// CP-003-RegistroTrazabilidad -> Verificar registro de trazabilidad en la base de datos
    /// </summary>
    [TestMethod]
    [DoNotParallelize]
    [Description("CP-003-RegistroTrazabilidad: Registro en Trazabilidad de operaciones")]
    public async Task RegistrarTrazabilidadTest()
    {
       

        // Arrange
        var prestamosVencidos = await _prestamoRepository.ObtenerPrestamosVencidosAsync();
        Assert.IsTrue(prestamosVencidos.Any(), "Deben existir préstamos vencidos para la prueba");

        // Act - Limpiar tabla de trazabilidad para tener un estado inicial conocido
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Trazabilidad";
                await command.ExecuteNonQueryAsync();
            }
        }

        // Se registrar la trazabilidad para cada préstamo vencido
        foreach (var prestamo in prestamosVencidos)
        {
            //Se vuelve a actualizar el estado en R ya que las pruebas se renician con el método  PrepararDatosDePrueba()
            var updateEstadoPrestamo = await _prestamoRepository.ActualizarEstadoPrestamoAsync(prestamo.idPrestamo, 'R');

            //Registrar en trazabilidad
            string detalles = $"Libro '{prestamo.Libro.titulo}' bajo préstamo al usuario '{prestamo.Usuario.nombre} {prestamo.Usuario.primerApellido}' marcado como retrasado";

            var resultado = await _prestamoRepository.RegistrarTrazabilidadAsync(prestamo.idPrestamo, "Retraso", detalles);
            Assert.IsTrue(resultado, $"El registro de trazabilidad para el préstamo {prestamo.idPrestamo} debería ser exitoso");
        }

        // Assert - Verificar que se registró correctamente usando el nuevo método
        var verificacion = await _prestamoRepository.VerificarTrazabilidadAsync("Retraso");

        // Verificar el conteo
        Assert.AreEqual(prestamosVencidos.Count(), verificacion.TotalRegistros,
            "Debe existir un registro de trazabilidad por cada préstamo procesado");

        // Verificar el detalle del último registro
        Assert.IsNotNull(verificacion.UltimoRegistro, "Debe existir al menos un registro de trazabilidad");

        Assert.IsTrue(verificacion.UltimoRegistro.Detalles.Contains("Libro"),
            "Los detalles deben incluir información del libro");

        Assert.IsTrue(verificacion.UltimoRegistro.Detalles.Contains("usuario"),
            "Los detalles deben incluir información del usuario");

        Assert.IsTrue((DateTime.Now - verificacion.UltimoRegistro.FechaOperacion).TotalMinutes < 5,
            "La fecha de operación debe ser cercana al momento actual");

        // Verificaciones adicionales con los datos enriquecidos
        Assert.IsFalse(string.IsNullOrEmpty(verificacion.UltimoRegistro.LibroTitulo),
            "El título del libro debe estar disponible");
        Assert.IsFalse(string.IsNullOrEmpty(verificacion.UltimoRegistro.UsuarioNombre),
            "El nombre del usuario debe estar disponible");
    }

    /// <summary>
    /// CP-004-ActualizacionInventario -> Actualización automática del inventario(disponibilidad) de libros
    /// </summary>
    [TestMethod]
    [DoNotParallelize]
    [Description("CP-004-ActualizacionInventario: Actualización automática del inventario de libros")]
    public async Task ActualizarInventarioTest()
    {
   
        // Arrange - Obtener estado inicial de algunos libros
        Dictionary<int, int> copiasInicialesPorLibro = new Dictionary<int, int>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT idLibro, CopiasDisponibles FROM LIBRO WHERE idLibro IN (1, 2, 3)";
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int idLibro = (int)reader.GetInt64(0);
                        int copias = reader.GetInt32(1);
                        copiasInicialesPorLibro[idLibro] = copias;
                    }
                }
            }
        }

        Assert.IsTrue(copiasInicialesPorLibro.Count == 3, "Deben existir 3 libros para la prueba");

        // Act - Obtener préstamos activos y ejecutar devolución usando el RegistrarDevolucionesAsync
        var prestamosParaDevolver = new List<int>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                SELECT TOP 1 idLibro FROM PRESTAMO WHERE idLibro = 1 AND Estado = 'A'
                UNION ALL
                SELECT TOP 1 idLibro FROM PRESTAMO WHERE idLibro = 2 AND Estado = 'A'
                UNION ALL
                SELECT TOP 1 idLibro FROM PRESTAMO WHERE idLibro = 3 AND Estado = 'A'
            ";
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        prestamosParaDevolver.Add((int)reader.GetInt64(0));
                    }
                }
            }
        }

        // Ejecutar la devolución para cada préstamo usando el método RegistrarDevolucionesAsync
        foreach (var idPrestamo in prestamosParaDevolver)
        {
            await _prestamoRepository.RegistrarDevolucionesAsync(idPrestamo);
        }


        // Assert - Verificar que el inventario se actualizó
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                foreach (var libroId in copiasInicialesPorLibro.Keys)
                {
                    command.CommandText = "SELECT copiasDisponibles FROM LIBRO WHERE idLibro = @IdLibro";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@IdLibro", libroId);

                    var copiasActuales = (int)await command.ExecuteScalarAsync();
                    var copiasIniciales = copiasInicialesPorLibro[libroId];

                    // Verificar si el libro fue devuelto (incremento en copias)
                    command.CommandText = @"
                    SELECT COUNT(*) FROM Trazabilidad t
                    JOIN PRESTAMO p ON t.idPrestamo = p.idPrestamo
                    WHERE p.idLibro = @IdLibro AND t.tipoOperacion = 'Devolución'
                ";
                    var devolucionesRegistradas = (int)await command.ExecuteScalarAsync();

                    Assert.AreEqual(copiasIniciales + devolucionesRegistradas, copiasActuales,
                        $"El libro {libroId} debería tener {copiasIniciales + devolucionesRegistradas} copias disponibles");
                }

                // Verificar que se registró en trazabilidad
                command.CommandText = "SELECT COUNT(*) FROM Trazabilidad WHERE tipoOperacion = 'Devolución'";
                command.Parameters.Clear();
                var contadorDevolucionesTrazabilidad = (int)await command.ExecuteScalarAsync();

                Assert.IsTrue(contadorDevolucionesTrazabilidad > 0,
                    "Se deben registrar las devoluciones en la tabla de trazabilidad");
            }
        }

    }



    /// <summary>
    /// CP-005-ReportesDiarios-> Generación automática de reportes diarios
    /// </summary>
    [TestMethod]
    [DoNotParallelize]
    [Description("CP-005-ReportesDiarios: Generación automática de reportes diarios")]
    public async Task GenerarReporteDiarioTest()
    {
      

        // Arrange - Limpiar y poblar datos de prueba
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Trazabilidad";
                await command.ExecuteNonQueryAsync();

                command.CommandText = @"
                INSERT INTO Trazabilidad (idPrestamo, tipoOperacion, detalles, fechaOperacion)
                SELECT TOP 3 idPrestamo, 'Préstamo', 'Préstamo realizado', GETDATE() FROM PRESTAMO WHERE estado = 'A';

                INSERT INTO Trazabilidad (idPrestamo, tipoOperacion, detalles, fechaOperacion)
                SELECT TOP 2 idPrestamo, 'Devolución', 'Devolución realizada', GETDATE() FROM PRESTAMO WHERE estado = 'A';

                INSERT INTO Trazabilidad (idPrestamo, tipoOperacion, detalles, fechaOperacion)
                SELECT TOP 4 idPrestamo, 'Retraso', 'Préstamo marcado como retrasado', GETDATE() FROM PRESTAMO WHERE fechaDevolucionEsperada < GETDATE();
            ";
                await command.ExecuteNonQueryAsync();
            }
        }

        // Act - Ejecutar el procedimiento almacenado usando el método GenerarReporteDiarioAsync
        var reporteOperaciones = await _prestamoRepository.GenerarReporteDiarioAsync();

        // Assert - Validar los resultados
        Assert.IsTrue(reporteOperaciones.ContainsKey("Préstamo"), "El reporte debe incluir préstamos");
        Assert.IsTrue(reporteOperaciones.ContainsKey("Devolución"), "El reporte debe incluir devoluciones");
        Assert.IsTrue(reporteOperaciones.ContainsKey("Retraso"), "El reporte debe incluir retrasos");

        Assert.AreEqual(3, reporteOperaciones["Préstamo"], "Debe haber 3 préstamos registrados");
        Assert.AreEqual(2, reporteOperaciones["Devolución"], "Debe haber 2 devoluciones registradas");
        Assert.AreEqual(4, reporteOperaciones["Retraso"], "Debe haber 4 retrasos registrados");
    }

    #endregion
}
