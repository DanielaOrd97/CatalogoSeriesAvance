using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CatalogoSeries.Models
{
    public partial class CatalogoseriesContext : DbContext
    {
        public CatalogoseriesContext()
        {
        }

        public CatalogoseriesContext(DbContextOptions<CatalogoseriesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Series> Series { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=Catalogoseries", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Series>(entity =>
            {
                entity.ToTable("series");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.Genero).HasMaxLength(150);

                entity.Property(e => e.Imagen).HasColumnType("mediumtext");

                entity.Property(e => e.Nombre).HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
