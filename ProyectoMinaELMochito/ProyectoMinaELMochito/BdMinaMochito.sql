/*Base de datos Mina el Mochito*/

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
	descripcion varchar(30),
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
	modelo varchar(30),
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
	salario decimal(18,2),
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
	idViaje int not null,
	idMineral int not null,
	precio numeric(18,2),
	peso numeric(18, 2),
	total as (precio * peso) persisted
	
		
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
[Total]=[Total]-@Total
	where [idMineral] = @idMineral
END
GO

-- Trigger para incrementar el inventario en caso de una eliminacion en la salida
Create Trigger Minas.BorrarProductoSalida
On Minas.Salida
After delete
As
Begin
	Declare @peso decimal, @precio decimal, @total decimal, @idMineral int
	Select @idMineral = idmineral from deleted
	Select @total = total from deleted
	select @peso = cantidad from deleted
    Update Minas.InventarioMineral set peso = peso + @peso ,Total =Total + @total where idMineral = @idMineral
END
Go


-- Trigger que restablece el inventario si es que se elimina una producción
CREATE TRIGGER Minas.restablecerInventario
ON [Minas].Produccion
AFTER delete
AS
BEGIN
	DECLARE @cantidad decimal,@total decimal , @idMineral int
	SELECT @cantidad = peso from deleted
	select @total = total from deleted
	update [Minas].[InventarioMineral]
	set
	[peso] =  [peso] - @cantidad, 
	[fechaActualizacion]= SYSUTCDATETIME(),
	[Total]=[Total]-@Total
	where [idMineral] = @idMineral
END
GO


----------------------------------------------------------------Inserciones------------------------------------------------------


insert into Minas.Genero values
('M'),
('F')
go

--select * from Minas.Genero

insert into Minas.cargo values
('Gerente'),
('Secretarios'),
('Supervisor de Area'),
('Guardia'),
('Conductor'),
('Minero'),
('conductor Maquinaria')
go

insert into minas.EstadoVehiculo values 
('Activo'),
('Reparacion'),
('Inactivo')


insert into minas.Vehiculo values 
('Caterpillar','Retroexcabadora','AQE1265','Amarillo',1),
('Caterpillar','Retroexcabadora','QQXSQ23','Amarillo',2),
('XCMG','Retroexcabadora','ASDJU15','Amarillo',1),
('Caterpillar','Cargadora','QQXSQ23','Naranja',1),
('xcmg','Billdozers','OTIR159','Amarillo',2),
('xcmg','Billdozers','NVJGO84','Amarillo',1),
('xcmg','Motoniveladoras','PYNGI10','Amarillo',1)
go

insert into [Minas].[Mineral] ([descripcion],[precio]) values
('Oro',56182.88),
('Zinc',2809.50),
('Plomo',1984.50),
('Plata',836.88),
('Cobre',9187.50)
go

insert into [Minas].[InventarioMineral] ([idMineral],[peso], [fechaActualizacion], [Total]) values
(1,00,GETDATE(),00),
(2,00,GETDATE(),00),
(3,00,GETDATE(),00),
(4,00,GETDATE(),00),
(5,00,GETDATE(),00)
go


insert into [Minas].[Empleado] ([identidad],[primerNombre],[edad],[idGenero],direccion,[idCargo],[salario],[estado]) values 
('0318200300249','lionel messi','33',1,'Barrio Guamilito',1,10000,'Activo'),
('1627865479101','Victor Madrid','30',1,'Barrio Morazan',1,10678,'Activo')
go

insert into [Minas].[viajeInterno]([idVehiculo],[idEmpleado])values 
(1,1),
(2, 2)
go

insert into [Minas].[Produccion]([idViaje],[idMineral],[precio],[peso])Values
(1,1,56182.88,11.5)
go


--Insert into Minas.Salida ([idmineral],[cantidad],[Total],[detalleVenta],[fechaSalida]) values 
--(1, 1, 56182.88, 'Salio 1 kg de oro', GETDATE())
--go

insert into [Usuarios].[Usuario] values
('Messi Andres Cuccitini','Admin','MinaMochito2021','ADMINISTRADOR', 1),
('Ronaldo Santo Aveiro','Cr7','siuuuuu','EMPLEADODETURNO', 1)
go
