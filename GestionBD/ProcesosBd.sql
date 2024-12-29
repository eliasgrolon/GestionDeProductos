create database Gestion;
use Gestion;

create table productos(
	idProducto int identity (1,1),
	codigoBarra varchar(50),
	nombre varchar(50),
	marca varchar(50),
	categoria varchar(50),
	precio decimal(10,2)
);

select * from productos

create proc listaProductos
as 
begin
	select 
	idProducto,
	codigoBarra,
	nombre,
	marca,
	categoria,
	precio
	from productos
end

create proc guardarProducto(
	@codigoBarra varchar(50),
	@nombre varchar(50),
	@marca varchar(50),
	@categoria varchar(100),
	@precio decimal(10,2)
)as
begin
	insert into productos (codigoBarra, nombre, marca, categoria, precio)
	values(@codigoBarra, @nombre, @marca, @categoria, @precio)
end

create proc editarProducto(
	@idProducto int,
	@codigoBarra varchar(50)null,
	@nombre varchar(50)null,
	@marca varchar(50)null,
	@categoria varchar(100)null,
	@precio decimal(10,2)null
)as
begin
	update productos set
	codigoBarra = isnull (@codigoBarra, codigoBarra),
	nombre = isnull (@nombre, nombre),
	marca = isnull (@marca, marca),
	categoria = isnull (@categoria, categoria),
	precio = isnull (@precio, precio) where idProducto = @idProducto
end

drop proc editarProducto

create proc eliminarProducto(
	@idProducto int
)as
begin
	delete from productos where idProducto=@idProducto
end