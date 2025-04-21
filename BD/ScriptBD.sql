CREATE DATABASE Biblioteca;
GO

USE Biblioteca;
GO

CREATE TABLE TipoMaterial (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL UNIQUE,
    Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE Material (
    Id INT PRIMARY KEY IDENTITY(1,1),
	NroIdentificacion NVARCHAR(255) NOT NULL,
    Titulo NVARCHAR(255) NOT NULL,
    FechaRegistro DATE NOT NULL,
    CantidadRegistrada INT NOT NULL,
    CantidadActual INT NOT NULL,
    TipoMaterialId INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (TipoMaterialId) REFERENCES TipoMaterial(Id)
);

CREATE TABLE Rol (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL UNIQUE,
    Estado BIT NOT NULL DEFAULT 1
);

CREATE TABLE Usuario (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(255) NOT NULL,
    Cedula NVARCHAR(50) NOT NULL UNIQUE,
    RolId INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (RolId) REFERENCES Rol(Id)
);

CREATE TABLE Prestamo (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioId INT NOT NULL,
    MaterialId INT NOT NULL,
    FechaPrestamo DATE NOT NULL,
	CantidadPrestada INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id),
    FOREIGN KEY (MaterialId) REFERENCES Material(Id)
);

CREATE TABLE Devolucion (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PrestamoId INT NOT NULL,
    FechaDevolucion DATE NOT NULL,
    FOREIGN KEY (PrestamoId) REFERENCES Prestamo(Id)
);

INSERT INTO [Biblioteca].[dbo].[Rol] ([Nombre], [Estado])
VALUES 
    ('Estudiante', 1),
    ('Profesor', 1),
    ('Administrador', 1);



INSERT INTO [Biblioteca].[dbo].[TipoMaterial] ([Nombre], [Estado])
VALUES
    ('Libro', 1),
    ('Revista', 1),
    ('Material Audiovisual', 1);

