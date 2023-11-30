--Procedimientos Almacenados para la tabla Usuario--

-- Procedimiento almacenado para agregar un nuevo usuario
CREATE PROCEDURE UsuarioAdd
    @Nombre VARCHAR(100),
    @UserName VARCHAR(100),
    @Email VARCHAR(200),
    @Password VARCHAR(256)
AS
BEGIN
    INSERT INTO Usuario (Nombre, UserName, Email, Password)
    VALUES (@Nombre, @UserName, @Email, @Password);
END;

-- Procedimiento almacenado para actualizar un usuario existente por su ID
CREATE PROCEDURE UsuarioUpdate
    @IdUsuario INT,
    @Nombre VARCHAR(100),
    @UserName VARCHAR(100),
    @Email VARCHAR(200),
    @Password VARCHAR(256)
AS
BEGIN
    UPDATE Usuario
    SET
        Nombre = @Nombre,
        UserName = @UserName,
        Email = @Email,
        Password = @Password
    WHERE
        IdUsuario = @IdUsuario;
END;

-- Procedimiento almacenado para eliminar un usuario por su ID
CREATE PROCEDURE UsuarioDelete
    @IdUsuario INT
AS
BEGIN
    DELETE FROM Usuario
    WHERE
        IdUsuario = @IdUsuario;
END;

-- Procedimiento almacenado para obtener todos los usuarios
CREATE PROCEDURE UsuarioGetAll
AS
BEGIN
    SELECT  
	
	IdUsuario,
	Nombre,
	UserName,
	Email,
	Password
	
	FROM Usuario;
END;

-- Procedimiento almacenado para obtener un usuario por su ID
CREATE PROCEDURE UsuarioGetById
    @IdUsuario INT
AS
BEGIN
    SELECT 
	
	IdUsuario,
	Nombre,
	UserName,
	Email,
	Password
	
	 FROM Usuario
    WHERE
        IdUsuario = @IdUsuario;
END;


-- Procedimiento almacenado para validar un usuario durante el inicio de sesión
CREATE PROCEDURE ValidarUsuario
    @UserName VARCHAR(100),
    @Password VARCHAR(256)
AS
BEGIN
    DECLARE @Mensaje NVARCHAR(255);

    -- Verificar si el usuario existe
    IF NOT EXISTS (SELECT 1 FROM Usuario WHERE UserName = @UserName)
    BEGIN
        SET @Mensaje = 'El usuario no existe.';
    END
    ELSE
    BEGIN
        -- Verificar si la contraseña es correcta
        IF NOT EXISTS (SELECT 1 FROM Usuario WHERE UserName = @UserName AND Password = @Password)
        BEGIN
            SET @Mensaje = 'La contraseña es incorrecta.';
        END
        ELSE
        BEGIN
            SET @Mensaje = 'Inicio de sesión exitoso.';
        END
    END

    -- Devolver el mensaje
    SELECT @Mensaje AS Mensaje;
END;
