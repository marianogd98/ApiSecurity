using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class BIOContext : DbContext
    {
        public BIOContext()
        {
        }

        public BIOContext(DbContextOptions<BIOContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Departamentos> Departamentos { get; set; }
        public virtual DbSet<DetallesxTurnos> DetallesxTurnos { get; set; }
        public virtual DbSet<UsuariosxTurnos> UsuariosxTurnos { get; set; }
        public virtual DbSet<Turnos> Turnos { get; set; }
        public virtual DbSet<Marcajes> Marcajes { get; set; }
        public virtual DbSet<Estados> Estados { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=(local);Database=RIOPOS;UID=sa;PWD=12345678;");//pc local
                //optionsBuilder.UseSqlServer("Server=172.50.3.54;Database=RIOPOS;UID=apiuser;PWD=123456;");//ruben Oficina
                //optionsBuilder.UseSqlServer("Server=172.50.0.15;Database=RIOPOS;UID=apiuser;PWD=123456;");//INSIDE
                //optionsBuilder.UseSqlServer("Server=172.17.0.15;Database=RIOPOS;UID=apiuser;PWD=123456;");//JB
                //optionsBuilder.UseSqlServer("Server=172.18.0.15;Database=RIOPOS;UID=apiuser;PWD=123456;");//Traki
                //optionsBuilder.UseSqlServer("Server=172.19.0.15;Database=RIOPOS;UID=apiuser;PWD=123456;");//playa Nuevo
                //optionsBuilder.UseSqlServer("Server=172.54.0.15;Database=RIOPOS;UID=apiuser;PWD=123456;");//terranova
                optionsBuilder.UseSqlServer("Server=172.18.0.15;Database=BDBioTRKSQL;UID=sa;PWD=;");//terranova biotrack
                //optionsBuilder.UseSqlServer("Server=172.21.0.15;Database=RIOPOS;UID=apiuser;PWD=123456;");//31Julio
                //optionsBuilder.UseSqlServer("Server=172.22.0.15;Database=RIOPOS;UID=apiuser;PWD=123456;");//JGO
                //optionsBuilder.UseSqlServer("Server=172.23.0.15;Database=RIOPOS;UID=apiseguridad;PWD=123456;");//Sambil
                //optionsBuilder.UseSqlServer("Server=172.24.0.15;Database=RIOPOS;UID=apiuser;PWD=123456;");//palma caribe
                //optionsBuilder.UseSqlServer("Server=172.25.0.15;Database=RIOPOS;UID=apiuser;PWD=123456;");//maturim
                //optionsBuilder.UseSqlServer("Server=20.228.173.33;Database=RIOPOS;UID=apiusermat2;PWD=MAT123456**;");//maturim

                //optionsBuilder.UseSqlServer("Server=172.50.3.11;Database=RIOPOS;UID=apiuser;PWD=123456;");//Pedro chachadas
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Usuarios");

                entity.Property(e => e.IdUser).HasColumnName("IdUser");

                entity.Property(e => e.Name).HasColumnName("Name");

                entity.Property(e => e.IdDepartament).HasColumnName("IdDepartament");

                entity.Property(e => e.Position).HasColumnName("Position");

                entity.Property(e => e.Active).HasColumnName("Active");
            });

            modelBuilder.Entity<Estados>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Estados");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Accion).HasColumnName("Accion");

                entity.Property(e => e.TipoAccion).HasColumnName("TipoAccion");
            });

            modelBuilder.Entity<Departamentos>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Departamentos");

                entity.Property(e => e.IdDepartament).HasColumnName("IdDepartament");

                entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
            });

            modelBuilder.Entity<Marcajes>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Marcajes");

                entity.Property(e => e.IdUser).HasColumnName("IdUser");

                entity.Property(e => e.RecordTime).HasColumnName("RecordTime");
            });

            modelBuilder.Entity<Turnos>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Turnos");

                entity.Property(e => e.IdTurno).HasColumnName("IdTurno");

                entity.Property(e => e.Descripcion).HasColumnName("Descripcion");

                entity.Property(e => e.Ciclo).HasColumnName("Ciclo");

                entity.Property(e => e.Activo).HasColumnName("Activo");

                entity.Property(e => e.HoraInicio).HasColumnName("HoraInicio");

                entity.Property(e => e.HoraFin).HasColumnName("HoraFin");
            });

            modelBuilder.Entity<UsuariosxTurnos>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UsuariosxTurnos");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.IdUser).HasColumnName("IdUser");

                entity.Property(e => e.IdTurno).HasColumnName("IdTurno");

                entity.Property(e => e.FechaInicio).HasColumnName("FechaInicio");

                entity.Property(e => e.FechaFin).HasColumnName("FechaFin");

                entity.Property(e => e.Activo).HasColumnName("Activo");
            });

            modelBuilder.Entity<DetallesxTurnos>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DetallesxTurnos");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.IdTurno).HasColumnName("IdTurno");

                entity.Property(e => e.Descripcion).HasColumnName("Descripcion");
            });

            modelBuilder.Entity<SpCrearHorarios>().HasNoKey();
            modelBuilder.Entity<SpAsignarHorariosxEmpleados>().HasNoKey();
            modelBuilder.Entity<SpAsignarDiasxTurnos>().HasNoKey();
            modelBuilder.Entity<SpBuscarTurnos>().HasNoKey();
            modelBuilder.Entity<SpBuscarEmpleados>().HasNoKey();

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
