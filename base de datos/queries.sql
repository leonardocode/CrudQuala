create database DbQuala

use DbQuala


create table Moneda(
id int primary key identity not null,
moneda varchar(10) not null)

create table Sucursales(
codigo int primary key identity not null,
descripcion varchar(250) not null,
direccion varchar(250) not null,
identificacion varchar(50) not null,
fechaCreacion datetime default getdate(),
idMoneda int references Moneda(id));

insert into Moneda(moneda) values('COP');
insert into Moneda(moneda) values('USD');
insert into Moneda(moneda) values('EUR');

select * from Moneda;
select * from Sucursales


