create database MinaElMochito
go

use MinaElMochito
go

create schema Minas
go


create table Minas.Genero
(
	idGenero int identity primary key not null,
	descripcion varchar(1)
)
go

create table Minas.EstadoVehiculo
(
	idEstado int identity primary key not null,
	descripcion varchar(15)
)
go

create table Minas.cargo
(
	idCargo int identity primary key not null,
	descripcion varchar(15)
)
go

create table Minas.Mineral
(
	idMineral int identity primary key not null,
	descripcion varchar(15),
	precio numeric(18,2)	
)
go

create table Minas.Vehiculo
(
	idVehiculo int identity primary key not null,
	marca varchar(15),
	modelo varchar(15),
	placa varchar(15) not null,
	color varchar(15),
	estado int Foreign key references Minas.EstadoVehiculo(idEstado)
)
go

create table Minas.Empleado
(
	IdEmpleado int identity primary key not null,
	identidad varchar(13) not null,
	primerNombre varchar(20) not null,
	segundoNombre varchar(20),
	primerApellido varchar(20) not null,
	segundoApellido varchar(20),
	edad int,
	idGenero int Foreign key references Minas.Genero(idGenero),
	direccion  varchar(50),
	idCargo int Foreign key references Minas.cargo(idCargo),
	estado  varchar(15)
)
go

create table Minas.viajeInterno
(
	idViaje int identity primary key not null,
	idVehiculo int Foreign key references Minas.Vehiculo (idVehiculo),
	precio numeric(18,2),
	idEmpleado int Foreign key references Minas.Empleado(IdEmpleado)
)
go

create table Minas.Produccion
(
	idProduccion int identity primary key not null,
	idViaje int Foreign key references Minas.viajeInterno(idViaje),
	idMineral int Foreign key references Minas.Mineral(idMineral),
	precio numeric(18,2)	
)
go

create table Minas.Invetario
(
	idInventario int identity primary key not null,
	descripcion varchar(15),
	cantidad int
)
go

create table Minas.InventarioMineral
(
	idMineral int Foreign key references Minas.Mineral(idMineral)not null,
	peso decimal(18,2),
	fechaActualizacion datetime,
	Total decimal(18,2)
)
go

create table Minas.Salida
(
	idSalida int identity primary key not null,
	idProducto int Foreign key references Minas.Mineral(idMineral),
	cantidad decimal(18,2),
	Total decimal(18,2),
	detalleVenta varchar(50),
	fechaSalida datetime
)
go

create table Minas.Entrada
(
	idEntrada int identity primary key not null,
	idProducto int Foreign key references Minas.Mineral(idMineral),
	cantidad decimal(18,2),
	Total decimal(18,2),
	detalleEntrada varchar(50),
	fechaEntrada datetime
)
go







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

--select * from Minas.cargo




---------------------------------------------------------------Procedimientos Almanenados----------------------------------------
--CRUD Empleado
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

--exec CrearEmpleado '0314200000189','Abdiel','Jesus','Giron','Garcia',21,1,'San Jose De Comayagua',1
