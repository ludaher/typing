using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Alcaze.IC.Typing.DTO;
using Alcaze.IC.Typing.DTO.PersistenceEntities;
using Microsoft.Extensions.Configuration;
using Alcaze.IC.Typing.DTO.CustomEntities;

namespace Alcaze.IC.Typing.DAL
{
    public partial class ImaginCrudContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<FieldDataSourceDetail> FieldDataSourceDetails { get; set; }
        public virtual DbSet<FieldDataSource> FieldDataSources { get; set; }
        public virtual DbSet<Field> Fields { get; set; }
        public virtual DbSet<CaptureDetail> CaptureDetails { get; set; }
        public virtual DbSet<CaptureHistory> CaptureHistories { get; set; }
        public virtual DbSet<CaptureHistoryDetail> CaptureHistoryDetails { get; set; }
        public virtual DbSet<Capture> Captures { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductStateHistory> ProductStateHistories { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<TypingProcesses> TypingProcesses { get; set; }
        public virtual DbSet<UserInProduct> UserInProduct { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.Instance.GetConnectionString("ImaginCrud_Connection"));
                //optionsBuilder.UseSqlServer(@"Server=DESKTOP-S3RTP7K\LUCHO;Database=ImaginCrud;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CustomerId).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FieldDataSourceDetail>(entity =>
            {
                entity.HasKey(e => e.FieldDataSourceDetailId);

                entity.HasIndex(e => e.FieldDataSourceId)
                    .HasName("IX_FieldDataSourceId");

                entity.Property(e => e.Label)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.FieldDataSource)
                    .WithMany(p => p.FieldDataSourceDetails)
                    .HasForeignKey(d => d.FieldDataSourceId)
                    .HasConstraintName("FK_dbo.FieldDataSourceDetails_dbo.FieldDataSources_FieldDataSourceId");
            });

            modelBuilder.Entity<FieldDataSource>(entity =>
            {
                entity.HasKey(e => e.FieldDataSourceId);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Field>(entity =>
            {
                entity.HasKey(e => e.FieldId);

                entity.HasIndex(e => e.ParentFieldId)
                    .HasName("IX_ParentFieldId");

                entity.HasIndex(e => e.SectionId)
                    .HasName("IX_SectionId");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DefaultValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Options)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Validation)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ParentField)
                    .WithMany(p => p.InverseParentField)
                    .HasForeignKey(d => d.ParentFieldId)
                    .HasConstraintName("FK_dbo.Fields_dbo.Fields_ParentFieldId");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.SectionId)
                    .HasConstraintName("FK_dbo.Fields_dbo.Sections_SectionId");
            });

            modelBuilder.Entity<CaptureDetail>(entity =>
            {
                entity.HasKey(e => new { e.FieldId, e.CaptureId });

                entity.HasIndex(e => e.CaptureId)
                    .HasName("IX_CaptureId");

                entity.Property(e => e.Value)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Capture)
                    .WithMany(p => p.CapturtDetails)
                    .HasForeignKey(d => d.CaptureId)
                    .HasConstraintName("FK_dbo.CaptureDetails_dbo.Captures_CaptureId");
            });

            modelBuilder.Entity<CaptureHistory>(entity =>
            {
                entity.HasKey(e => e.CaptureId);

                entity.HasIndex(e => new { e.TypingProcessId, e.ProductId })
                    .HasName("IX_TypingProcessId_ProductId");

                entity.Property(e => e.CaptureId).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.TypingProcessId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.TypingProcesses)
                    .WithMany(p => p.CaptureHistories)
                    .HasForeignKey(d => new { d.TypingProcessId, d.ProductId })
                    .HasConstraintName("FK_dbo.CaptureHistories_dbo.TypingProcesses_TypingProcessId_ProductId");
            });

            modelBuilder.Entity<CaptureHistoryDetail>(entity =>
            {
                entity.HasKey(e => new { e.FieldId, e.CaptureId });

                entity.HasIndex(e => e.CaptureId)
                    .HasName("IX_CaptureId");

                entity.Property(e => e.Value)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.HasOne(d => d.CaptureHistory)
                    .WithMany(p => p.CaptureHistoryDetails)
                    .HasForeignKey(d => d.CaptureId)
                    .HasConstraintName("FK_dbo.CaptureHistoryDetails_dbo.CaptureHistories_CaptureId");
            });

            modelBuilder.Entity<Capture>(entity =>
            {
                entity.HasKey(e => e.CaptureId);

                entity.HasIndex(e => new { e.TypingProcessId, e.ProductId })
                    .HasName("IX_TypingProcessId_ProductId");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.TypingProcessId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.TypingProcesses)
                    .WithMany(p => p.Captures)
                    .HasForeignKey(d => new { d.TypingProcessId, d.ProductId })
                    .HasConstraintName("FK_dbo.Captures_dbo.TypingProcesses_TypingProcessId_ProductId");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.HasIndex(e => e.CustomerId)
                    .HasName("IX_CustomerId");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TemplatePath)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_dbo.Products_dbo.Customers_CustomerId");
            });
            
            modelBuilder.Entity<ProductStateHistory>(entity =>
            {
                entity.HasKey(e => e.ProductStateHistoryId);

                entity.HasIndex(e => new { e.TypingProcessId, e.ProductId })
                    .HasName("IX_TypingProcessId_ProductId");

                entity.Property(e => e.ModifiedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Observations)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TypingProcessId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.TypingProcesses)
                    .WithMany(p => p.ProductStateHistories)
                    .HasForeignKey(d => new { d.TypingProcessId, d.ProductId })
                    .HasConstraintName("FK_dbo.ProductStateHistories_dbo.TypingProcesses_TypingProcessId_ProductId");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(e => e.SectionId);

                entity.HasIndex(e => e.ProductId)
                    .HasName("IX_ProductId");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.SectionName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_dbo.Sections_dbo.Products_ProductId");
            });

            modelBuilder.Entity<TypingProcesses>(entity =>
            {
                entity.HasKey(e => new { e.TypingProcessId, e.ProductId });

                entity.HasIndex(e => e.ProductId)
                    .HasName("IX_ProductId");

                entity.Property(e => e.TypingProcessId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Observations)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProductionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.TypingProcesses)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_dbo.TypingProcesses_dbo.Products_ProductId");
            });

            modelBuilder.Entity<UserInProduct>(entity =>
            {
                entity.HasKey(e => new { e.UserFunction, e.UserName, e.ProductId });

                entity.HasIndex(e => e.ProductId)
                    .HasName("IX_ProductId");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.UserInProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_dbo.UserInProduct_dbo.Products_ProductId");
            });
        }
    }
}
