using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OPERACION.CORE.Entities;

namespace OPERACION.INFRASTRUCTURE.Data
{
    public partial class BD_TRANSACCIONESContext : DbContext
    {
        public BD_TRANSACCIONESContext()
        {
        }

        public BD_TRANSACCIONESContext(DbContextOptions<BD_TRANSACCIONESContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cuenta> Cuenta { get; set; }
        public virtual DbSet<Movimiento> Movimiento { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.NroCuenta)
                    .HasName("cuenta_nro_cuenta_pk");

                entity.Property(e => e.NroCuenta)
                    .HasColumnName("NRO_CUENTA")
                    .HasMaxLength(14);

                entity.Property(e => e.Moneda)
                    .IsRequired()
                    .HasColumnName("MONEDA")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(40);

                entity.Property(e => e.Saldo)
                    .HasColumnName("SALDO")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnName("TIPO")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => new { e.NroCuenta, e.Fecha })
                    .HasName("movimiento_nro_cuenta_fecha_pk");

                entity.Property(e => e.NroCuenta)
                    .HasColumnName("NRO_CUENTA")
                    .HasMaxLength(14);

                entity.Property(e => e.Fecha)
                    .HasColumnName("FECHA")
                    .HasColumnType("datetime");

                entity.Property(e => e.Importe)
                    .HasColumnName("IMPORTE")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnName("TIPO")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.NroCuentaNavigation)
                    .WithMany(p => p.Movimiento)
                    .HasForeignKey(d => d.NroCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movimiento_Cuenta");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
