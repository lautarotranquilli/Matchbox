/****** Object:  Database [profesionales]    Script Date: 23/11/2022 13:28:33 ******/
CREATE DATABASE [profesionales]  (EDITION = 'GeneralPurpose', SERVICE_OBJECTIVE = 'GP_S_Gen5_1', MAXSIZE = 32 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS, LEDGER = OFF;
GO
ALTER DATABASE [profesionales] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [profesionales] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [profesionales] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [profesionales] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [profesionales] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [profesionales] SET ARITHABORT OFF 
GO
ALTER DATABASE [profesionales] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [profesionales] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [profesionales] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [profesionales] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [profesionales] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [profesionales] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [profesionales] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [profesionales] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [profesionales] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [profesionales] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [profesionales] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [profesionales] SET  MULTI_USER 
GO
ALTER DATABASE [profesionales] SET ENCRYPTION ON
GO
ALTER DATABASE [profesionales] SET QUERY_STORE = ON
GO
ALTER DATABASE [profesionales] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Table [dbo].[cliente]    Script Date: 23/11/2022 13:28:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cliente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[telefono] [varchar](10) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[idProvincia] [varchar](50) NOT NULL,
	[idLocalidad] [varchar](50) NOT NULL,
	[fechaAlta] [datetime] NOT NULL,
	[fechaModificacion] [datetime] NULL,
	[fechaBaja] [datetime] NULL,
	[profilePath] [varchar](500) NULL,
 CONSTRAINT [PK_clientes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[contratacion]    Script Date: 23/11/2022 13:28:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contratacion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[idServicio] [int] NOT NULL,
	[fechaAlta] [datetime] NOT NULL,
	[fechaModificacion] [datetime] NOT NULL,
	[fechaBaja] [datetime] NULL,
 CONSTRAINT [PK_contrataciones] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[empresa]    Script Date: 23/11/2022 13:28:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[empresa](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[razonSocial] [varchar](50) NOT NULL,
	[fechaAlta] [datetime] NOT NULL,
	[fechaModificacion] [datetime] NULL,
	[fechaBaja] [datetime] NULL,
	[telefono] [varchar](10) NOT NULL,
	[idProvincia] [varchar](50) NOT NULL,
	[idLocalidad] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[profilePath] [varchar](500) NULL,
 CONSTRAINT [PK_empresas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rubro]    Script Date: 23/11/2022 13:28:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rubro](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[fechaAlta] [datetime] NOT NULL,
	[fechaModificacion] [datetime] NULL,
	[fechaBaja] [datetime] NULL,
 CONSTRAINT [PK_rubros] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[servicio]    Script Date: 23/11/2022 13:28:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[servicio](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idRubro] [int] NOT NULL,
	[idEmpresa] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](1000) NOT NULL,
	[fechaAlta] [datetime] NOT NULL,
	[fechaModificacion] [datetime] NULL,
	[fechaBaja] [datetime] NULL,
 CONSTRAINT [PK_servicios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 23/11/2022 13:28:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[fechaAlta] [datetime] NOT NULL,
	[fechaModificacion] [datetime] NULL,
	[fechaBaja] [datetime] NULL,
	[email] [varchar](50) NULL,
	[contrasena] [varchar](50) NULL,
	[reingresarContrasena] [varchar](50) NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[valoracion]    Script Date: 23/11/2022 13:28:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[valoracion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[idEmpresa] [int] NOT NULL,
	[tipo] [int] NOT NULL,
	[puntaje] [int] NOT NULL,
	[descripcion] [varchar](50) NULL,
	[fechaAlta] [datetime] NOT NULL,
	[fechaBaja] [datetime] NULL,
 CONSTRAINT [PK_valoraciones] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[cliente] ON 

INSERT [dbo].[cliente] ([id], [idUsuario], [nombre], [apellido], [telefono], [email], [idProvincia], [idLocalidad], [fechaAlta], [fechaModificacion], [fechaBaja], [profilePath]) VALUES (2, 2, N'Juan', N'Lider', N'dd', N'mathisupino@gmail.com', N'06', N'06588030000', CAST(N'2022-01-02T00:00:00.000' AS DateTime), CAST(N'2022-09-26T22:31:52.283' AS DateTime), CAST(N'2022-09-26T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[cliente] ([id], [idUsuario], [nombre], [apellido], [telefono], [email], [idProvincia], [idLocalidad], [fechaAlta], [fechaModificacion], [fechaBaja], [profilePath]) VALUES (6, 2, N'Gonzalo', N'Donda', N'123', N'mama1', N'mama2', N'mama3', CAST(N'2022-08-19T00:00:00.000' AS DateTime), CAST(N'2022-08-19T13:30:20.713' AS DateTime), CAST(N'2022-09-20T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[cliente] ([id], [idUsuario], [nombre], [apellido], [telefono], [email], [idProvincia], [idLocalidad], [fechaAlta], [fechaModificacion], [fechaBaja], [profilePath]) VALUES (7, 2, N'Florencia', N'Peña', N'1425367890', N'mmm@gmail.com', N'06', N'06854100000', CAST(N'2022-08-21T00:00:00.000' AS DateTime), CAST(N'2022-09-20T12:04:34.320' AS DateTime), CAST(N'2022-09-20T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[cliente] ([id], [idUsuario], [nombre], [apellido], [telefono], [email], [idProvincia], [idLocalidad], [fechaAlta], [fechaModificacion], [fechaBaja], [profilePath]) VALUES (8, 2, N'Marcela', N'M', N'1425789630', N'iiii@gmailc.om', N'asd', N'qwe', CAST(N'2022-08-21T00:00:00.000' AS DateTime), CAST(N'2022-08-21T17:31:57.097' AS DateTime), CAST(N'2022-09-20T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[cliente] ([id], [idUsuario], [nombre], [apellido], [telefono], [email], [idProvincia], [idLocalidad], [fechaAlta], [fechaModificacion], [fechaBaja], [profilePath]) VALUES (10, 2, N'Paola', N'Paola', N'1425367896', N'iiii@iiii.com', N'26', N'26084020000', CAST(N'2022-08-28T00:00:00.000' AS DateTime), CAST(N'2022-09-28T20:58:24.407' AS DateTime), NULL, N'c1030b2e-9464-4e9a-b68c-972c6499f91e_Steam.jpg')
INSERT [dbo].[cliente] ([id], [idUsuario], [nombre], [apellido], [telefono], [email], [idProvincia], [idLocalidad], [fechaAlta], [fechaModificacion], [fechaBaja], [profilePath]) VALUES (11, 14, N'Gabriel', N'abc', N'1425367896', N'iiii@iiii.com', N'10', N'10098010000', CAST(N'2022-08-31T00:00:00.000' AS DateTime), CAST(N'2022-08-31T01:17:10.363' AS DateTime), CAST(N'2022-09-20T00:00:00.000' AS DateTime), N'7077450a-8f96-421a-aed6-12580a365065_d4c530048ac30c3cae46259a7b037004.jpg')
INSERT [dbo].[cliente] ([id], [idUsuario], [nombre], [apellido], [telefono], [email], [idProvincia], [idLocalidad], [fechaAlta], [fechaModificacion], [fechaBaja], [profilePath]) VALUES (12, 14, N'jjj', N'eeee', N'1234567891', N'ggg@hhh.com', N'22', N'22070020000', CAST(N'2022-08-31T00:00:00.000' AS DateTime), CAST(N'2022-09-27T19:54:02.250' AS DateTime), CAST(N'2022-09-27T00:00:00.000' AS DateTime), N'7c13795e-8415-43ba-a170-646e55d4510c_WrjUfYljLV.png')
INSERT [dbo].[cliente] ([id], [idUsuario], [nombre], [apellido], [telefono], [email], [idProvincia], [idLocalidad], [fechaAlta], [fechaModificacion], [fechaBaja], [profilePath]) VALUES (13, 16, N'ARbol', N'Arbol', N'123456789', N'hola@gmail.com', N'42', N'42105010000', CAST(N'2022-08-31T00:00:00.000' AS DateTime), CAST(N'2022-09-20T12:25:49.670' AS DateTime), CAST(N'2022-08-31T00:00:00.000' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[empresa] ON 

INSERT [dbo].[empresa] ([id], [idUsuario], [razonSocial], [fechaAlta], [fechaModificacion], [fechaBaja], [telefono], [idProvincia], [idLocalidad], [email], [profilePath]) VALUES (1, 2, N'Lautaro', CAST(N'2022-09-26T00:00:00.000' AS DateTime), CAST(N'2022-09-27T19:49:01.733' AS DateTime), CAST(N'2022-09-27T00:00:00.000' AS DateTime), N'3564608163', N'14', N'14182010000', N'lautaro@gmail.com', N'8010e702-c1a1-48f0-b3da-85c19055ec38_Desert.jpg')
INSERT [dbo].[empresa] ([id], [idUsuario], [razonSocial], [fechaAlta], [fechaModificacion], [fechaBaja], [telefono], [idProvincia], [idLocalidad], [email], [profilePath]) VALUES (2, 2, N'Madera', CAST(N'2022-09-26T00:00:00.000' AS DateTime), CAST(N'2022-09-26T20:42:55.800' AS DateTime), NULL, N'3564608163', N'26', N'26042040000', N'lautaroj@gmail.com', N'3a1e4674-49b9-4fd9-8a91-991048ef7af0_Koala.jpg')
INSERT [dbo].[empresa] ([id], [idUsuario], [razonSocial], [fechaAlta], [fechaModificacion], [fechaBaja], [telefono], [idProvincia], [idLocalidad], [email], [profilePath]) VALUES (3, 17, N'JHierro', CAST(N'2022-09-26T00:00:00.000' AS DateTime), CAST(N'2022-11-02T18:44:26.290' AS DateTime), NULL, N'3564612345', N'14', N'14119010000', N'lauti@gmail.com', NULL)
INSERT [dbo].[empresa] ([id], [idUsuario], [razonSocial], [fechaAlta], [fechaModificacion], [fechaBaja], [telefono], [idProvincia], [idLocalidad], [email], [profilePath]) VALUES (4, 16, N'Arcor', CAST(N'2022-09-27T00:00:00.000' AS DateTime), CAST(N'2022-09-28T21:11:57.780' AS DateTime), NULL, N'3564778899', N'06', N'06588030000', N'admin@admin.com', NULL)
INSERT [dbo].[empresa] ([id], [idUsuario], [razonSocial], [fechaAlta], [fechaModificacion], [fechaBaja], [telefono], [idProvincia], [idLocalidad], [email], [profilePath]) VALUES (5, 19, N'Empresa', CAST(N'2022-09-28T00:00:00.000' AS DateTime), CAST(N'2022-09-28T21:21:40.083' AS DateTime), CAST(N'2022-09-28T00:00:00.000' AS DateTime), N'3564608153', N'22', N'22070010000', N'josemario@email.com', NULL)
SET IDENTITY_INSERT [dbo].[empresa] OFF
GO
SET IDENTITY_INSERT [dbo].[rubro] ON 

INSERT [dbo].[rubro] ([id], [nombre], [fechaAlta], [fechaModificacion], [fechaBaja]) VALUES (1, N'Reformas', CAST(N'2022-01-01T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[rubro] ([id], [nombre], [fechaAlta], [fechaModificacion], [fechaBaja]) VALUES (2, N'Jardinería', CAST(N'2022-01-01T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[rubro] ([id], [nombre], [fechaAlta], [fechaModificacion], [fechaBaja]) VALUES (3, N'Mascotas', CAST(N'2022-01-01T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[rubro] ([id], [nombre], [fechaAlta], [fechaModificacion], [fechaBaja]) VALUES (6, N'Vehículos', CAST(N'2022-01-01T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[rubro] ([id], [nombre], [fechaAlta], [fechaModificacion], [fechaBaja]) VALUES (7, N'Cuidado personal', CAST(N'2022-01-01T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[rubro] ([id], [nombre], [fechaAlta], [fechaModificacion], [fechaBaja]) VALUES (8, N'Eventos sociales', CAST(N'2022-01-01T00:00:00.000' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[rubro] OFF
GO
SET IDENTITY_INSERT [dbo].[servicio] ON 

INSERT [dbo].[servicio] ([id], [idRubro], [idEmpresa], [nombre], [descripcion], [fechaAlta], [fechaModificacion], [fechaBaja]) VALUES (1, 1, 4, N'Albañilería general', N'Albañil, durlock, cielorrasos, yeso impermeabilizador, mantenimiento piletas.', CAST(N'2022-10-28T00:00:00.000' AS DateTime), CAST(N'2022-10-28T17:52:23.643' AS DateTime), NULL)
INSERT [dbo].[servicio] ([id], [idRubro], [idEmpresa], [nombre], [descripcion], [fechaAlta], [fechaModificacion], [fechaBaja]) VALUES (2, 1, 4, N'Electricidad en hogares', N'Electricidad, calefacción, herrero, soldador, pintor, plomero, reparación electrodomésticos.', CAST(N'2022-10-28T00:00:00.000' AS DateTime), CAST(N'2022-11-02T20:20:17.330' AS DateTime), NULL)
INSERT [dbo].[servicio] ([id], [idRubro], [idEmpresa], [nombre], [descripcion], [fechaAlta], [fechaModificacion], [fechaBaja]) VALUES (3, 2, 4, N'Jardinería de interiores', N'Jardinero, desmalezador, jardín y exteriores.', CAST(N'2022-10-28T00:00:00.000' AS DateTime), CAST(N'2022-10-28T18:01:24.397' AS DateTime), NULL)
INSERT [dbo].[servicio] ([id], [idRubro], [idEmpresa], [nombre], [descripcion], [fechaAlta], [fechaModificacion], [fechaBaja]) VALUES (7, 3, 3, N'Peludog', N'Peluquería y baño de perros.', CAST(N'2022-11-02T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[servicio] ([id], [idRubro], [idEmpresa], [nombre], [descripcion], [fechaAlta], [fechaModificacion], [fechaBaja]) VALUES (8, 8, 3, N'Salón de fiestas', N'Salones para cumpleaños, alquiler de mesas y tablones.', CAST(N'2022-11-02T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[servicio] ([id], [idRubro], [idEmpresa], [nombre], [descripcion], [fechaAlta], [fechaModificacion], [fechaBaja]) VALUES (9, 6, 3, N'Electromecánica Centro', N'Reparación y electricidad en vehículos.', CAST(N'2022-11-02T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[servicio] ([id], [idRubro], [idEmpresa], [nombre], [descripcion], [fechaAlta], [fechaModificacion], [fechaBaja]) VALUES (10, 7, 3, N'Malika', N'Estética facial, manicura y pedicura.', CAST(N'2022-11-02T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[servicio] ([id], [idRubro], [idEmpresa], [nombre], [descripcion], [fechaAlta], [fechaModificacion], [fechaBaja]) VALUES (11, 3, 3, N'Cuatro Patas', N'Alimentos balanceados y accesorios para mascotas.', CAST(N'2022-11-02T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[servicio] ([id], [idRubro], [idEmpresa], [nombre], [descripcion], [fechaAlta], [fechaModificacion], [fechaBaja]) VALUES (12, 3, 4, N'Paseo', N'Paseo de perros', CAST(N'2022-11-02T00:00:00.000' AS DateTime), CAST(N'2022-11-02T21:08:39.297' AS DateTime), CAST(N'2022-11-02T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[servicio] OFF
GO
SET IDENTITY_INSERT [dbo].[usuario] ON 

INSERT [dbo].[usuario] ([id], [nombre], [apellido], [fechaAlta], [fechaModificacion], [fechaBaja], [email], [contrasena], [reingresarContrasena]) VALUES (2, N'prueba', N'uno', CAST(N'2022-08-14T00:00:00.000' AS DateTime), CAST(N'2022-08-14T00:00:00.000' AS DateTime), NULL, N'abc@abc.com', N'abc', N'abc')
INSERT [dbo].[usuario] ([id], [nombre], [apellido], [fechaAlta], [fechaModificacion], [fechaBaja], [email], [contrasena], [reingresarContrasena]) VALUES (10, N'Marina', N'Perez', CAST(N'2022-08-30T00:00:00.000' AS DateTime), CAST(N'2022-08-30T00:00:00.000' AS DateTime), NULL, N'mperez@google.com', N'123', N'123       ')
INSERT [dbo].[usuario] ([id], [nombre], [apellido], [fechaAlta], [fechaModificacion], [fechaBaja], [email], [contrasena], [reingresarContrasena]) VALUES (12, N'Mathias', N'abc', CAST(N'2022-08-30T00:00:00.000' AS DateTime), CAST(N'2022-08-30T22:06:03.453' AS DateTime), NULL, N'mathisupino@gmail.com', N'142536', N'142536    ')
INSERT [dbo].[usuario] ([id], [nombre], [apellido], [fechaAlta], [fechaModificacion], [fechaBaja], [email], [contrasena], [reingresarContrasena]) VALUES (13, N'Gabriel', N'Argento', CAST(N'2022-08-30T00:00:00.000' AS DateTime), CAST(N'2022-08-30T22:07:43.557' AS DateTime), NULL, N'mathisupino@gmail.com', N'142536', N'142536    ')
INSERT [dbo].[usuario] ([id], [nombre], [apellido], [fechaAlta], [fechaModificacion], [fechaBaja], [email], [contrasena], [reingresarContrasena]) VALUES (14, N'Tatiana', N'Tatian', CAST(N'2022-08-30T00:00:00.000' AS DateTime), CAST(N'2022-08-30T23:24:47.523' AS DateTime), NULL, N'tat@iana.com', N'123', N'123       ')
INSERT [dbo].[usuario] ([id], [nombre], [apellido], [fechaAlta], [fechaModificacion], [fechaBaja], [email], [contrasena], [reingresarContrasena]) VALUES (16, N'Pruebas', N'Sistema', CAST(N'2022-01-01T00:00:00.000' AS DateTime), CAST(N'2022-01-01T00:00:00.000' AS DateTime), NULL, N'admin@admin.com', N'admin', N'admin     ')
INSERT [dbo].[usuario] ([id], [nombre], [apellido], [fechaAlta], [fechaModificacion], [fechaBaja], [email], [contrasena], [reingresarContrasena]) VALUES (17, N'Lautaro', N'Tranquilli', CAST(N'2022-08-31T00:00:00.000' AS DateTime), CAST(N'2022-08-31T14:30:33.883' AS DateTime), NULL, N'lauti@gmail.com', N'Lautaro00', N'Lautaro00 ')
INSERT [dbo].[usuario] ([id], [nombre], [apellido], [fechaAlta], [fechaModificacion], [fechaBaja], [email], [contrasena], [reingresarContrasena]) VALUES (18, N'Jose', N'Mario', CAST(N'2022-08-31T00:00:00.000' AS DateTime), CAST(N'2022-08-31T14:37:55.063' AS DateTime), NULL, N'josemario@email.com', N'Lautaro08', N'Lautaro08 ')
INSERT [dbo].[usuario] ([id], [nombre], [apellido], [fechaAlta], [fechaModificacion], [fechaBaja], [email], [contrasena], [reingresarContrasena]) VALUES (19, N'Marina', N'Mario', CAST(N'2022-08-31T00:00:00.000' AS DateTime), CAST(N'2022-08-31T19:32:47.633' AS DateTime), NULL, N'josemario@email.com', N'Lautaro80', N'Lautaro8052')
INSERT [dbo].[usuario] ([id], [nombre], [apellido], [fechaAlta], [fechaModificacion], [fechaBaja], [email], [contrasena], [reingresarContrasena]) VALUES (21, N'tata', N'tata', CAST(N'2022-08-31T00:00:00.000' AS DateTime), CAST(N'2022-08-31T21:42:14.143' AS DateTime), NULL, N'tata@tata.com', N'Jajajaja90', N'Jajajaja90')
SET IDENTITY_INSERT [dbo].[usuario] OFF
GO
ALTER TABLE [dbo].[cliente]  WITH CHECK ADD  CONSTRAINT [FK_clientes_usuarios] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[cliente] CHECK CONSTRAINT [FK_clientes_usuarios]
GO
ALTER TABLE [dbo].[contratacion]  WITH CHECK ADD  CONSTRAINT [FK_contrataciones_servicios] FOREIGN KEY([idServicio])
REFERENCES [dbo].[servicio] ([id])
GO
ALTER TABLE [dbo].[contratacion] CHECK CONSTRAINT [FK_contrataciones_servicios]
GO
ALTER TABLE [dbo].[contratacion]  WITH CHECK ADD  CONSTRAINT [FK_contrataciones_usuarios] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[contratacion] CHECK CONSTRAINT [FK_contrataciones_usuarios]
GO
ALTER TABLE [dbo].[servicio]  WITH CHECK ADD  CONSTRAINT [FK_servicios_empresas] FOREIGN KEY([idEmpresa])
REFERENCES [dbo].[empresa] ([id])
GO
ALTER TABLE [dbo].[servicio] CHECK CONSTRAINT [FK_servicios_empresas]
GO
ALTER TABLE [dbo].[servicio]  WITH CHECK ADD  CONSTRAINT [FK_servicios_rubros] FOREIGN KEY([idRubro])
REFERENCES [dbo].[rubro] ([id])
GO
ALTER TABLE [dbo].[servicio] CHECK CONSTRAINT [FK_servicios_rubros]
GO
ALTER TABLE [dbo].[valoracion]  WITH CHECK ADD  CONSTRAINT [FK_valoraciones_empresas] FOREIGN KEY([idEmpresa])
REFERENCES [dbo].[empresa] ([id])
GO
ALTER TABLE [dbo].[valoracion] CHECK CONSTRAINT [FK_valoraciones_empresas]
GO
ALTER TABLE [dbo].[valoracion]  WITH CHECK ADD  CONSTRAINT [FK_valoraciones_usuarios] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[valoracion] CHECK CONSTRAINT [FK_valoraciones_usuarios]
GO
ALTER DATABASE [profesionales] SET  READ_WRITE 
GO
