/*Base de datos Mina el MOchito*/

use tempdb
go

create database MinaElMochito
go

use MinaElMochito
go

create schema Minas
go

create schema Usuarios
go

create table Minas.Genero
(
	idGenero int identity not null,
	descripcion varchar(1),
	Constraint PK_Genero_id
	       primary key clustered(idGenero)
)
go

create table Minas.EstadoVehiculo
(
	idEstado int identity not null,
	descripcion varchar(15),
	Constraint PK_EstadoVehiculo_id
	       primary key clustered(idEstado)
)
go

create table Minas.cargo
(
	idCargo int identity not null,
	descripcion varchar(15),
	Constraint PK_Cargo_id
	       primary key clustered(idCargo)
)
go

create table Minas.Mineral
(
	idMineral int identity  not null,
	descripcion varchar(15),
	precio decimal(18,2) not null,
	Constraint PK_Mineral_id
	       primary key clustered(idMineral)
)
go

create table Minas.Vehiculo
(
	idVehiculo int identity  not null,
	marca varchar(15),
	modelo varchar(15),
	placa varchar(15) not null,
	color varchar(15),
	estado int ,
      Constraint PK_Vehiculo_id
	       primary key clustered(idVehiculo),    
   
    constraint fk_vehiculo_estado
	foreign key (estado)
	references Minas.EstadoVehiculo(idEstado),
) 
go

create table Minas.Empleado
(
	IdEmpleado int identity not null,
	identidad varchar(13) not null,
	primerNombre varchar(100) not null,
	edad int,
	idGenero int ,
	direccion  varchar(50),
	idCargo int,
	salario decimal,
	estado  varchar(15)

	Constraint PK_Empleado_id
	primary key clustered(IdEmpleado),
	  
	constraint fk_Empleado_idGenero
	foreign key (idGenero)
	references Minas.Genero(idGenero),

    constraint fk_vehiculo_idcargo
	foreign key (idCargo)
	references Minas.Cargo(idCargo)
)
go

create table Minas.viajeInterno
(
	idViaje int identity not null,
	idVehiculo int not null,
	idEmpleado int not null,

	Constraint PK_viaje_idViaje
	primary key clustered(idViaje),

    constraint fk_ViajeInterno_idVehiculo
	foreign key (idVehiculo)
	references Minas.Vehiculo(idVehiculo),

	constraint fk_ViajeInterno_idEmpleado
	foreign key (idEmpleado)
	references Minas.Empleado(idEmpleado)
	  
)
go

create table Minas.Produccion
(
	idProduccion int identity not null,
	idViaje int not null unique,
	idMineral int not null,
	precio numeric(18,2),
	peso numeric(18, 2),
	total decimal(18, 2),
	
		
	Constraint PK_Produccion_idProduccion
	primary key clustered(idProduccion),

    constraint fk_Produccion_idViaje
	foreign key (idViaje)
	references Minas.viajeInterno(idViaje),

	constraint fk_Produccion_idMineral
	foreign key (idMineral)
	references Minas.Mineral(idMineral)
)
go


create table Minas.InventarioMineral
(
	idMineral int not null,
	peso decimal(18,2),
	fechaActualizacion datetime,
	Total decimal(18,2)

	constraint fk_InventarioMineral_idMineral
	foreign key (idMineral)
	references Minas.Mineral(idMineral)
)
go

create table Minas.Salida
(
	idSalida int identity  not null,
	idmineral int ,
	cantidad decimal(18,2),
	Total decimal(18,2),
	detalleVenta varchar(50),
	fechaSalida datetime,

	Constraint PK_Salida_idSalida
	primary key clustered(idSalida),

    constraint fk_Salidas_idProducto
	foreign key (idmineral)
	references Minas.Mineral(idMineral),
)
go

create table Minas.Entrada
(
	idEntrada int identity  not null,
	idProducto int ,
	cantidad decimal(18,2),
	Total decimal(18,2),
	detalleEntrada varchar(50),
	fechaEntrada datetime,

	Constraint PK_Entrada_idEntrada
	primary key clustered(idEntrada),

    constraint fk_Entrada_Producto
	foreign key (idProducto)
	references Minas.Mineral(idMineral),
)
go

Create table Usuarios.Usuario
(
    id INT NOT NULL IDENTITY (200, 1),
	nombreCompleto VARCHAR(255) NOT NULL,
	username VARCHAR(100) NOT NULL,
	password VARCHAR(100) NOT NULL,
	rol VARCHAR (25),
	estado BIT NOT NULL,

	CONSTRAINT PK_Usuario_id
    PRIMARY KEY CLUSTERED (id)
)


