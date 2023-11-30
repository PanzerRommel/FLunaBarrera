--Procedimientos Almacenados para la tabla Inscripciones--

-- Procedimiento almacenado para agregar una nueva inscripción
CREATE PROCEDURE InscripcionAdd
    @IdGrupo INT,
    @IdAlumno INT,
    @IdMateria INT
AS
BEGIN
    INSERT INTO Inscripciones (IdGrupo, IdAlumno, IdMateria)
    VALUES (@IdGrupo, @IdAlumno, @IdMateria);
END;

-- Procedimiento almacenado para actualizar una inscripción existente por su ID
CREATE PROCEDURE InscripcionUpdate
    @IdInscripcion INT,
    @IdGrupo INT,
    @IdAlumno INT,
    @IdMateria INT
AS
BEGIN
    UPDATE Inscripciones
    SET
        IdGrupo = @IdGrupo,
        IdAlumno = @IdAlumno,
        IdMateria = @IdMateria
    WHERE
        IdInscripcion = @IdInscripcion;
END;

-- Procedimiento almacenado para eliminar una inscripción por su ID
CREATE PROCEDURE InscripcionDelete
    @IdInscripcion INT
AS
BEGIN
    DELETE FROM Inscripciones
    WHERE
        IdInscripcion = @IdInscripcion;
END;

-- Procedimiento almacenado para obtener todas las inscripciones con nombres de alumno, grupo y materia
CREATE PROCEDURE InscripcionGetAll
AS
BEGIN
    SELECT
        I.IdInscripcion,
        I.IdGrupo,
        G.IdMateria,
        M.Nombre AS NombreMateria,
        G.IdProfesor,
        P.Nombre AS NombreProfesor,
        I.IdAlumno,
        A.Nombre AS NombreAlumno
    FROM
        Inscripciones I
        INNER JOIN Grupo G ON I.IdGrupo = G.IdGrupo
        INNER JOIN Materia M ON G.IdMateria = M.IdMateria
        INNER JOIN Profesor P ON G.IdProfesor = P.IdProfesor
        INNER JOIN Alumno A ON I.IdAlumno = A.IdAlumno;
END;

-- Procedimiento almacenado para obtener una inscripción por su ID con nombres de alumno, grupo y materia
CREATE PROCEDURE InscripcionGetById
    @IdInscripcion INT
AS
BEGIN
    SELECT
        I.IdInscripcion,
        I.IdGrupo,
        G.IdMateria,
        M.Nombre AS NombreMateria,
        G.IdProfesor,
        P.Nombre AS NombreProfesor,
        I.IdAlumno,
        A.Nombre AS NombreAlumno
    FROM
        Inscripciones I
        INNER JOIN Grupo G ON I.IdGrupo = G.IdGrupo
        INNER JOIN Materia M ON G.IdMateria = M.IdMateria
        INNER JOIN Profesor P ON G.IdProfesor = P.IdProfesor
        INNER JOIN Alumno A ON I.IdAlumno = A.IdAlumno
    WHERE
        I.IdInscripcion = @IdInscripcion;
END;
