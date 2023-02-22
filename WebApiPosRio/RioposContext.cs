using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApiPosRio.Models.DB;

#nullable disable

namespace WebApiPosRio
{
    public partial class RioposContext : DbContext
    {
        public RioposContext()
        {
        }

        public RioposContext(DbContextOptions<RioposContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MovimientoWallet> MovimientoWallets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=172.50.0.15;Database=Riopos;User id=apiuser;password=123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<MovimientoWallet>(entity =>
            {
                entity.ToTable("MovimientoWallet");

                entity.HasIndex(e => e.Rowguid, "MSmerge_index_1561452794")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Monto).HasColumnType("money");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.TiendaId).HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
