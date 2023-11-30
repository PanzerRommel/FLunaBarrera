-- Procedimiento para agregar una nueva materia
CREATE PROCEDURE MateriaAdd
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(200)
AS
BEGIN
    INSERT INTO Materia (Nombre, Descripcion)
    VALUES (@Nombre, @Descripcion);
END;

-- Procedimiento para actualizar una materia existente
CREATE PROCEDURE MateriaUpdate
    @IdMateria INT,
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(200)
AS
BEGIN
    UPDATE Materia
    SET Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE IdMateria = @IdMateria;
END;

-- Procedimiento para eliminar una materia por ID
CREATE PROCEDURE MateriaDelete
    @IdMateria INT
AS
BEGIN
    DELETE FROM Materia
    WHERE IdMateria = @IdMateria;
END;

-- Procedimiento para obtener todas las materias
CREATE PROCEDURE MateriaGetAll
AS
BEGIN
    SELECT IdMateria, Nombre, Descripcion FROM Materia;
END;

-- Procedimiento para obtener una materia por ID
CREATE PROCEDURE MateriaGetById
    @IdMateria INT
AS
BEGIN
    SELECT IdMateria, Nombre, Descripcion FROM Materia
    WHERE IdMateria = @IdMateria;
END;