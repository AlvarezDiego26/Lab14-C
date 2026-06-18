using System;
using System.Collections.Generic;
using Biblioteca.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Mvc.Data;

public partial class BibliotecaDbContext : DbContext
{
    public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Editoriale> Editoriales { get; set; }

    public virtual DbSet<Ejemplare> Ejemplares { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Multa> Multas { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("autores_pkey");

            entity.ToTable("autores");

            entity.HasIndex(e => e.Apellidos, "ix_autores_apellidos");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(120)
                .HasColumnName("apellidos");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(80)
                .HasColumnName("nacionalidad");
            entity.Property(e => e.Nombres)
                .HasMaxLength(120)
                .HasColumnName("nombres");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categorias_pkey");

            entity.ToTable("categorias");

            entity.HasIndex(e => e.Nombre, "categorias_nombre_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Editoriale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("editoriales_pkey");

            entity.ToTable("editoriales");

            entity.HasIndex(e => e.Nombre, "editoriales_nombre_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.Pais)
                .HasMaxLength(80)
                .HasColumnName("pais");
            entity.Property(e => e.SitioWeb)
                .HasMaxLength(200)
                .HasColumnName("sitio_web");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Ejemplare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ejemplares_pkey");

            entity.ToTable("ejemplares");

            entity.HasIndex(e => e.Codigo, "ejemplares_codigo_key").IsUnique();

            entity.HasIndex(e => e.LibroId, "ix_ejemplares_libro_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasMaxLength(30)
                .HasColumnName("codigo");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'DISPONIBLE'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.LibroId).HasColumnName("libro_id");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(100)
                .HasColumnName("ubicacion");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Libro).WithMany(p => p.Ejemplares)
                .HasForeignKey(d => d.LibroId)
                .HasConstraintName("ejemplares_libro_id_fkey");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("libros_pkey");

            entity.ToTable("libros");

            entity.HasIndex(e => e.EditorialId, "ix_libros_editorial_id");

            entity.HasIndex(e => e.Titulo, "ix_libros_titulo");

            entity.HasIndex(e => e.Isbn, "libros_isbn_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AnioPublicacion).HasColumnName("anio_publicacion");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.EditorialId).HasColumnName("editorial_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'ACTIVO'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("isbn");
            entity.Property(e => e.NumeroPaginas).HasColumnName("numero_paginas");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .HasColumnName("titulo");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Editorial).WithMany(p => p.Libros)
                .HasForeignKey(d => d.EditorialId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("libros_editorial_id_fkey");

            entity.HasMany(d => d.Autors).WithMany(p => p.Libros)
                .UsingEntity<Dictionary<string, object>>(
                    "LibroAutore",
                    r => r.HasOne<Autore>().WithMany()
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("libro_autores_autor_id_fkey"),
                    l => l.HasOne<Libro>().WithMany()
                        .HasForeignKey("LibroId")
                        .HasConstraintName("libro_autores_libro_id_fkey"),
                    j =>
                    {
                        j.HasKey("LibroId", "AutorId").HasName("libro_autores_pkey");
                        j.ToTable("libro_autores");
                        j.IndexerProperty<Guid>("LibroId").HasColumnName("libro_id");
                        j.IndexerProperty<Guid>("AutorId").HasColumnName("autor_id");
                    });

            entity.HasMany(d => d.Categoria).WithMany(p => p.Libros)
                .UsingEntity<Dictionary<string, object>>(
                    "LibroCategoria",
                    r => r.HasOne<Categoria>().WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("libro_categorias_categoria_id_fkey"),
                    l => l.HasOne<Libro>().WithMany()
                        .HasForeignKey("LibroId")
                        .HasConstraintName("libro_categorias_libro_id_fkey"),
                    j =>
                    {
                        j.HasKey("LibroId", "CategoriaId").HasName("libro_categorias_pkey");
                        j.ToTable("libro_categorias");
                        j.IndexerProperty<Guid>("LibroId").HasColumnName("libro_id");
                        j.IndexerProperty<Guid>("CategoriaId").HasColumnName("categoria_id");
                    });
        });

        modelBuilder.Entity<Multa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("multas_pkey");

            entity.ToTable("multas");

            entity.HasIndex(e => e.PrestamoId, "multas_prestamo_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'PENDIENTE'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.Monto)
                .HasPrecision(10, 2)
                .HasColumnName("monto");
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .HasColumnName("motivo");
            entity.Property(e => e.PrestamoId).HasColumnName("prestamo_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Prestamo).WithOne(p => p.Multa)
                .HasForeignKey<Multa>(d => d.PrestamoId)
                .HasConstraintName("multas_prestamo_id_fkey");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prestamos_pkey");

            entity.ToTable("prestamos");

            entity.HasIndex(e => e.EjemplarId, "ix_prestamos_ejemplar_id");

            entity.HasIndex(e => e.Estado, "ix_prestamos_estado");

            entity.HasIndex(e => e.UsuarioId, "ix_prestamos_usuario_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.EjemplarId).HasColumnName("ejemplar_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'ACTIVO'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaDevolucion).HasColumnName("fecha_devolucion");
            entity.Property(e => e.FechaPrestamo)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("fecha_prestamo");
            entity.Property(e => e.FechaVencimiento).HasColumnName("fecha_vencimiento");
            entity.Property(e => e.Observacion)
                .HasMaxLength(250)
                .HasColumnName("observacion");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Ejemplar).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.EjemplarId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("prestamos_ejemplar_id_fkey");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("prestamos_usuario_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Nombre, "roles_nombre_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.RolId, "ix_usuarios_rol_id");

            entity.HasIndex(e => e.Dni, "usuarios_dni_key").IsUnique();

            entity.HasIndex(e => e.Email, "usuarios_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .HasColumnName("apellidos");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.Dni)
                .HasMaxLength(20)
                .HasColumnName("dni");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'ACTIVO'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .HasColumnName("nombres");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .HasColumnName("telefono");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("usuarios_rol_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
