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
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<FieldDataSourceDetails> FieldDataSourceDetails { get; set; }
        public virtual DbSet<FieldDataSources> FieldDataSources { get; set; }
        public virtual DbSet<Fields> Fields { get; set; }
        public virtual DbSet<FormDataDetails> FormDataDetails { get; set; }
        public virtual DbSet<FormDataHistories> FormDataHistories { get; set; }
        public virtual DbSet<FormDataHistoryDetails> FormDataHistoryDetails { get; set; }
        public virtual DbSet<FormDatas> FormDatas { get; set; }
        public virtual DbSet<Forms> Forms { get; set; }
        public virtual DbSet<ProductStatusHistories> ProductStatusHistories { get; set; }
        public virtual DbSet<Sections> Sections { get; set; }
        public virtual DbSet<TypingProcesses> TypingProcesses { get; set; }
        public virtual DbSet<UserInForms> UserInForms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.Instance.GetConnectionString("ImaginCrud_Connection"));
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-S3RTP7K\LUCHO;Database=ImaginCrud;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
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

            modelBuilder.Entity<FieldDataSourceDetails>(entity =>
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

            modelBuilder.Entity<FieldDataSources>(entity =>
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

            modelBuilder.Entity<Fields>(entity =>
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

            modelBuilder.Entity<FormDataDetails>(entity =>
            {
                entity.HasKey(e => new { e.FieldId, e.FormDataId });

                entity.HasIndex(e => e.FormDataId)
                    .HasName("IX_FormDataId");

                entity.Property(e => e.Value)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.HasOne(d => d.FormData)
                    .WithMany(p => p.FormDataDetails)
                    .HasForeignKey(d => d.FormDataId)
                    .HasConstraintName("FK_dbo.FormDataDetails_dbo.FormDatas_FormDataId");
            });

            modelBuilder.Entity<FormDataHistories>(entity =>
            {
                entity.HasKey(e => e.FormDataId);

                entity.HasIndex(e => new { e.TypingProcessId, e.FormId })
                    .HasName("IX_TypingProcessId_FormId");

                entity.Property(e => e.FormDataId).ValueGeneratedNever();

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
                    .WithMany(p => p.FormDataHistories)
                    .HasForeignKey(d => new { d.TypingProcessId, d.FormId })
                    .HasConstraintName("FK_dbo.FormDataHistories_dbo.TypingProcesses_TypingProcessId_FormId");
            });

            modelBuilder.Entity<FormDataHistoryDetails>(entity =>
            {
                entity.HasKey(e => new { e.FieldId, e.FormDataId });

                entity.HasIndex(e => e.FormDataId)
                    .HasName("IX_FormDataId");

                entity.Property(e => e.Value)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.HasOne(d => d.FormData)
                    .WithMany(p => p.FormDataHistoryDetails)
                    .HasForeignKey(d => d.FormDataId)
                    .HasConstraintName("FK_dbo.FormDataHistoryDetails_dbo.FormDataHistories_FormDataId");
            });

            modelBuilder.Entity<FormDatas>(entity =>
            {
                entity.HasKey(e => e.FormDataId);

                entity.HasIndex(e => new { e.TypingProcessId, e.FormId })
                    .HasName("IX_TypingProcessId_FormId");

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
                    .WithMany(p => p.FormDatas)
                    .HasForeignKey(d => new { d.TypingProcessId, d.FormId })
                    .HasConstraintName("FK_dbo.FormDatas_dbo.TypingProcesses_TypingProcessId_FormId");
            });

            modelBuilder.Entity<Forms>(entity =>
            {
                entity.HasKey(e => e.FormId);

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
                    .WithMany(p => p.Forms)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_dbo.Forms_dbo.Customers_CustomerId");
            });
            
            modelBuilder.Entity<ProductStatusHistories>(entity =>
            {
                entity.HasKey(e => e.ProductStatusHistoryId);

                entity.HasIndex(e => new { e.TypingProcessId, e.FormId })
                    .HasName("IX_TypingProcessId_FormId");

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
                    .WithMany(p => p.ProductStatusHistories)
                    .HasForeignKey(d => new { d.TypingProcessId, d.FormId })
                    .HasConstraintName("FK_dbo.ProductStatusHistories_dbo.TypingProcesses_TypingProcessId_FormId");
            });

            modelBuilder.Entity<Sections>(entity =>
            {
                entity.HasKey(e => e.SectionId);

                entity.HasIndex(e => e.FormId)
                    .HasName("IX_FormId");

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

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.FormId)
                    .HasConstraintName("FK_dbo.Sections_dbo.Forms_FormId");
            });

            modelBuilder.Entity<TypingProcesses>(entity =>
            {
                entity.HasKey(e => new { e.TypingProcessId, e.FormId });

                entity.HasIndex(e => e.FormId)
                    .HasName("IX_FormId");

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

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.TypingProcesses)
                    .HasForeignKey(d => d.FormId)
                    .HasConstraintName("FK_dbo.TypingProcesses_dbo.Forms_FormId");
            });

            modelBuilder.Entity<UserInForms>(entity =>
            {
                entity.HasKey(e => new { e.UserFunction, e.UserName, e.FormId });

                entity.HasIndex(e => e.FormId)
                    .HasName("IX_FormId");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Form)
                    .WithMany(p => p.UserInForms)
                    .HasForeignKey(d => d.FormId)
                    .HasConstraintName("FK_dbo.UserInForms_dbo.Forms_FormId");
            });
        }
    }
}
