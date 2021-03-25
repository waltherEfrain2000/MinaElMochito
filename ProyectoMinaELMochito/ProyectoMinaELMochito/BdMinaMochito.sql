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
	precio numeric(18,2),
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
	primerNombre varchar(20) not null,
	segundoNombre varchar(20),
	primerApellido varchar(20) not null,
	segundoApellido varchar(20),
	edad int,
	idGenero int ,
	direccion  varchar(50),
	idCargo int,
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
	precio numeric(18,2),
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

create table Minas.Invetario
(
	idInventario int identity not null,
	descripcion varchar(15),
	cantidad int,

	 Constraint PK_Inventario_idInventario
	       primary key clustered(idInventario),

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
	rol char (25),
	estado BIT NOT NULL,

	CONSTRAINT PK_Usuario_id
		PRIMARY KEY CLUSTERED (id)


)


------------------------------restricciones

ALTER TABLE Usuarios.Usuario WITH CHECK
	ADD CONSTRAINT CHK_Usuarios_USuarios$RolUsuario
	CHECK (rol IN('ADMINISTRADOR', 'EMPLEADODETURNO'))
GO


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
