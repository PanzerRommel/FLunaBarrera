-- Procedimiento para agregar un nuevo grupo
CREATE PROCEDURE GrupoAdd
    @Nombre VARCHAR(100),
    @IdMateria INT,
    @IdProfesor INT
AS
BEGIN
    INSERT INTO Grupo (Nombre, IdMateria, IdProfesor)
    VALUES (@Nombre, @IdMateria, @IdProfesor);
END;

-- Procedimiento para actualizar un grupo existente
CREATE PROCEDURE GrupoUpdate
    @IdGrupo INT,
    @Nombre VARCHAR(100),
    @IdMateria INT,
    @IdProfesor INT
AS
BEGIN
    UPDATE Grupo
    SET Nombre = @Nombre,
        IdMateria = @IdMateria,
        IdProfesor = @IdProfesor
    WHERE IdGrupo = @IdGrupo;
END;

-- Procedimiento para eliminar un grupo por ID
CREATE PROCEDURE GrupoDelete
    @IdGrupo INT
AS
BEGIN
    DELETE FROM Grupo
    WHERE IdGrupo = @IdGrupo;
END;

-- Procedimiento para obtener todos los grupos con nombres de materia y profesor
CREATE PROCEDURE GrupoGetAll
AS
BEGIN
    SELECT 
        G.IdGrupo,
        G.Nombre,
        M.Nombre AS NombreMateria,
        P.Nombre AS NombreProfesor
    FROM Grupo G
    INNER JOIN Materia M ON G.IdMateria = M.IdMateria
    INNER JOIN Profesor P ON G.IdProfesor = P.IdProfesor;
END;

-- Procedimiento para obtener un grupo por ID con nombres de materia y profesor
CREATE PROCEDURE GrupoGetById
    @IdGrupo INT
AS
BEGIN
    SELECT 
        G.IdGrupo,
        G.Nombre,
        M.Nombre AS NombreMateria,
        P.Nombre AS NombreProfesor
    FROM Grupo G
    INNER JOIN Materia M ON G.IdMateria = M.IdMateria
    INNER JOIN Profesor P ON G.IdProfesor = P.IdProfesor
    WHERE G.IdGrupo = @IdGrupo;
END;
