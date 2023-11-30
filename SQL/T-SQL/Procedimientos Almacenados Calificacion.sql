-- Procedimiento para agregar una nueva calificación
CREATE PROCEDURE CalificacionAdd
    @IdAlumno INT,
    @IdGrupo INT,
    @IdMateria INT,
	@IdProfesor INT,
    @Calificacion DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Calificacion (IdAlumno, IdGrupo, IdMateria, IdProfesor, Calificacion)
    VALUES (@IdAlumno, @IdGrupo, @IdMateria, @IdProfesor, @Calificacion);
END;

-- Procedimiento para actualizar una calificación existente
CREATE PROCEDURE CalificacionUpdate
    @IdCalificacion INT,
    @IdAlumno INT,
    @IdGrupo INT,
    @IdMateria INT,
	@IdProfesor INT,
    @Calificacion DECIMAL(10,2)
AS
BEGIN
    UPDATE Calificacion
    SET IdAlumno = @IdAlumno,
        IdGrupo = @IdGrupo,
        IdMateria = @IdMateria,
		IdProfesor = @IdProfesor,
        Calificacion = @Calificacion
    WHERE IdCalificacion = @IdCalificacion;
END;

-- Procedimiento para obtener todas las calificaciones con nombres de alumno, grupo, materia y profesor, y promedio final
CREATE PROCEDURE CalificacionGetAll
AS
BEGIN
    SELECT 
        C.IdCalificacion,
        A.Nombre AS NombreAlumno,
        G.Nombre AS NombreGrupo,
        M.Nombre AS NombreMateria,
        P.Nombre AS NombreProfesor,
        C.Calificacion,
        AVG(C.Calificacion) OVER (PARTITION BY A.IdAlumno) AS PromedioFinal
    FROM Calificacion C
    INNER JOIN Alumno A ON C.IdAlumno = A.IdAlumno
    INNER JOIN Grupo G ON C.IdGrupo = G.IdGrupo
    INNER JOIN Materia M ON C.IdMateria = M.IdMateria
    INNER JOIN Profesor P ON C.IdProfesor = P.IdProfesor;
END;

-- Procedimiento para obtener una calificación por ID con nombres de alumno, grupo, materia y profesor, y promedio final
CREATE PROCEDURE CalificacionGetById
    @IdCalificacion INT
AS
BEGIN
    SELECT 
        C.IdCalificacion,
        A.Nombre AS NombreAlumno,
        G.Nombre AS NombreGrupo,
        M.Nombre AS NombreMateria,
        P.Nombre AS NombreProfesor,
        C.Calificacion,
        AVG(C.Calificacion) OVER (PARTITION BY A.IdAlumno) AS PromedioFinal
    FROM Calificacion C
    INNER JOIN Alumno A ON C.IdAlumno = A.IdAlumno
    INNER JOIN Grupo G ON C.IdGrupo = G.IdGrupo
    INNER JOIN Materia M ON C.IdMateria = M.IdMateria
    INNER JOIN Profesor P ON C.IdProfesor = P.IdProfesor
    WHERE C.IdCalificacion = @IdCalificacion;
END;
-- Procedimiento para eliminar una calificación por ID
CREATE PROCEDURE CalificacionDelete
    @IdCalificacion INT
AS
BEGIN
    DELETE FROM Calificacion
    WHERE IdCalificacion = @IdCalificacion;
END;
