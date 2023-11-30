-- Procedimiento para agregar un nuevo profesor
CREATE PROCEDURE ProfesorAdd
    @Nombre VARCHAR(100),
    @Email VARCHAR(200),
    @Usuario VARCHAR(100),
    @Password VARCHAR(256),
    @Telefono VARCHAR(100)
AS
BEGIN
    INSERT INTO Profesor (Nombre, Email, Usuario, Password, Telefono)
    VALUES (@Nombre, @Email, @Usuario, @Password, @Telefono);
END;

-- Procedimiento para actualizar un profesor existente
CREATE PROCEDURE ProfesorUpdate
    @IdProfesor INT,
    @Nombre VARCHAR(100),
    @Email VARCHAR(200),
    @Usuario VARCHAR(100),
    @Password VARCHAR(256),
    @Telefono VARCHAR(100)
AS
BEGIN
    UPDATE Profesor
    SET Nombre = @Nombre,
        Email = @Email,
        Usuario = @Usuario,
        Password = @Password,
        Telefono = @Telefono
    WHERE IdProfesor = @IdProfesor;
END;

-- Procedimiento para eliminar un profesor por ID
CREATE PROCEDURE ProfesorDelete
    @IdProfesor INT
AS
BEGIN
    DELETE FROM Profesor
    WHERE IdProfesor = @IdProfesor;
END;

-- Procedimiento para obtener todos los profesores
CREATE PROCEDURE ProfesorGetAll
AS
BEGIN
    SELECT IdProfesor, Nombre, Email, Usuario, Telefono FROM Profesor;
END;

-- Procedimiento para obtener un profesor por ID
CREATE PROCEDURE ProfesorGetById
    @IdProfesor INT
AS
BEGIN
    SELECT IdProfesor, Nombre, Email, Usuario, Telefono FROM Profesor
    WHERE IdProfesor = @IdProfesor;
END;
