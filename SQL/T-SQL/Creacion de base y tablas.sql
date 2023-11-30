CREATE DATABASE FLunaBarrera;

CREATE TABLE Materia (
    IdMateria INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(200)
);

CREATE TABLE Profesor (
    IdProfesor INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Email VARCHAR(200) NOT NULL,
    Usuario VARCHAR(100) NOT NULL,
    Password VARCHAR(256) NOT NULL,
    Telefono VARCHAR(100) NOT NULL
);

CREATE TABLE Alumno (
    IdAlumno INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    NumeroBoleta VARCHAR(100) UNIQUE NOT NULL,
	UserName VARCHAR(100) NOT NULL,
	Password VARCHAR(256) NOT NULL,
	Email VARCHAR(256) NOT NULL,
	Telefono VARCHAR(100) NOT NULL
);

CREATE TABLE Grupo (
    IdGrupo INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(100) NOT NULL,
    IdMateria INT,
    IdProfesor INT,
    FOREIGN KEY (IdMateria) REFERENCES Materia(IdMateria),
    FOREIGN KEY (IdProfesor) REFERENCES Profesor(IdProfesor)
);

CREATE TABLE Calificacion (
    IdCalificacion INT PRIMARY KEY IDENTITY(1,1),
    IdAlumno INT,
    IdGrupo INT,
	IdMateria INT,
	IdProfesor INT,
    Calificacion DECIMAL(10,2),
    FOREIGN KEY (IdAlumno) REFERENCES Alumno(IdAlumno),
    FOREIGN KEY (IdGrupo) REFERENCES Grupo(IdGrupo),
	FOREIGN KEY (IdMateria) REFERENCES Materia(IdMateria),
	FOREIGN KEY (IdProfesor) REFERENCES Profesor(IdProfesor)
);
