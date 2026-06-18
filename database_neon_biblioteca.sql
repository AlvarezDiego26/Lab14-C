-- Sistema de Biblioteca - PostgreSQL / Neon DB
-- Ejecutar en Neon SQL Editor.

create extension if not exists pgcrypto;

create table roles (
    id uuid primary key default gen_random_uuid(),
    nombre varchar(50) not null unique,
    descripcion varchar(200),
    created_at timestamptz not null default now(),
    updated_at timestamptz not null default now()
);

create table usuarios (
    id uuid primary key default gen_random_uuid(),
    rol_id uuid not null references roles(id) on delete restrict,
    nombres varchar(100) not null,
    apellidos varchar(100) not null,
    dni varchar(20) not null unique,
    email varchar(150) not null unique,
    telefono varchar(30),
    direccion varchar(200),
    estado varchar(20) not null default 'ACTIVO',
    created_at timestamptz not null default now(),
    updated_at timestamptz not null default now(),
    constraint ck_usuarios_estado check (estado in ('ACTIVO', 'INACTIVO', 'SUSPENDIDO'))
);

create table autores (
    id uuid primary key default gen_random_uuid(),
    nombres varchar(120) not null,
    apellidos varchar(120) not null,
    nacionalidad varchar(80),
    fecha_nacimiento date,
    created_at timestamptz not null default now(),
    updated_at timestamptz not null default now()
);

create table editoriales (
    id uuid primary key default gen_random_uuid(),
    nombre varchar(150) not null unique,
    pais varchar(80),
    sitio_web varchar(200),
    created_at timestamptz not null default now(),
    updated_at timestamptz not null default now()
);

create table categorias (
    id uuid primary key default gen_random_uuid(),
    nombre varchar(100) not null unique,
    descripcion varchar(250),
    created_at timestamptz not null default now(),
    updated_at timestamptz not null default now()
);

create table libros (
    id uuid primary key default gen_random_uuid(),
    editorial_id uuid references editoriales(id) on delete set null,
    titulo varchar(200) not null,
    isbn varchar(20) not null unique,
    anio_publicacion int,
    numero_paginas int,
    descripcion text,
    estado varchar(20) not null default 'ACTIVO',
    created_at timestamptz not null default now(),
    updated_at timestamptz not null default now(),
    constraint ck_libros_estado check (estado in ('ACTIVO', 'INACTIVO')),
    constraint ck_libros_anio check (anio_publicacion is null or anio_publicacion between 1400 and 2100),
    constraint ck_libros_paginas check (numero_paginas is null or numero_paginas > 0)
);

create table libro_autores (
    libro_id uuid not null references libros(id) on delete cascade,
    autor_id uuid not null references autores(id) on delete restrict,
    primary key (libro_id, autor_id)
);

create table libro_categorias (
    libro_id uuid not null references libros(id) on delete cascade,
    categoria_id uuid not null references categorias(id) on delete restrict,
    primary key (libro_id, categoria_id)
);

create table ejemplares (
    id uuid primary key default gen_random_uuid(),
    libro_id uuid not null references libros(id) on delete cascade,
    codigo varchar(30) not null unique,
    ubicacion varchar(100),
    estado varchar(20) not null default 'DISPONIBLE',
    created_at timestamptz not null default now(),
    updated_at timestamptz not null default now(),
    constraint ck_ejemplares_estado check (estado in ('DISPONIBLE', 'PRESTADO', 'MANTENIMIENTO', 'BAJA'))
);

create table prestamos (
    id uuid primary key default gen_random_uuid(),
    usuario_id uuid not null references usuarios(id) on delete restrict,
    ejemplar_id uuid not null references ejemplares(id) on delete restrict,
    fecha_prestamo date not null default current_date,
    fecha_vencimiento date not null,
    fecha_devolucion date,
    estado varchar(20) not null default 'ACTIVO',
    observacion varchar(250),
    created_at timestamptz not null default now(),
    updated_at timestamptz not null default now(),
    constraint ck_prestamos_estado check (estado in ('ACTIVO', 'DEVUELTO', 'VENCIDO', 'ANULADO')),
    constraint ck_prestamos_fechas check (fecha_vencimiento >= fecha_prestamo),
    constraint ck_prestamos_devolucion check (fecha_devolucion is null or fecha_devolucion >= fecha_prestamo)
);

create table multas (
    id uuid primary key default gen_random_uuid(),
    prestamo_id uuid not null unique references prestamos(id) on delete cascade,
    monto numeric(10, 2) not null,
    motivo varchar(200) not null,
    estado varchar(20) not null default 'PENDIENTE',
    fecha_pago date,
    created_at timestamptz not null default now(),
    updated_at timestamptz not null default now(),
    constraint ck_multas_monto check (monto >= 0),
    constraint ck_multas_estado check (estado in ('PENDIENTE', 'PAGADA', 'ANULADA'))
);

create index ix_usuarios_rol_id on usuarios(rol_id);
create index ix_autores_apellidos on autores(apellidos);
create index ix_libros_editorial_id on libros(editorial_id);
create index ix_libros_titulo on libros(titulo);
create index ix_ejemplares_libro_id on ejemplares(libro_id);
create index ix_prestamos_usuario_id on prestamos(usuario_id);
create index ix_prestamos_ejemplar_id on prestamos(ejemplar_id);
create index ix_prestamos_estado on prestamos(estado);