------------------------------Restricciones
---para que el rol del usuario solo sea administrador o empleado de turno
ALTER TABLE Usuarios.Usuario WITH CHECK
	ADD CONSTRAINT CHK_Usuarios_USuarios$RolUsuario
	CHECK (rol IN('ADMINISTRADOR', 'EMPLEADODETURNO'))
GO

-- No puede existir nombres de usuarios repetidos
ALTER TABLE Usuarios.Usuario
	ADD CONSTRAINT AK_Usuarios_Usuario_username
	UNIQUE NONCLUSTERED (username)

GO

-- La contraseña debe contener al menos 6 caracteres
ALTER TABLE Usuarios.Usuario WITH CHECK
	ADD CONSTRAINT CHK_Usuarios_Usuario$VerificarLongitudContraseña
	CHECK (LEN(password) >= 6)
	
GO

----Triggers -------------------------

---trigger para llenar Inventario mineral despues de una insercion en produccion
create trigger Minas.ActualizarInventarioMinera
on [Minas].[Produccion]
AFTER INSERT
AS BEGIN 
declare 
@idMineral as int , @peso as decimal,@precio as decimal,@total as decimal

select @peso= [peso] from inserted
select @idMineral= [idMineral]from inserted
select @precio=[precio] from inserted
Set @total=@peso * @precio

update [Minas].[InventarioMineral]
set[peso]=(peso + @peso),[fechaActualizacion] =SYSDATETIMEOFFSET(), total =total + @total where [idMineral] =@idMineral

end 
GO

ALTER TABLE [Minas].[Produccion] ENABLE TRIGGER [ActualizarInventarioMinera]
GO

---Triggers para un manejo de entradas de minerales (concepto de entrada en kardex)----
create trigger Minas.AumentarInventario
on [Minas].[Produccion]
AFTER INSERT
AS BEGIN 
declare 
@idMineral as int , @peso as decimal,@precio as decimal,@total as decimal

select @peso= [peso] from inserted
select @idMineral= [idMineral]from inserted
select @precio=[precio] from inserted
Set @total=@peso * @precio

INSERT INTO  [Minas].[Entrada]
VALUES(@idMineral,@peso,@total,'',SYSDATETIMEOFFSET())
end 
GO
ALTER TABLE [Minas].[Produccion] ENABLE TRIGGER [AumentarInventario]
GO


-----trigger para restar en inventario mineral despues de una salida
CREATE TRIGGER Minas.reduccionInventarioMineral
ON [Minas].[Salida]
AFTER INSERT
AS
BEGIN
	DECLARE @cantidad decimal,@total decimal , @idMineral int
	SELECT @cantidad = cantidad,@total=Total  ,@idMineral = idmineral from inserted 
	update [Minas].[InventarioMineral]
	set
	[peso] =  [peso] - @cantidad,
[fechaActualizacion]= SYSUTCDATETIME(),
[Total]=[Total]-Total
	where [idMineral] = @idMineral
END
GO

-- Trigger par calcular el total de una producción despues de insertar el peso
Create Trigger Minas.TotalProduccion
On Minas.Produccion
After Insert
As
Begin
	Declare @peso decimal, @precio decimal, @total decimal, @idProduccion int
    Select @peso = peso from inserted
	Select @precio = precio from inserted
	Select @idProduccion = idProduccion from inserted
	   Set @total = @peso * @precio
    Update Minas.Produccion set @total = total where idProduccion = @idProduccion
END
Go

----------------------------------------------------------------Inserciones------------------------------------------------------


insert into Minas.Genero values
('M'),
('F')
go

--select * from Minas.Genero

insert into Minas.cargo values
('Gerente'),
('escabador')
go



insert into [Minas].[Mineral] ([descripcion],[precio]) values
('ORO',1174.44),
('PLATA', 10678.68)
go

insert into [Minas].[EstadoVehiculo] ([descripcion]) values
('Disponible')
go 

insert into [Minas].[Vehiculo] ([marca],[modelo],[placa],[color],[estado]) values
('MACK', 'MACK 4X4','12wer12s','rojo',1),
('CAT', '795F CA','1567ygays','amarilloo',1)
go

insert into [Minas].[Empleado] ([identidad],[primerNombre],[edad],[idGenero],direccion,[idCargo],[salario],[estado]) values 
('0318200300249','lionel messi','33',1,'barrio no te encuentro',1,10000,1),
('1627865479101','Victor Madrid','30',1,'Morazan',1,10678,1)
go

insert into [Minas].[viajeInterno]([idVehiculo],[idEmpleado])values 
(1,1),
(2, 2)
go

