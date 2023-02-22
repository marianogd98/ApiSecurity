using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApiPosRio.Models.DB
{
    public partial class RIOPOSContext : DbContext
    {
        public RIOPOSContext()
        {
        }

        public RIOPOSContext(DbContextOptions<RIOPOSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Acceso> Accesos { get; set; }
        public virtual DbSet<Accion> Accions { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Arqueo> Arqueos { get; set; }
        public virtual DbSet<Banco> Bancos { get; set; }
        public virtual DbSet<Caja> Cajas { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<CodigoBarra> CodigoBarras { get; set; }
        public virtual DbSet<Combo> Combos { get; set; }
        public virtual DbSet<ComboProducto> ComboProductos { get; set; }
        public virtual DbSet<ConceptoArqueo> ConceptoArqueos { get; set; }
        public virtual DbSet<ConfigBoton> ConfigBotons { get; set; }
        public virtual DbSet<CuentasBancaria> CuentasBancarias { get; set; }
        public virtual DbSet<DesgloseDolar> DesgloseDolars { get; set; }
        public virtual DbSet<DesglosePunto> DesglosePuntos { get; set; }
        public virtual DbSet<DetalleArqueo> DetalleArqueos { get; set; }
        public virtual DbSet<DetalleDevolucion> DetalleDevolucions { get; set; }
        public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }
        public virtual DbSet<Devolucion> Devolucions { get; set; }
        public virtual DbSet<Donacion> Donacions { get; set; }
        public virtual DbSet<ErrorDevolucion> ErrorDevolucions { get; set; }
        public virtual DbSet<ErrorFactura> ErrorFacturas { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<FacturaTemp> FacturaTemps { get; set; }
        public virtual DbSet<FormaPago> FormaPagos { get; set; }
        public virtual DbSet<Modulo> Modulos { get; set; }
        public virtual DbSet<MovimientoWallet> MovimientoWallets { get; set; }
        public virtual DbSet<Organizacion> Organizacions { get; set; }
        public virtual DbSet<PagoFactura> PagoFacturas { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<RolAccion> RolAccions { get; set; }
        public virtual DbSet<Serial> Serials { get; set; }
        public virtual DbSet<Tempuser> Tempusers { get; set; }
        public virtual DbSet<Tiendum> Tienda { get; set; }
        public virtual DbSet<TmpDetalleMercha> TmpDetalleMerchas { get; set; }
        public virtual DbSet<TmpDetalleMercha1> TmpDetalleMercha1s { get; set; }
        public virtual DbSet<TmpMerchan> TmpMerchans { get; set; }
        public virtual DbSet<TmpMerchan1> TmpMerchan1s { get; set; }
        public virtual DbSet<Turno> Turnos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<UsuarioAccion> UsuarioAccions { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }
        public virtual DbSet<PromoSeptiembre> PromocionSeptiembre { get; set; }
        public virtual DbSet<Metum> Meta { get; set; }
        public virtual DbSet<Promocion> Promociones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=(local);Database=RIOPOS;UID=sa;PWD=12345678;");//pc local
                //optionsBuilder.UseSqlServer("Server=172.50.3.54;Database=RIOPOS;UID=apiuser;PWD=123456;");//ruben Oficina
                //optionsBuilder.UseSqlServer(_connectionString);
                //optionsBuilder.UseSqlServer("Server=172.50.0.15;Database=RIOPOS;UID=apiuser;PWD=123456;");//INSIDE
                //optionsBuilder.UseSqlServer("Server=172.17.0.15;Database=RIOPOS;UID=apiuser;PWD=123456;");//JB
                //optionsBuilder.UseSqlServer("Server=172.18.0.15;Database=RIOPOS;UID=apiuser;PWD=123456;");//Traki
                //optionsBuilder.UseSqlServer("Server=172.19.0.15;Database=RIOPOS;UID=apiuser;PWD=123456;");//playa Nuevo
                //optionsBuilder.UseSqlServer("Server=172.54.0.15;Database=RIOPOS;UID=apiuser;PWD=123456;");//terranova
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
            modelBuilder.Entity<Acceso>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Acceso");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MacAdress).HasColumnName("mac_adress");
            });

            modelBuilder.Entity<Accion>(entity =>
            {
                entity.ToTable("Accion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Modulo)
                    .WithMany(p => p.Accions)
                    .HasForeignKey(d => d.ModuloId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("accion_moduloid_foreign");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Tienda)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.TiendaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Area_Tienda");
            });

            modelBuilder.Entity<Arqueo>(entity =>
            {
                entity.ToTable("Arqueo");

                entity.HasIndex(e => e.Id, "UX_Turno")
                    .IsUnique();

                entity.Property(e => e.Estatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Observacion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Tienda)
                    .WithMany(p => p.Arqueos)
                    .HasForeignKey(d => d.TiendaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Arqueo_Tienda");

                entity.HasOne(d => d.Turno)
                    .WithMany(p => p.Arqueos)
                    .HasForeignKey(d => d.TurnoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Arqueo_Turno");
            });

            modelBuilder.Entity<Banco>(entity =>
            {
                entity.ToTable("Banco");

                entity.Property(e => e.Codigo).HasDefaultValueSql("((134))");

                entity.Property(e => e.CodigoStellar)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Estatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Caja>(entity =>
            {
                entity.ToTable("Caja");

                entity.HasIndex(e => e.CodigoCaja, "UK_CodigoCaja")
                    .IsUnique();

                entity.Property(e => e.BancoId).HasDefaultValueSql("((1))");

                entity.Property(e => e.CodigoCaja)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Consecutivo).HasDefaultValueSql("((1))");

                entity.Property(e => e.PuertoBalanza)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PuertoCodigoBarra)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PuertoImpresora)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SerialImpresora)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Vtid)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("VTID");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Apellido).HasMaxLength(255);

                entity.Property(e => e.CodigoTipoCliente).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Estatus)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nit)
                    .HasMaxLength(10)
                    .HasColumnName("NIT");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.RazonComercial).HasMaxLength(255);

                entity.Property(e => e.Rif)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("RIF");

                entity.Property(e => e.Telefono).HasMaxLength(15);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<CodigoBarra>(entity =>
            {
                entity.ToTable("CodigoBarra");

                entity.HasIndex(e => e.CodigoBarra1, "UK_Barra")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CodigoBarra1)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CodigoBarra");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.CodigoBarras)
                    .HasForeignKey(d => d.ProductoId)
                    .HasConstraintName("FK_CodigoBarra_Producto");
            });

            modelBuilder.Entity<Combo>(entity =>
            {
                entity.ToTable("Combo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Barra).HasMaxLength(20);

                entity.Property(e => e.CodigoCombo)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaIni).HasColumnType("datetime");
            });

            modelBuilder.Entity<ComboProducto>(entity =>
            {
                entity.ToTable("ComboProducto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.Combo)
                    .WithMany(p => p.ComboProductos)
                    .HasForeignKey(d => d.ComboId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComboProducto_Combo");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.ComboProductos)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComboProducto_Producto");
            });

            modelBuilder.Entity<ConceptoArqueo>(entity =>
            {
                entity.ToTable("ConceptoArqueo");

                entity.Property(e => e.Abreviatura)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoMoneda)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ConfigBoton>(entity =>
            {
                entity.ToTable("ConfigBoton");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Imagen)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.ConfigBotons)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK_ConfigBoton_Area");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.ConfigBotons)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConfigBoton_Producto");
            });

            modelBuilder.Entity<CuentasBancaria>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.NumeroCuenta)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<DesgloseDolar>(entity =>
            {
                entity.ToTable("DesgloseDolar");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Moneda).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<DesglosePunto>(entity =>
            {
                entity.ToTable("DesglosePunto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.Arqueo)
                    .WithMany(p => p.DesglosePuntos)
                    .HasForeignKey(d => d.ArqueoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DesglosePunto_Arqueo");

                entity.HasOne(d => d.CuentaBancaria)
                    .WithMany(p => p.DesglosePuntos)
                    .HasForeignKey(d => d.CuentaBancariaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DesglosePunto_CuentasBancarias");
            });

            modelBuilder.Entity<DetalleArqueo>(entity =>
            {
                entity.ToTable("DetalleArqueo");

                entity.Property(e => e.Tipo)
                    .HasDefaultValueSql("((1))")
                    .HasComment("1: Formapago\r\n2: ConceptoArqueo (Ajuste)");

                entity.HasOne(d => d.Arqueo)
                    .WithMany(p => p.DetalleArqueos)
                    .HasForeignKey(d => d.ArqueoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleArqueo_Arqueo");
            });

            modelBuilder.Entity<DetalleDevolucion>(entity =>
            {
                entity.ToTable("DetalleDevolucion");

                entity.HasIndex(e => new { e.ProductoId, e.FacturaId }, "UK_ProductoFactura")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DevolucionId).HasColumnName("devolucionId");

                entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Devolucion)
                    .WithMany(p => p.DetalleDevolucions)
                    .HasForeignKey(d => d.DevolucionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleDevolucion_Devolucion");

                entity.HasOne(d => d.Factura)
                    .WithMany(p => p.DetalleDevolucions)
                    .HasForeignKey(d => d.FacturaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleDevolucion_Factura");
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.ToTable("DetalleFactura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Iva).HasColumnName("IVA");

                entity.Property(e => e.Serial)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoProducto).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Factura)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.FacturaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetalleFactura_Factura");
            });

            modelBuilder.Entity<Devolucion>(entity =>
            {
                entity.ToTable("Devolucion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FormaPago)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.NumeroDevolucion)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SerialFiscal)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");

                entity.HasOne(d => d.Caja)
                    .WithMany(p => p.Devolucions)
                    .HasForeignKey(d => d.CajaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Devolucion_Caja");

                entity.HasOne(d => d.Factura)
                    .WithMany(p => p.Devolucions)
                    .HasForeignKey(d => d.FacturaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("devolucion_facturaid_foreign");

                entity.HasOne(d => d.Tienda)
                    .WithMany(p => p.Devolucions)
                    .HasForeignKey(d => d.TiendaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Devolucion_Tienda");

                entity.HasOne(d => d.Turno)
                    .WithMany(p => p.Devolucions)
                    .HasForeignKey(d => d.TurnoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Devolucion_Turno");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Devolucions)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Devolucion_Usuario");
            });

            modelBuilder.Entity<Donacion>(entity =>
            {
                entity.ToTable("Donacion");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.NroAutorizacion).HasMaxLength(50);

                entity.Property(e => e.NumeroTransacion).HasMaxLength(15);

                entity.Property(e => e.TipoTarjeta).HasMaxLength(10);

                entity.Property(e => e.Vposdata)
                    .HasColumnType("xml")
                    .HasColumnName("VPOSData");
            });

            modelBuilder.Entity<ErrorDevolucion>(entity =>
            {
                entity.ToTable("ErrorDevolucion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FormaPago)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroDevolucion)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Productos).HasColumnType("xml");

                entity.Property(e => e.SerialFiscal)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ErrorFactura>(entity =>
            {
                entity.ToTable("ErrorFactura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MensajeError)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MontoIva).HasColumnName("MontoIVA");

                entity.Property(e => e.NumeroFactura)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Productos)
                    .IsRequired()
                    .HasColumnType("xml");

                entity.Property(e => e.Serialfilcal)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("Factura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodigoAfiliacion).HasMaxLength(255);

                entity.Property(e => e.CodigoArea).HasMaxLength(255);

                entity.Property(e => e.Estatus).HasComment("0: Creado\r\n1: Impreso\r\n2: Anulado\r\n3 Devolucion Parcial\r\n4:Devolucion total");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GuiaLicor).HasMaxLength(255);

                entity.Property(e => e.MontoIva).HasColumnName("MontoIVA");

                entity.Property(e => e.NumeroControl)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.NumeroFactura)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.SerialFiscal)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Tasa).HasColumnName("tasa");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Factura_Cliente");

                entity.HasOne(d => d.Turno)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.TurnoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Factura_Turno");
            });

            modelBuilder.Entity<FacturaTemp>(entity =>
            {
                entity.ToTable("FacturaTemp");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Productos)
                    .IsRequired()
                    .HasColumnType("xml");
            });

            modelBuilder.Entity<FormaPago>(entity =>
            {
                entity.ToTable("FormaPago");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CodigoMoneda)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("001: bolivar\r\n002: dolares");

                entity.Property(e => e.CorreoZelle).HasMaxLength(255);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Orden).HasColumnName("orden");

                entity.Property(e => e.Telefono).HasMaxLength(255);
            });

            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.ToTable("Modulo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<MovimientoWallet>(entity =>
            {
                entity.ToTable("MovimientoWallet");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Monto).HasColumnType("money");
            });

            modelBuilder.Entity<Organizacion>(entity =>
            {
                entity.ToTable("Organizacion");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Estatus)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Motivo).HasMaxLength(255);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Rif)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("RIF");

                entity.Property(e => e.Telefono).HasMaxLength(15);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<PagoFactura>(entity =>
            {
                entity.ToTable("PagoFactura");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BancoadquirienteId).HasColumnName("bancoadquirienteId");

                entity.Property(e => e.Lote)
                    .HasMaxLength(255)
                    .HasColumnName("lote");

                entity.Property(e => e.Nombre).HasMaxLength(255);

                entity.Property(e => e.NroAutorizacion).HasMaxLength(50);

                entity.Property(e => e.NumeroTransaccion).HasMaxLength(255);

                entity.Property(e => e.TipoTarjeta).HasMaxLength(50);

                entity.Property(e => e.Vposdata)
                    .HasColumnType("xml")
                    .HasColumnName("VPOSData");

                entity.HasOne(d => d.FormaPago)
                    .WithMany(p => p.PagoFacturas)
                    .HasForeignKey(d => d.FormaPagoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PagoFactura_FormaPago");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Producto");

                entity.HasIndex(e => e.CodigoProducto, "UK_Codigo")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodigoBalanza)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.CodigoMoneda)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.CodigoProducto)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.CodigoSubgrupo)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CodigoTipo)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FechaOfertaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaOfertaIni).HasColumnType("datetime");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");

                entity.HasIndex(e => e.Descripcion, "UK_Descripcion")
                    .IsUnique();

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<RolAccion>(entity =>
            {
                entity.ToTable("RolAccion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.Accion)
                    .WithMany(p => p.RolAccions)
                    .HasForeignKey(d => d.AccionId)
                    .HasConstraintName("rolaccion_accionid_foreign");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.RolAccions)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("rolaccion_rolid_foreign");
            });

            modelBuilder.Entity<Serial>(entity =>
            {
                entity.ToTable("Serial");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Serial1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Serial");
            });

            modelBuilder.Entity<Tempuser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tempuser");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("cedula");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("usuario");
            });

            modelBuilder.Entity<Tiendum>(entity =>
            {
                entity.HasIndex(e => e.CodigoTienda, "tienda_codigotienda_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AplicaIva).HasColumnName("AplicaIVA");

                entity.Property(e => e.CodigoArea)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CodigoTienda)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Tasa).HasColumnName("tasa");

                entity.Property(e => e.Urlapi)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("URLAPI");
            });

            modelBuilder.Entity<TmpDetalleMercha>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TMP_Detalle_Mercha");

                entity.Property(e => e.Afiliacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("afiliacion");

                entity.Property(e => e.Aprobacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aprobacion");

                entity.Property(e => e.Banco)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("banco");

                entity.Property(e => e.Comision).HasColumnName("comision");

                entity.Property(e => e.Fecha1)
                    .HasColumnType("date")
                    .HasColumnName("fecha1");

                entity.Property(e => e.Fecha2)
                    .HasColumnType("date")
                    .HasColumnName("fecha2");

                entity.Property(e => e.Hora1).HasColumnName("hora1");

                entity.Property(e => e.Hora2).HasColumnName("hora2");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lote");

                entity.Property(e => e.MontoBruto).HasColumnName("montoBruto");

                entity.Property(e => e.Neto).HasColumnName("neto");

                entity.Property(e => e.Retencion).HasColumnName("retencion");

                entity.Property(e => e.Tarjeta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tarjeta");

                entity.Property(e => e.TipoTarjeta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipoTarjeta");

                entity.Property(e => e.Vtid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("vtid");

                entity.Property(e => e.Vtide)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("vtide");
            });

            modelBuilder.Entity<TmpDetalleMercha1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TMP_Detalle_Mercha1");

                entity.Property(e => e.Afiliacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("afiliacion");

                entity.Property(e => e.Aprobacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aprobacion");

                entity.Property(e => e.Banco)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("banco");

                entity.Property(e => e.Comision)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comision");

                entity.Property(e => e.Fecha1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fecha1");

                entity.Property(e => e.Fecha2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fecha2");

                entity.Property(e => e.Hora1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("hora1");

                entity.Property(e => e.Hora2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("hora2");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lote");

                entity.Property(e => e.MontoBruto)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("montoBruto");

                entity.Property(e => e.Neto)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("neto");

                entity.Property(e => e.Retencion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("retencion");

                entity.Property(e => e.Tarjeta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tarjeta");

                entity.Property(e => e.TipoTarjeta)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipoTarjeta");

                entity.Property(e => e.Vtid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("vtid");

                entity.Property(e => e.Vtide)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("vtide");
            });

            modelBuilder.Entity<TmpMerchan>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TMP_Merchan");

                entity.Property(e => e.Actualizado)
                    .HasColumnName("actualizado")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Afiliacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("afiliacion");

                entity.Property(e => e.Banco)
                    .HasMaxLength(10)
                    .HasColumnName("banco")
                    .IsFixedLength(true);

                entity.Property(e => e.Bruto).HasColumnName("bruto");

                entity.Property(e => e.Comision).HasColumnName("comision");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.Hora)
                    .HasColumnType("datetime")
                    .HasColumnName("hora");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Lote).HasColumnName("lote");

                entity.Property(e => e.Neto).HasColumnName("neto");

                entity.Property(e => e.Retencion).HasColumnName("retencion");

                entity.Property(e => e.Terminal)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("terminal");

                entity.Property(e => e.Tipopago)
                    .HasMaxLength(50)
                    .HasColumnName("tipopago");
            });

            modelBuilder.Entity<TmpMerchan1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TMP_Merchan1");

                entity.Property(e => e.Afiliacion)
                    .HasMaxLength(50)
                    .HasColumnName("afiliacion");

                entity.Property(e => e.Banco)
                    .HasMaxLength(10)
                    .HasColumnName("banco")
                    .IsFixedLength(true);

                entity.Property(e => e.Bruto)
                    .HasMaxLength(50)
                    .HasColumnName("bruto");

                entity.Property(e => e.Comision)
                    .HasMaxLength(50)
                    .HasColumnName("comision");

                entity.Property(e => e.Fecha)
                    .HasMaxLength(50)
                    .HasColumnName("fecha");

                entity.Property(e => e.Hora)
                    .HasMaxLength(50)
                    .HasColumnName("hora");

                entity.Property(e => e.Lote)
                    .HasMaxLength(50)
                    .HasColumnName("lote");

                entity.Property(e => e.Neto)
                    .HasMaxLength(50)
                    .HasColumnName("neto");

                entity.Property(e => e.Retencion)
                    .HasMaxLength(50)
                    .HasColumnName("retencion");

                entity.Property(e => e.Terminal)
                    .HasMaxLength(50)
                    .HasColumnName("terminal");

                entity.Property(e => e.Tipopago)
                    .HasMaxLength(50)
                    .HasColumnName("tipopago");
            });

            modelBuilder.Entity<Turno>(entity =>
            {
                entity.ToTable("Turno");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Estatus)
                    .HasDefaultValueSql("((1))")
                    .HasComment("1: Abierto\r\n2: Cerrado\r\n3: Suspendido\r\n4: En Revision\r\n5: Verificado");

                entity.Property(e => e.Inicio).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Caja)
                    .WithMany(p => p.Turnos)
                    .HasForeignKey(d => d.CajaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Turno_Caja");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Turnos)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Turno_Usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.HasIndex(e => e.Cedula, "UK_cedula")
                    .IsUnique();

                entity.HasIndex(e => e.Nick, "UK_nick")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cedula");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Estatus)
                    .HasColumnName("estatus")
                    .HasComment("-1: Eliminado\r\n0: Inactivo\r\n1: Activo\r\n2: Online");

                entity.Property(e => e.Nick)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("nick");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.RecordarToken)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("recordar_token");

                entity.Property(e => e.Tipo)
                    .HasColumnName("tipo")
                    .HasComment("0: Super usuario\r\n1: Administradores\r\n11: Seguridad\r\n2: caja Administrativa (Supervisor)\r\n21: Receptor\r\n3: pos Rio");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Rol");
            });

            modelBuilder.Entity<UsuarioAccion>(entity =>
            {
                entity.ToTable("UsuarioAccion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.Accion)
                    .WithMany(p => p.UsuarioAccions)
                    .HasForeignKey(d => d.AccionId)
                    .HasConstraintName("usuarioaccion_accionid_foreign");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.UsuarioAccions)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_UsuarioAccion_Usuario");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("Wallet");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Saldo).HasColumnType("money");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_at")
                    .HasDefaultValueSql("(getdate())");
            });


            modelBuilder.Entity<Promocion>(entity =>
            {
                entity.ToTable("Promocion");

                entity.Property(e => e.Condiciones).IsRequired();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Estatus).HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaFin).HasColumnType("datetime");

                entity.Property(e => e.FechaInicio).HasColumnType("datetime");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_at")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<PromoSeptiembre>(entity =>
            {
                entity.ToTable("PromoSeptiembre");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Hora)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasDefaultValueSql("(N'10:00')");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("Updated_at")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Metum>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
            });

            //HasNokey for Stores procedures Enables
            modelBuilder.Entity<SpFacturadoXTurnoXFecha>().HasNoKey();
            modelBuilder.Entity<SpListadoDetalleFactura>().HasNoKey();
            modelBuilder.Entity<SpFacturadoXCajaXFecha>().HasNoKey();
            modelBuilder.Entity<SpMostrarCierresTurnoS4>().HasNoKey();
            modelBuilder.Entity<SpListadoFacturaRangFech>().HasNoKey();
            modelBuilder.Entity<SpFormasPagoFacturaId>().HasNoKey();
            modelBuilder.Entity<SpDevolucionByTurnoId>().HasNoKey();
            modelBuilder.Entity<SpReporteFormasPago>().HasNoKey();
            modelBuilder.Entity<SpReporteCierresToC>().HasNoKey();
            modelBuilder.Entity<SpMostrarCierresToC>().HasNoKey();
            modelBuilder.Entity<SpDeclaradoProcesado>().HasNoKey();
            modelBuilder.Entity<SpMostrarCierreCaja>().HasNoKey();
            modelBuilder.Entity<SpFaltanteSobrante>().HasNoKey();
            modelBuilder.Entity<SpResumenxCajaxFecha>().HasNoKey();
            modelBuilder.Entity<SpDetalleFormaPago>().HasNoKey();
            modelBuilder.Entity<SpListadoFactura>().HasNoKey();
            modelBuilder.Entity<SpListadoDevoluciones>().HasNoKey();
            modelBuilder.Entity<SpMostrarConsolidado>().HasNoKey();
            modelBuilder.Entity<SpListadoWallet>().HasNoKey();
            modelBuilder.Entity<SpDetalleMoneda>().HasNoKey();
            modelBuilder.Entity<SpDescloseDolar>().HasNoKey();
            modelBuilder.Entity<SpPruebaData>().HasNoKey();
            modelBuilder.Entity<CrudAccion>().HasNoKey();
            modelBuilder.Entity<SpAcciones>().HasNoKey();
            modelBuilder.Entity<SpArqueo>().HasNoKey();
            modelBuilder.Entity<SpGame>().HasNoKey();
            modelBuilder.Entity<SpProfomaSave>().HasNoKey();
            modelBuilder.Entity<SpListadoProveedor>().HasNoKey();
            modelBuilder.Entity<SpSaveAcreedor>().HasNoKey();
            modelBuilder.Entity<SpSaveFacturaProforma>().HasNoKey();
            modelBuilder.Entity<SpDesglosePuntos>().HasNoKey();
            modelBuilder.Entity<SpMostrarFacturaPromedio>().HasNoKey();
            modelBuilder.Entity<SpListaResumenVentaXFormaPago>().HasNoKey();
            modelBuilder.Entity<SpMostrarVentasXDep>().HasNoKey();
            modelBuilder.Entity<SpListadoResumenVenta>().HasNoKey();
            modelBuilder.Entity<SpListadoResumenVentaDepartamento>().HasNoKey();
            modelBuilder.Entity<SpListadoResumenTopVentaProducto>().HasNoKey();
            modelBuilder.Entity<SpListadoDonaciones>().HasNoKey();
            modelBuilder.Entity<SpBuscarTasas>().HasNoKey();
            modelBuilder.Entity<SpGuardarTasa>().HasNoKey();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