create or replace function set_updated_at()
returns trigger
language plpgsql
as $$
begin
    new.updated_at = now();
    return new;
end;
$$;

create trigger trg_roles_updated_at
before update on roles
for each row execute function set_updated_at();

create trigger trg_usuarios_updated_at
before update on usuarios
for each row execute function set_updated_at();

create trigger trg_autores_updated_at
before update on autores
for each row execute function set_updated_at();

create trigger trg_editoriales_updated_at
before update on editoriales
for each row execute function set_updated_at();

create trigger trg_categorias_updated_at
before update on categorias
for each row execute function set_updated_at();

create trigger trg_libros_updated_at
before update on libros
for each row execute function set_updated_at();

create trigger trg_ejemplares_updated_at
before update on ejemplares
for each row execute function set_updated_at();

create trigger trg_prestamos_updated_at
before update on prestamos
for each row execute function set_updated_at();

create trigger trg_multas_updated_at
before update on multas
for each row execute function set_updated_at();

insert into roles (nombre, descripcion) values
('ADMINISTRADOR', 'Usuario con acceso administrativo'),
('BIBLIOTECARIO', 'Usuario encargado de gestionar prestamos'),
('LECTOR', 'Usuario que solicita prestamos');

insert into editoriales (nombre, pais, sitio_web) values
('Planeta', 'Espana', 'https://www.planetadelibros.com'),
('Penguin Random House', 'Estados Unidos', 'https://www.penguinrandomhouse.com'),
('Fondo Editorial PUCP', 'Peru', 'https://www.fondoeditorial.pucp.edu.pe');

insert into categorias (nombre, descripcion) values
('Programacion', 'Libros sobre desarrollo de software'),
('Base de Datos', 'Libros sobre diseno y administracion de datos'),
('Literatura', 'Obras literarias y novelas'),
('Gestion', 'Libros de gestion y administracion');

insert into autores (nombres, apellidos, nacionalidad, fecha_nacimiento) values
('Robert', 'Martin', 'Estados Unidos', '1952-12-05'),
('Martin', 'Fowler', 'Reino Unido', '1963-12-18'),
('Mario', 'Vargas Llosa', 'Peru', '1936-03-28');

insert into libros (editorial_id, titulo, isbn, anio_publicacion, numero_paginas, descripcion)
select e.id, 'Clean Code', '9780132350884', 2008, 464, 'Buenas practicas para escribir codigo mantenible.'
from editoriales e
where e.nombre = 'Penguin Random House';

insert into libros (editorial_id, titulo, isbn, anio_publicacion, numero_paginas, descripcion)
select e.id, 'Refactoring', '9780201485677', 1999, 431, 'Tecnicas para mejorar el diseno de codigo existente.'
from editoriales e
where e.nombre = 'Penguin Random House';

insert into libros (editorial_id, titulo, isbn, anio_publicacion, numero_paginas, descripcion)
select e.id, 'La ciudad y los perros', '9788420471839', 1963, 448, 'Novela representativa de la literatura peruana.'
from editoriales e
where e.nombre = 'Planeta';

insert into libro_autores (libro_id, autor_id)
select l.id, a.id
from libros l
join autores a on a.apellidos = 'Martin'
where l.titulo = 'Clean Code' and a.nombres = 'Robert';

insert into libro_autores (libro_id, autor_id)
select l.id, a.id
from libros l
join autores a on a.apellidos = 'Fowler'
where l.titulo = 'Refactoring';

insert into libro_autores (libro_id, autor_id)
select l.id, a.id
from libros l
join autores a on a.apellidos = 'Vargas Llosa'
where l.titulo = 'La ciudad y los perros';

insert into libro_categorias (libro_id, categoria_id)
select l.id, c.id
from libros l
join categorias c on c.nombre = 'Programacion'
where l.titulo in ('Clean Code', 'Refactoring');

insert into libro_categorias (libro_id, categoria_id)
select l.id, c.id
from libros l
join categorias c on c.nombre = 'Literatura'
where l.titulo = 'La ciudad y los perros';

insert into ejemplares (libro_id, codigo, ubicacion)
select id, 'BIB-CC-001', 'Estante A1' from libros where titulo = 'Clean Code';

insert into ejemplares (libro_id, codigo, ubicacion)
select id, 'BIB-RF-001', 'Estante A2' from libros where titulo = 'Refactoring';

insert into ejemplares (libro_id, codigo, ubicacion)
select id, 'BIB-LP-001', 'Estante L1' from libros where titulo = 'La ciudad y los perros';

insert into usuarios (rol_id, nombres, apellidos, dni, email, telefono, direccion)
select id, 'Diego', 'Mayhua', '70000001', 'diego.mayhua@example.com', '999888777', 'Lima'
from roles
where nombre = 'LECTOR';

insert into usuarios (rol_id, nombres, apellidos, dni, email, telefono, direccion)
select id, 'Ana', 'Torres', '70000002', 'ana.torres@example.com', '988777666', 'Lima'
from roles
where nombre = 'BIBLIOTECARIO';
