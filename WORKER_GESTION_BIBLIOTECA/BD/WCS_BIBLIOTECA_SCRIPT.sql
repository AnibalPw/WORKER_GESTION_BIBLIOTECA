USE [WCS_GESTION_BIBLIOTECA]
GO
/****** Object:  Table [dbo].[LIBRO]    Script Date: 12/3/2025 00:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LIBRO](
	[idLibro] [bigint] IDENTITY(1,1) NOT NULL,
	[titulo] [varchar](200) NOT NULL,
	[autor] [varchar](200) NOT NULL,
	[copiasDisponibles] [int] NULL,
	[estado] [char](1) NULL,
	[fechaRegistro] [datetime] NULL,
	[fechaModificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idLibro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRESTAMO]    Script Date: 12/3/2025 00:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRESTAMO](
	[idPrestamo] [bigint] IDENTITY(1,1) NOT NULL,
	[idLibro] [bigint] NULL,
	[idUsuario] [bigint] NULL,
	[fechaPrestamo] [datetime] NULL,
	[fechaDevolucionEsperada] [datetime] NULL,
	[estado] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[idPrestamo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TRAZABILIDAD]    Script Date: 12/3/2025 00:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRAZABILIDAD](
	[idTrazabilidad] [bigint] IDENTITY(1,1) NOT NULL,
	[idPrestamo] [bigint] NULL,
	[tipoOperacion] [varchar](20) NULL,
	[fechaOperacion] [datetime] NULL,
	[detalles] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[idTrazabilidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIO]    Script Date: 12/3/2025 00:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIO](
	[idUsuario] [bigint] IDENTITY(1,1) NOT NULL,
	[cedula] [varchar](30) NULL,
	[nombre] [varchar](100) NULL,
	[primerApellido] [varchar](100) NULL,
	[segundoApellido] [varchar](100) NULL,
	[correo] [varchar](200) NULL,
	[nombreUsuario] [varchar](150) NULL,
	[contrasena] [varchar](30) NULL,
	[esEmpleado] [bit] NULL,
	[estado] [char](1) NULL,
	[fechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[LIBRO] ON 

INSERT [dbo].[LIBRO] ([idLibro], [titulo], [autor], [copiasDisponibles], [estado], [fechaRegistro], [fechaModificacion]) VALUES (1, N'Cien años de soledad', N'Gabriel García Márquez', 3, N'A', CAST(N'2025-02-19T22:45:53.367' AS DateTime), NULL)
INSERT [dbo].[LIBRO] ([idLibro], [titulo], [autor], [copiasDisponibles], [estado], [fechaRegistro], [fechaModificacion]) VALUES (2, N'El principito', N'Antoine de Saint-Exupéry', 2, N'A', CAST(N'2025-02-19T22:45:53.367' AS DateTime), NULL)
INSERT [dbo].[LIBRO] ([idLibro], [titulo], [autor], [copiasDisponibles], [estado], [fechaRegistro], [fechaModificacion]) VALUES (3, N'Don Quijote de la Mancha', N'Miguel de Cervantes', 1, N'A', CAST(N'2025-02-19T22:45:53.367' AS DateTime), NULL)
INSERT [dbo].[LIBRO] ([idLibro], [titulo], [autor], [copiasDisponibles], [estado], [fechaRegistro], [fechaModificacion]) VALUES (4, N'1984', N'George Orwell', 2, N'A', CAST(N'2025-02-19T22:45:53.367' AS DateTime), NULL)
INSERT [dbo].[LIBRO] ([idLibro], [titulo], [autor], [copiasDisponibles], [estado], [fechaRegistro], [fechaModificacion]) VALUES (5, N'El Señor de los Anillos', N'J.R.R. Tolkien', 1, N'A', CAST(N'2025-02-19T22:45:53.367' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[LIBRO] OFF
GO
SET IDENTITY_INSERT [dbo].[PRESTAMO] ON 

INSERT [dbo].[PRESTAMO] ([idPrestamo], [idLibro], [idUsuario], [fechaPrestamo], [fechaDevolucionEsperada], [estado]) VALUES (1, 1, 3, CAST(N'2025-02-14T22:46:01.530' AS DateTime), CAST(N'2025-03-01T22:46:01.530' AS DateTime), N'A')
INSERT [dbo].[PRESTAMO] ([idPrestamo], [idLibro], [idUsuario], [fechaPrestamo], [fechaDevolucionEsperada], [estado]) VALUES (2, 2, 4, CAST(N'2025-02-16T22:46:01.530' AS DateTime), CAST(N'2025-03-03T22:46:01.530' AS DateTime), N'A')
INSERT [dbo].[PRESTAMO] ([idPrestamo], [idLibro], [idUsuario], [fechaPrestamo], [fechaDevolucionEsperada], [estado]) VALUES (3, 3, 5, CAST(N'2025-02-04T22:46:01.530' AS DateTime), CAST(N'2025-02-18T22:46:01.530' AS DateTime), N'A')
INSERT [dbo].[PRESTAMO] ([idPrestamo], [idLibro], [idUsuario], [fechaPrestamo], [fechaDevolucionEsperada], [estado]) VALUES (4, 4, 3, CAST(N'2025-01-30T22:46:01.530' AS DateTime), CAST(N'2025-02-14T22:46:01.530' AS DateTime), N'A')
INSERT [dbo].[PRESTAMO] ([idPrestamo], [idLibro], [idUsuario], [fechaPrestamo], [fechaDevolucionEsperada], [estado]) VALUES (5, 5, 4, CAST(N'2025-02-09T22:46:01.530' AS DateTime), CAST(N'2025-02-24T22:46:01.530' AS DateTime), N'D')
SET IDENTITY_INSERT [dbo].[PRESTAMO] OFF
GO
SET IDENTITY_INSERT [dbo].[TRAZABILIDAD] ON 

INSERT [dbo].[TRAZABILIDAD] ([idTrazabilidad], [idPrestamo], [tipoOperacion], [fechaOperacion], [detalles]) VALUES (1, 1, N'Prestamo', CAST(N'2025-02-14T22:46:14.750' AS DateTime), N'Préstamo realizado correctamente')
INSERT [dbo].[TRAZABILIDAD] ([idTrazabilidad], [idPrestamo], [tipoOperacion], [fechaOperacion], [detalles]) VALUES (2, 2, N'Prestamo', CAST(N'2025-02-16T22:46:14.750' AS DateTime), N'Préstamo realizado correctamente')
INSERT [dbo].[TRAZABILIDAD] ([idTrazabilidad], [idPrestamo], [tipoOperacion], [fechaOperacion], [detalles]) VALUES (3, 3, N'Prestamo', CAST(N'2025-02-04T22:46:14.750' AS DateTime), N'Préstamo realizado correctamente')
INSERT [dbo].[TRAZABILIDAD] ([idTrazabilidad], [idPrestamo], [tipoOperacion], [fechaOperacion], [detalles]) VALUES (4, 4, N'Prestamo', CAST(N'2025-01-30T22:46:14.750' AS DateTime), N'Préstamo realizado correctamente')
INSERT [dbo].[TRAZABILIDAD] ([idTrazabilidad], [idPrestamo], [tipoOperacion], [fechaOperacion], [detalles]) VALUES (5, 5, N'Prestamo', CAST(N'2025-02-09T22:46:14.750' AS DateTime), N'Préstamo realizado correctamente')
INSERT [dbo].[TRAZABILIDAD] ([idTrazabilidad], [idPrestamo], [tipoOperacion], [fechaOperacion], [detalles]) VALUES (6, 5, N'Devolución', CAST(N'2025-02-17T22:46:14.750' AS DateTime), N'Libro devuelto en buen estado')
SET IDENTITY_INSERT [dbo].[TRAZABILIDAD] OFF
GO
SET IDENTITY_INSERT [dbo].[USUARIO] ON 

INSERT [dbo].[USUARIO] ([idUsuario], [cedula], [nombre], [primerApellido], [segundoApellido], [correo], [nombreUsuario], [contrasena], [esEmpleado], [estado], [fechaRegistro]) VALUES (1, N'101110111', N'Juan', N'Pérez', N'Mora', N'juan.perez@mail.com', N'jperez', N'123456', 1, N'A', CAST(N'2025-02-19T22:45:45.797' AS DateTime))
INSERT [dbo].[USUARIO] ([idUsuario], [cedula], [nombre], [primerApellido], [segundoApellido], [correo], [nombreUsuario], [contrasena], [esEmpleado], [estado], [fechaRegistro]) VALUES (2, N'202220222', N'María', N'González', N'Silva', N'maria.gs@mail.com', N'mgonzalez', N'123456', 1, N'A', CAST(N'2025-02-19T22:45:45.797' AS DateTime))
INSERT [dbo].[USUARIO] ([idUsuario], [cedula], [nombre], [primerApellido], [segundoApellido], [correo], [nombreUsuario], [contrasena], [esEmpleado], [estado], [fechaRegistro]) VALUES (3, N'303330333', N'Carlos', N'Rodríguez', N'López', N'carlos.rl@mail.com', N'crodriguez', N'123456', 0, N'A', CAST(N'2025-02-19T22:45:45.797' AS DateTime))
INSERT [dbo].[USUARIO] ([idUsuario], [cedula], [nombre], [primerApellido], [segundoApellido], [correo], [nombreUsuario], [contrasena], [esEmpleado], [estado], [fechaRegistro]) VALUES (4, N'404440444', N'Ana', N'Martínez', N'Vargas', N'ana.mv@mail.com', N'amartinez', N'123456', 0, N'A', CAST(N'2025-02-19T22:45:45.797' AS DateTime))
INSERT [dbo].[USUARIO] ([idUsuario], [cedula], [nombre], [primerApellido], [segundoApellido], [correo], [nombreUsuario], [contrasena], [esEmpleado], [estado], [fechaRegistro]) VALUES (5, N'505550555', N'Pedro', N'Sánchez', N'Rojas', N'pedro.sr@mail.com', N'psanchez', N'123456', 0, N'A', CAST(N'2025-02-19T22:45:45.797' AS DateTime))
SET IDENTITY_INSERT [dbo].[USUARIO] OFF
GO
ALTER TABLE [dbo].[LIBRO] ADD  DEFAULT ('A') FOR [estado]
GO
ALTER TABLE [dbo].[USUARIO] ADD  DEFAULT ((0)) FOR [esEmpleado]
GO
ALTER TABLE [dbo].[USUARIO] ADD  DEFAULT ('A') FOR [estado]
GO
ALTER TABLE [dbo].[PRESTAMO]  WITH CHECK ADD FOREIGN KEY([idLibro])
REFERENCES [dbo].[LIBRO] ([idLibro])
GO
ALTER TABLE [dbo].[PRESTAMO]  WITH CHECK ADD FOREIGN KEY([idUsuario])
REFERENCES [dbo].[USUARIO] ([idUsuario])
GO
ALTER TABLE [dbo].[TRAZABILIDAD]  WITH CHECK ADD FOREIGN KEY([idPrestamo])
REFERENCES [dbo].[PRESTAMO] ([idPrestamo])
GO
/****** Object:  StoredProcedure [dbo].[PA_ACTUALIZAR_ESTADO_PRESTAMO]    Script Date: 12/3/2025 00:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PA_ACTUALIZAR_ESTADO_PRESTAMO]
    @IdPrestamo bigint,
    @Estado char(1)
AS
BEGIN
    UPDATE WCS_GESTION_BIBLIOTECA..PRESTAMO 
    SET estado = @Estado
    WHERE idPrestamo = @IdPrestamo
END
GO
/****** Object:  StoredProcedure [dbo].[PA_GENERAR_REPORTE_DIARIO]    Script Date: 12/3/2025 00:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PA_GENERAR_REPORTE_DIARIO] 
    @FechaReporte DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @FechaReporte IS NULL
        SET @FechaReporte = CAST(GETDATE() AS DATE);

    -- Reporte con datos básicos
    SELECT 
        tipoOperacion,
        COUNT(*) AS TotalOperaciones
    FROM Trazabilidad
    WHERE CAST(fechaOperacion AS DATE) = @FechaReporte
    GROUP BY tipoOperacion
    ORDER BY tipoOperacion;
END;
GO
/****** Object:  StoredProcedure [dbo].[PA_OBTENER_PRESTAMOS_VENCIDOS]    Script Date: 12/3/2025 00:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PA_OBTENER_PRESTAMOS_VENCIDOS]
AS
BEGIN
	 SELECT p.*, l.Titulo, u.Nombre, u.PrimerApellido 
    FROM WCS_GESTION_BIBLIOTECA..PRESTAMO p
    INNER JOIN LIBRO l ON p.idLibro = l.idLibro
    INNER JOIN USUARIO u ON p.idUsuario = u.idUsuario
    WHERE CONVERT(DATE, p.fechaDevolucionEsperada) < CONVERT(DATE, GETDATE())
    AND p.estado = 'A' -- Sólo estado A porque no necesito poner en R estados que ya están en R
END
GO
/****** Object:  StoredProcedure [dbo].[PA_REGISTRAR_DEVOLUCION]    Script Date: 12/3/2025 00:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PA para registrar Devoluciones de libros en la trazabilidad
CREATE PROCEDURE [dbo].[PA_REGISTRAR_DEVOLUCION]
    @IdPrestamo INT
AS
BEGIN
    DECLARE @IdLibro INT
    
    
    SELECT @IdLibro = idLibro FROM WCS_GESTION_BIBLIOTECA..PRESTAMO WHERE idPrestamo = @IdPrestamo
    
   
    UPDATE WCS_GESTION_BIBLIOTECA..PRESTAMO 
	Set 
        estado = 'D',
        fechaDevolucionEsperada = GETDATE()
    WHERE idPrestamo = @IdPrestamo
    

    UPDATE WCS_GESTION_BIBLIOTECA..LIBRO SET 
        copiasDisponibles = copiasDisponibles + 1
    WHERE idLibro = @IdLibro
    
    -- Registrar en trazabilidad
    INSERT INTO WCS_GESTION_BIBLIOTECA..Trazabilidad (idPrestamo, tipoOperacion, detalles, fechaOperacion)
    SELECT
        p.idPrestamo,
        'Devolución',
        'Libro ''' + l.titulo + ''' devuelto por ''' + u.nombre + '''. Copia disponible.',
        GETDATE()
    FROM WCS_GESTION_BIBLIOTECA..PRESTAMO p
    JOIN WCS_GESTION_BIBLIOTECA..LIBRO l ON p.IdLibro = l.idLibro
    JOIN WCS_GESTION_BIBLIOTECA..USUARIO u ON p.IdUsuario = u.idUsuario
    WHERE p.idPrestamo = @IdPrestamo
END

GO
/****** Object:  StoredProcedure [dbo].[PA_REGISTRAR_TRAZABILIDAD]    Script Date: 12/3/2025 00:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PA_REGISTRAR_TRAZABILIDAD]
    @IdPrestamo bigint,
    @TipoOperacion varchar(20),
    @Detalles varchar(max)
AS
BEGIN
    INSERT INTO WCS_GESTION_BIBLIOTECA..TRAZABILIDAD(idPrestamo, tipoOperacion, fechaOperacion, detalles)
    VALUES (@IdPrestamo, @TipoOperacion, GETDATE(), @Detalles)
END
GO
/****** Object:  StoredProcedure [dbo].[PA_VERIFICAR_TRAZABILIDAD]    Script Date: 12/3/2025 00:48:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PA_VERIFICAR_TRAZABILIDAD]
    @tipoOperacion NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Obtener el conteo total de registros para el tipo de operación
    SELECT COUNT(*) AS TotalRegistros
    FROM Trazabilidad
    WHERE TipoOperacion = @tipoOperacion;
    
    -- Obtener los detalles del último registro para revisión
    SELECT TOP 1
        T.IdTrazabilidad,
        T.IdPrestamo,
        T.TipoOperacion,
        T.Detalles,
        T.FechaOperacion,
        P.IdLibro,
        P.IdUsuario,
        L.Titulo AS LibroTitulo,
        U.Nombre AS UsuarioNombre,
        U.PrimerApellido AS UsuarioApellido
    FROM Trazabilidad T
    INNER JOIN PRESTAMO P ON T.IdPrestamo = P.IdPrestamo
    INNER JOIN LIBRO L ON P.IdLibro = L.IdLibro
    INNER JOIN USUARIO U ON P.IdUsuario = U.IdUsuario
    WHERE T.TipoOperacion = @tipoOperacion
    ORDER BY T.FechaOperacion DESC;
END
GO
