USE MASTER
GO

CREATE DATABASE scpo
go

use scpo
go

create schema scpo
go


/*crear las tablas */
create table scpo.Paciente(
	idPaciente INT NOT NULL IDENTITY(100,1) CONSTRAINT PK_Paciente_ID PRIMARY KEY CLUSTERED(idPaciente),
	nombrePaciente text not null,
	identidadPaciente char(15) not null,
	fechaNacimiento date not null,
	edad int not null,
	numeroTelefono char(9) not null,
	genero text not null,
	estadoCivil text not null,
	ocupacion text not null,
	direccion text not null
);

create table scpo.Usuario(
	idUsuario int not null identity(100,1) constraint PK_Usuario_ID primary key clustered(idUsuario),
	nombreUsuario varchar(255) not null,
	password varchar(255) not null
);

create table scpo.HistoriaClinica(
	idHistoriaClinica int not null identity(100,1) constraint PK_HistoriaClinica_ID primary key clustered(idHistoriaClinica),
	idPaciente int not null,
	motivoConsulta text not null,
	fechaIngreso datetime not null,
	HEA text not null,
	diagnostico text not null,
	tratamiento text not  null
);

create table scpo.Odontograma(
	idOdontograma int not null identity(100,1) constraint PK_Odontograma_ID primary key clustered(idOdontograma),
	idPaciente int not null,
	idProcedimiento int not null,
	numeroDiente int not null
);

create table scpo.ProcedimientoDental(
	idProcedimientoDental int not null identity(100,1) constraint PK_ProcedimientoDental_ID primary key clustered(idProcedimientoDental),
	procedimiento text not null
);

create table scpo.Cita(
	idCita int not null identity(100,1) constraint PK_Cita_ID primary key clustered(idCita),
	idPaciente int not null,
	fechaCita datetime not null,
	descripcion text not null
);

/* Relaciones de las tablas */

alter table scpo.HistoriaClinica
ADD CONSTRAINT 
FK_IdPaciente$idPaciente_Paciente
FOREIGN KEY(idPaciente) REFERENCES scpo.Paciente(idPaciente)
on update no action
on delete no action;

alter table scpo.Odontograma
ADD CONSTRAINT 
FK_IdPacienteOdontograma$idPaciente_Paciente
FOREIGN KEY(idPaciente) REFERENCES scpo.Paciente(idPaciente)
on update no action
on delete no action;

alter table scpo.Odontograma
ADD CONSTRAINT 
FK_IdProcedomientoOdontograma$idProcedimientoDental_ProcedimientoDental
FOREIGN KEY(idProcedimiento) REFERENCES scpo.ProcedimientoDental(idProcedimientoDental)
on update no action
on delete no action;

alter table scpo.Cita
ADD CONSTRAINT 
FK_IdPacienteCita$idPaciente_Paciente
FOREIGN KEY(idPaciente) REFERENCES scpo.Paciente(idPaciente)
on update no action
on delete no action;


/* CONSTRAINT DEL NUMERO DE IDENTIDAD Y NUMERO DE TELEFONO*/
ALTER TABLE scpo.Paciente WITH CHECK
ADD CONSTRAINT CHK_Paciente$FormatoParaNumeroIdentidad
CHECK (identidadPaciente LIKE '[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9][0-9]')
GO

ALTER TABLE scpo.Paciente WITH CHECK
ADD CONSTRAINT CHK_Paciente$FormatoParaElTelefono
CHECK (numeroTelefono LIKE '[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]')
GO

/* STORE PROCEDURES DE PACIENTES*/

create procedure sp_Add_Paciente(
	@nombrePaciente text,
	@identidadPaciente char(15),
	@fechaNacimiento date,
	@edad int,
	@numeroTelefono char(9),
	@genero text,
	@estadoCivil text,
	@ocupacion text,
	@direccion text
)
as 
begin	
	insert into scpo.Paciente(nombrePaciente, identidadPaciente, fechaNacimiento, edad, numeroTelefono, genero, estadoCivil, ocupacion, direccion)
						 values(@nombrePaciente,@identidadPaciente,@fechaNacimiento,@edad,@numeroTelefono,@genero, @estadoCivil,@ocupacion, @direccion)
end 
go



--- actualizar
create procedure sp_Update_Paciente(
	@idPaciente int,
	@nombrePaciente text,
	@identidadPaciente char(15),
	@fechaNacimiento date,
	@edad int,
	@numeroTelefono char(9),
	@genero text,
	@estadoCivil text,
	@ocupacion text,
	@direccion text
)
as
begin 
	update scpo.Paciente set nombrePaciente=@nombrePaciente,
										identidadPaciente=@identidadPaciente,
										fechaNacimiento=@fechaNacimiento,
										edad=@edad,
										numeroTelefono=@numeroTelefono,
										genero=@genero,
										estadoCivil=@estadoCivil,
										ocupacion=@ocupacion,
										direccion= @direccion
	where idPaciente = @idPaciente
end 
go
	

	
	--drop procedure sp_Update_Paciente
	--select * from scpo.Paciente

	--delete scpo.Paciente where idPaciente = 101