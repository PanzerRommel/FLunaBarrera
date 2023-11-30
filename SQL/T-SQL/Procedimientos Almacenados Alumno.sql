-- Procedimiento para agregar un nuevo alumno
CREATE PROCEDURE AlumnoAdd
    @Nombre VARCHAR(100),
    @NumeroBoleta VARCHAR(100),
    @UserName VARCHAR(100),
    @Password VARCHAR(256),
    @Email VARCHAR(256),
    @Telefono VARCHAR(100)
AS
BEGIN
    INSERT INTO Alumno (Nombre, NumeroBoleta, UserName, Password, Email, Telefono)
    VALUES (@Nombre, @NumeroBoleta, @UserName, @Password, @Email, @Telefono);
END;

-- Procedimiento para actualizar un alumno existente
CREATE PROCEDURE AlumnoUpdate
    @IdAlumno INT,
    @Nombre VARCHAR(100),
    @NumeroBoleta VARCHAR(100),
    @UserName VARCHAR(100),
    @Password VARCHAR(256),
    @Email VARCHAR(256),
    @Telefono VARCHAR(100)
AS
BEGIN
    UPDATE Alumno
    SET Nombre = @Nombre,
        NumeroBoleta = @NumeroBoleta,
        UserName = @UserName,
        Password = @Password,
        Email = @Email,
        Telefono = @Telefono
    WHERE IdAlumno = @IdAlumno;
END;

-- Procedimiento para eliminar un alumno por ID
CREATE PROCEDURE AlumnoDelete
    @IdAlumno INT
AS
BEGIN
    DELETE FROM Alumno
    WHERE IdAlumno = @IdAlumno;
END;

-- Procedimiento para obtener todos los alumnos
CREATE PROCEDURE AlumnoGetAll
AS
BEGIN
    SELECT IdAlumno, Nombre, NumeroBoleta, UserName, Email, Telefono FROM Alumno;
END;

-- Procedimiento para obtener un alumno por ID
CREATE PROCEDURE AlumnoGetById
    @IdAlumno INT
AS
BEGIN
    SELECT IdAlumno, Nombre, NumeroBoleta, UserName, Email, Telefono FROM Alumno
    WHERE IdAlumno = @IdAlumno;
END;