delete from Minas.Produccion where idProduccion = 3
insert into [Minas].[Produccion]([idViaje],[idMineral],[precio],[peso])Values
(1,1,10678.68,11.5)
go

insert into [Minas].[InventarioMineral]([idMineral],[peso],[fechaActualizacion],[Total])values
(1,0,GETDATE(),0)
go


---------------------------------------------------------------Procedimientos Almanenados----------------------------------------
/* Procedimiento para Usuarios (Agregar Nuevo usuario)*/


CREATE PROCEDURE agregarUsuarios (

@nombreCompleto varchar(225),
@username varchar(100),
@paswrod varchar(100),
@rol char (25),
@estado bit 
)

AS
BEGIN
if exists (select username from Usuarios.Usuario where username= @username and estado=1)
raiserror ('Ya existe un usuario con ese usuario, porfavor ingrese uno nuevo',16,1)
else
insert into Usuarios.Usuario( nombreCompleto,username,password,rol,estado) 
values(@nombreCompleto,@username,@paswrod,@rol,@estado)
END
go

/*procedimiento para actualizar un usuario*/
drop procedure actualizarUsuario
go
create procedure actualizarUsuario (
@id as int ,
@nombreCompleto varchar(225),
@username varchar(100),
@paswrod varchar(100),
@rol char (25),
@estado bit 
)

AS
BEGIN

update Usuarios.Usuario
set nombreCompleto  = @nombreCompleto, username = @username,
password  = @paswrod, rol=@rol,estado=@estado
where id = @id
end 
go

----crear procedimiento eliminar usuario

create procedure eliminarUsuario(@id int )
AS
BEGIN

update Usuarios.Usuario set estado =0
where  id =@id
end 
go

/*procedimiento ´para buscar usuario*/
create procedure buscarUser
@nombre as varchar(225)
as begin
select *
from Usuarios.Usuario
where nombreCompleto like '%' +@nombre+ '%'
end 
go



----------------------------------------CRUD Empleado
--Crear
create procedure CrearEmpleado
@identidad varchar(13),
@primerNombre varchar(20),
@segundoNombre varchar(20),
@primerApellido varchar(20),
@segundoApellido varchar(20),
@edad int,
@idGenero int,
@direccion  varchar(50),
@idPuesto int
as
begin
insert	into Minas.Empleado values(@identidad,@primerNombre,@segundoNombre,@primerApellido,@segundoApellido,@edad,@idGenero,@direccion,@idPuesto,'activo')
end
go

create procedure verEmpleados
as begin
select E.IdEmpleado AS 'Empleado ID',
	   E.identidad,
	   E.primerNombre as 'Primer Nombre',
	   E.segundoNombre as 'Segundo Nombre',
	   E.primerApellido as 'Primer Apellido',
	   E.segundoApellido as 'Segundo Apellido',
	   E.edad,
	   G.descripcion as 'Genero',
	   E.direccion,
	   C.descripcion as 'Cargo'
from Minas.Empleado E INNER JOIN  Minas.Genero G
on G.idGenero = E.idGenero INNER JOIN Minas.cargo C
on C.idCargo = E.idCargo
where E.estado = 'activo'
end
go

--ver empleado Individual
create procedure verEmpleado(@ID int)
as 
begin
select E.IdEmpleado AS 'Empleado ID',
	   E.identidad,
	   E.primerNombre as 'Primer Nombre',
	   E.segundoNombre as 'Segundo Nombre',
	   E.primerApellido as 'Primer Apellido',
	   E.segundoApellido as 'Segundo Apellido',
	   E.edad,
	   G.descripcion as 'Genero',
	   E.direccion,
	   C.descripcion as 'Cargo'
from Minas.Empleado E INNER JOIN  Minas.Genero G
on G.idGenero = E.idGenero INNER JOIN Minas.cargo C
on C.idCargo = E.idCargo
where E.IdEmpleado = @ID and  E.estado = 'activo'
end
go

--Actualizar un empleado
create procedure ActualizarEmpleado(	
@primerNombre varchar(20),
@segundoNombre varchar(20),
@primerApellido varchar(20) ,
@segundoApellido varchar(20),
@edad int,
@idGenero int ,
@direccion  varchar(50),
@idCargo int
)
as 
begin
update Minas.Empleado set 
primerNombre = @primerNombre,
segundoNombre = @segundoNombre,
primerApellido = @primerApellido,
segundoApellido = @segundoApellido,
edad = @edad,
idGenero =@idGenero,
direccion =@direccion,
idCargo = @idCargo
end
go

--Eliminar un empleado
create procedure EliminarEmpleado(@ID int)
as
begin
	update Minas.Empleado set estado = 'inactivo'
end
go
