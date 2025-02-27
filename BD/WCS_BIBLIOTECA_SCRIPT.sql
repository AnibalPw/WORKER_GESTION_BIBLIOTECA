USE [WCS_GESTION_BIBLIOTECA]
GO
/****** Object:  Table [dbo].[LIBRO]    Script Date: 19/2/2025 23:27:08 ******/
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
/****** Object:  Table [dbo].[PRESTAMO]    Script Date: 19/2/2025 23:27:08 ******/
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
/****** Object:  Table [dbo].[TRAZABILIDAD]    Script Date: 19/2/2025 23:27:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRAZABILIDAD](
	[idHistorial] [bigint] IDENTITY(1,1) NOT NULL,
	[idPrestamo] [bigint] NULL,
	[tipoOperacion] [varchar](20) NULL,
	[fechaOperacion] [datetime] NULL,
	[detalles] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[idHistorial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIO]    Script Date: 19/2/2025 23:27:08 ******/
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

INSERT [dbo].[TRAZABILIDAD] ([idHistorial], [idPrestamo], [tipoOperacion], [fechaOperacion], [detalles]) VALUES (1, 1, N'Prestamo', CAST(N'2025-02-14T22:46:14.750' AS DateTime), N'Préstamo realizado correctamente')
INSERT [dbo].[TRAZABILIDAD] ([idHistorial], [idPrestamo], [tipoOperacion], [fechaOperacion], [detalles]) VALUES (2, 2, N'Prestamo', CAST(N'2025-02-16T22:46:14.750' AS DateTime), N'Préstamo realizado correctamente')
INSERT [dbo].[TRAZABILIDAD] ([idHistorial], [idPrestamo], [tipoOperacion], [fechaOperacion], [detalles]) VALUES (3, 3, N'Prestamo', CAST(N'2025-02-04T22:46:14.750' AS DateTime), N'Préstamo realizado correctamente')
INSERT [dbo].[TRAZABILIDAD] ([idHistorial], [idPrestamo], [tipoOperacion], [fechaOperacion], [detalles]) VALUES (4, 4, N'Prestamo', CAST(N'2025-01-30T22:46:14.750' AS DateTime), N'Préstamo realizado correctamente')
INSERT [dbo].[TRAZABILIDAD] ([idHistorial], [idPrestamo], [tipoOperacion], [fechaOperacion], [detalles]) VALUES (5, 5, N'Prestamo', CAST(N'2025-02-09T22:46:14.750' AS DateTime), N'Préstamo realizado correctamente')
INSERT [dbo].[TRAZABILIDAD] ([idHistorial], [idPrestamo], [tipoOperacion], [fechaOperacion], [detalles]) VALUES (6, 5, N'Devolución', CAST(N'2025-02-17T22:46:14.750' AS DateTime), N'Libro devuelto en buen estado')
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
/****** Object:  StoredProcedure [dbo].[PA_ACTUALIZAR_ESTADO_PRESTAMO]    Script Date: 19/2/2025 23:27:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PA_ACTUALIZAR_ESTADO_PRESTAMO] --SP_ActualizarEstadoPrestamo
    @IdPrestamo bigint,
    @Estado char(1)
AS
BEGIN
    UPDATE PRESTAMO 
    SET estado = @Estado
    WHERE idPrestamo = @IdPrestamo
END

GO
/****** Object:  StoredProcedure [dbo].[PA_OBTENER_PRESTAMOS_VENCIDOS]    Script Date: 19/2/2025 23:27:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PA_OBTENER_PRESTAMOS_VENCIDOS] --SP_ObtenerPrestamosVencidos
AS
BEGIN
    SELECT p.*, l.Titulo, u.Nombre, u.PrimerApellido 
    FROM PRESTAMO p
    INNER JOIN LIBRO l ON p.idLibro = l.idLibro
    INNER JOIN USUARIO u ON p.idUsuario = u.idUsuario
    WHERE p.fechaDevolucionEsperada < GETDATE() 
    AND p.estado = 'A'
END

GO
/****** Object:  StoredProcedure [dbo].[PA_REGISTRAR_TRAZABILIDAD]    Script Date: 19/2/2025 23:27:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PA_REGISTRAR_TRAZABILIDAD] -- SP_RegistrarHistorial
    @IdPrestamo bigint,
    @TipoOperacion varchar(20),
    @Detalles varchar(max)
AS
BEGIN
    INSERT INTO TRAZABILIDAD(idPrestamo, tipoOperacion, fechaOperacion, detalles)
    VALUES (@IdPrestamo, @TipoOperacion, GETDATE(), @Detalles)
END

GO
