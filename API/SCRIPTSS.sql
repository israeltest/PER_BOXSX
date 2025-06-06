CREATE DATABASE PRUEBASOLIC2
USE [PRUEBASOLIC2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoSolicitud](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Solicitudes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](200) NOT NULL,
	[DireccionSolicitante] [nvarchar](300) NOT NULL,
	[TipoCompraId] [int] NOT NULL,
	[Descripcion] [nvarchar](500) NOT NULL,
	[FechaSolicitud] [datetime] NOT NULL,
	[EstadoId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TipoCompra](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Solicitudes] ADD  DEFAULT (getdate()) FOR [FechaSolicitud]
GO
ALTER TABLE [dbo].[Solicitudes]  WITH CHECK ADD FOREIGN KEY([EstadoId])
REFERENCES [dbo].[EstadoSolicitud] ([Id])
GO
ALTER TABLE [dbo].[Solicitudes]  WITH CHECK ADD FOREIGN KEY([TipoCompraId])
REFERENCES [dbo].[TipoCompra] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarSolicitud]    Script Date: 11/5/2025 22:53:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ActualizarSolicitud]
    @Id INT,
    @Nombre NVARCHAR(200),
    @DireccionSolicitante NVARCHAR(300),
    @TipoCompraId INT,
    @Descripcion NVARCHAR(500),
    @EstadoId INT
AS
BEGIN
    UPDATE Solicitudes
    SET Nombre = @Nombre,
        DireccionSolicitante = @DireccionSolicitante,
        TipoCompraId = @TipoCompraId,
        Descripcion = @Descripcion,
        EstadoId = @EstadoId,
        FechaSolicitud = GETDATE()
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerSolicitudes]    Script Date: 11/5/2025 22:53:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ObtenerSolicitudes]
AS
BEGIN
SELECT 
        s.Id,
        s.Nombre,
        s.DireccionSolicitante,
        s.Descripcion,
        s.FechaSolicitud,
        s.TipoCompraId,                     
        tc.Id AS TipoCompraId,             
        tc.Descripcion AS TipoCompraDescripcion,
        s.EstadoId,                         
        es.Id AS EstadoId,                  
        es.Descripcion AS EstadoDescripcion
    FROM Solicitudes s
    INNER JOIN TipoCompra tc ON s.TipoCompraId = tc.Id
    INNER JOIN EstadoSolicitud es ON s.EstadoId = es.Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerSolicitudPorId]    Script Date: 11/5/2025 22:53:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerSolicitudPorId]
    @SolicitudId INT
AS
BEGIN
    SELECT 
        s.Id,
        s.Nombre,
        s.DireccionSolicitante,
        s.Descripcion,
        s.FechaSolicitud,
        s.TipoCompraId,
        tc.Id AS TipoCompraId,
        tc.Descripcion AS TipoCompraDescripcion,
        s.EstadoId,
        es.Id AS EstadoId,
        es.Descripcion AS EstadoDescripcion
    FROM Solicitudes s
    INNER JOIN TipoCompra tc ON s.TipoCompraId = tc.Id
    INNER JOIN EstadoSolicitud es ON s.EstadoId = es.Id
    WHERE s.Id = @SolicitudId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RegistrarSolicitud]    Script Date: 11/5/2025 22:53:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_RegistrarSolicitud]
    @Nombre NVARCHAR(200),
    @DireccionSolicitante NVARCHAR(300),
    @TipoCompraId INT,
    @Descripcion NVARCHAR(500),
    @EstadoId INT
AS
BEGIN
    INSERT INTO Solicitudes (Nombre, DireccionSolicitante, TipoCompraId, Descripcion, FechaSolicitud, EstadoId)
    VALUES (@Nombre, @DireccionSolicitante, @TipoCompraId, @Descripcion, GETDATE(), @EstadoId);
END
GO
