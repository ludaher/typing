﻿// <auto-generated />
using Alcaze.IC.Typing.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Alcaze.IC.Typing.DAL.Migrations
{
    [DbContext(typeof(ImaginCrudContext))]
    [Migration("20180404201530_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.Customer", b =>
                {
                    b.Property<int>("CustomerId");

                    b.Property<string>("Address")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.Field", b =>
                {
                    b.Property<int>("FieldId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("DefaultValue")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<bool>("DobleCapture");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("FieldTypeId");

                    b.Property<int>("MaxLength");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Options")
                        .HasColumnType("varchar(250)")
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<int>("OrderInSection");

                    b.Property<int?>("ParentFieldId");

                    b.Property<bool>("Required");

                    b.Property<int>("SectionId");

                    b.Property<int>("Size");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Validation")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("FieldId");

                    b.HasIndex("ParentFieldId")
                        .HasName("IX_ParentFieldId");

                    b.HasIndex("SectionId")
                        .HasName("IX_SectionId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.FieldDataSource", b =>
                {
                    b.Property<int>("FieldDataSourceId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.HasKey("FieldDataSourceId");

                    b.ToTable("FieldDataSources");
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.FieldDataSourceDetail", b =>
                {
                    b.Property<int>("FieldDataSourceDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("FieldDataSourceId");

                    b.Property<string>("Label")
                        .HasColumnType("varchar(250)")
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Value")
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.HasKey("FieldDataSourceDetailId");

                    b.HasIndex("FieldDataSourceId")
                        .HasName("IX_FieldDataSourceId");

                    b.ToTable("FieldDataSourceDetails");
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("CustomerId");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("ProductState");

                    b.Property<short>("RequiredCaptures");

                    b.Property<int>("TemplateHeight");

                    b.Property<string>("TemplatePath")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.HasKey("ProductId");

                    b.HasIndex("CustomerId")
                        .HasName("IX_CustomerId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.ProductStateHistory", b =>
                {
                    b.Property<int>("ProductStateHistoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Observations")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<int>("ProductId");

                    b.Property<int>("TypingProcessId")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("TypingState");

                    b.HasKey("ProductStateHistoryId");

                    b.HasIndex("TypingProcessId", "ProductId")
                        .HasName("IX_TypingProcessId_ProductId");

                    b.ToTable("ProductStatusHistories");
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.Section", b =>
                {
                    b.Property<int>("SectionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsTable");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("NumberOfRows");

                    b.Property<int>("Position");

                    b.Property<int>("ProductId");

                    b.Property<string>("SectionName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("SectionId");

                    b.HasIndex("ProductId")
                        .HasName("IX_ProductId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.TypingProcesses", b =>
                {
                    b.Property<int>("TypingProcessId")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("ProductId");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Observations")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<int>("Priority");

                    b.Property<DateTime>("ProductionDate")
                        .HasColumnType("datetime");

                    b.Property<int>("TypingStatus");

                    b.HasKey("TypingProcessId", "ProductId");

                    b.HasAlternateKey("TypingProcessId");

                    b.HasIndex("ProductId")
                        .HasName("IX_ProductId");

                    b.ToTable("TypingProcesses");
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.Capture", b =>
                {
                    b.Property<long>("CaptureId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Completed");

                    b.Property<int>("CompletedSections");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("ProductId");

                    b.Property<int>("RegisterType");

                    b.Property<int>("TypingProcessId")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("CaptureId");

                    b.HasIndex("TypingProcessId", "ProductId")
                        .HasName("IX_TypingProcessId_ProductId");

                    b.ToTable("ProductDatas");
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.CaptureDetail", b =>
                {
                    b.Property<int>("FieldId");

                    b.Property<long>("CaptureId");

                    b.Property<string>("Value")
                        .HasColumnType("varchar(2000)")
                        .HasMaxLength(2000)
                        .IsUnicode(false);

                    b.HasKey("FieldId", "CaptureId");

                    b.HasIndex("CaptureId")
                        .HasName("IX_ProductDataId");

                    b.ToTable("ProductDataDetails");
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.CaptureHistory", b =>
                {
                    b.Property<long>("CaptureId");

                    b.Property<bool>("Completed");

                    b.Property<int>("CompletedSections");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("ProductId");

                    b.Property<int>("RegisterType");

                    b.Property<int>("TypingProcessId")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("CaptureId");

                    b.HasIndex("TypingProcessId", "ProductId")
                        .HasName("IX_TypingProcessId_ProductId");

                    b.ToTable("ProductDataHistories");
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.CaptureHistoryDetail", b =>
                {
                    b.Property<int>("FieldId");

                    b.Property<long>("CaptureId");

                    b.Property<string>("Value")
                        .HasColumnType("varchar(2000)")
                        .HasMaxLength(2000)
                        .IsUnicode(false);

                    b.HasKey("FieldId", "CaptureId");

                    b.HasIndex("CaptureId")
                        .HasName("IX_ProductDataId");

                    b.ToTable("ProductDataHistoryDetails");
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.UserInProduct", b =>
                {
                    b.Property<int>("UserFunction");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("ProductId");

                    b.HasKey("UserFunction", "UserName", "ProductId");

                    b.HasAlternateKey("ProductId", "UserFunction", "UserName");

                    b.HasIndex("ProductId")
                        .HasName("IX_ProductId");

                    b.ToTable("UserInProduct");
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.Field", b =>
                {
                    b.HasOne("Alcaze.IC.Typing.DTO.PersistenceEntities.Field", "ParentField")
                        .WithMany("InverseParentField")
                        .HasForeignKey("ParentFieldId")
                        .HasConstraintName("FK_dbo.Fields_dbo.Fields_ParentFieldId");

                    b.HasOne("Alcaze.IC.Typing.DTO.PersistenceEntities.Section", "Section")
                        .WithMany("Fields")
                        .HasForeignKey("SectionId")
                        .HasConstraintName("FK_dbo.Fields_dbo.Sections_SectionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.FieldDataSourceDetail", b =>
                {
                    b.HasOne("Alcaze.IC.Typing.DTO.PersistenceEntities.FieldDataSource", "FieldDataSource")
                        .WithMany("FieldDataSourceDetails")
                        .HasForeignKey("FieldDataSourceId")
                        .HasConstraintName("FK_dbo.FieldDataSourceDetails_dbo.FieldDataSources_FieldDataSourceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.Product", b =>
                {
                    b.HasOne("Alcaze.IC.Typing.DTO.PersistenceEntities.Customer", "Customer")
                        .WithMany("Products")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_dbo.Products_dbo.Customers_CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.ProductStateHistory", b =>
                {
                    b.HasOne("Alcaze.IC.Typing.DTO.PersistenceEntities.TypingProcesses", "TypingProcesses")
                        .WithMany("ProductStateHistories")
                        .HasForeignKey("TypingProcessId", "ProductId")
                        .HasConstraintName("FK_dbo.ProductStatusHistories_dbo.TypingProcesses_TypingProcessId_ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.Section", b =>
                {
                    b.HasOne("Alcaze.IC.Typing.DTO.PersistenceEntities.Product", "Product")
                        .WithMany("Sections")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_dbo.Sections_dbo.Products_ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.TypingProcesses", b =>
                {
                    b.HasOne("Alcaze.IC.Typing.DTO.PersistenceEntities.Product", "Product")
                        .WithMany("TypingProcesses")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_dbo.TypingProcesses_dbo.Products_ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.Capture", b =>
                {
                    b.HasOne("Alcaze.IC.Typing.DTO.PersistenceEntities.TypingProcesses", "TypingProcesses")
                        .WithMany("Captures")
                        .HasForeignKey("TypingProcessId", "ProductId")
                        .HasConstraintName("FK_dbo.ProductDatas_dbo.TypingProcesses_TypingProcessId_ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.CaptureDetail", b =>
                {
                    b.HasOne("Alcaze.IC.Typing.DTO.PersistenceEntities.Capture", "Capture")
                        .WithMany("CaptureDetails")
                        .HasForeignKey("CaptureId")
                        .HasConstraintName("FK_dbo.ProductDataDetails_dbo.ProductDatas_ProductDataId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.CaptureHistory", b =>
                {
                    b.HasOne("Alcaze.IC.Typing.DTO.PersistenceEntities.TypingProcesses", "TypingProcesses")
                        .WithMany("CaptureHistories")
                        .HasForeignKey("TypingProcessId", "ProductId")
                        .HasConstraintName("FK_dbo.ProductDataHistories_dbo.TypingProcesses_TypingProcessId_ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.CaptureHistoryDetail", b =>
                {
                    b.HasOne("Alcaze.IC.Typing.DTO.PersistenceEntities.CaptureHistory", "CaptureHistory")
                        .WithMany("CaptureHistoryDetails")
                        .HasForeignKey("CaptureId")
                        .HasConstraintName("FK_dbo.ProductDataHistoryDetails_dbo.ProductDataHistories_ProductDataId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Alcaze.IC.Typing.DTO.PersistenceEntities.UserInProduct", b =>
                {
                    b.HasOne("Alcaze.IC.Typing.DTO.PersistenceEntities.Product", "Product")
                        .WithMany("UserInProducts")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_dbo.UserInProduct_dbo.Products_ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
