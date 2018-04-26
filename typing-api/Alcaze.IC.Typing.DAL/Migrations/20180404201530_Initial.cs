using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Alcaze.IC.Typing.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    ModifiedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "FieldDataSources",
                columns: table => new
                {
                    FieldDataSourceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    ModifiedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldDataSources", x => x.FieldDataSourceId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    ModifiedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ProductState = table.Column<int>(nullable: false),
                    RequiredCaptures = table.Column<short>(nullable: false),
                    TemplateHeight = table.Column<int>(nullable: false),
                    TemplatePath = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_dbo.Products_dbo.Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldDataSourceDetails",
                columns: table => new
                {
                    FieldDataSourceDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    FieldDataSourceId = table.Column<int>(nullable: false),
                    Label = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Value = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldDataSourceDetails", x => x.FieldDataSourceDetailId);
                    table.ForeignKey(
                        name: "FK_dbo.FieldDataSourceDetails_dbo.FieldDataSources_FieldDataSourceId",
                        column: x => x.FieldDataSourceId,
                        principalTable: "FieldDataSources",
                        principalColumn: "FieldDataSourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    SectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsTable = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    NumberOfRows = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    SectionName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.SectionId);
                    table.ForeignKey(
                        name: "FK_dbo.Sections_dbo.Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypingProcesses",
                columns: table => new
                {
                    TypingProcessId = table.Column<int>(unicode: false, maxLength: 50, nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    FileName = table.Column<string>(type: "varchar(50)", nullable: false),
                    ModifiedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Observations = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TypingState = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypingProcesses", x => new { x.TypingProcessId, x.ProductId });
                    table.UniqueConstraint("AK_TypingProcesses_TypingProcessId", x => x.TypingProcessId);
                    table.ForeignKey(
                        name: "FK_dbo.TypingProcesses_dbo.Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInProduct",
                columns: table => new
                {
                    UserFunction = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 50, nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInProduct", x => new { x.UserFunction, x.UserName, x.ProductId });
                    table.UniqueConstraint("AK_UserInProduct_ProductId_UserFunction_UserName", x => new { x.ProductId, x.UserFunction, x.UserName });
                    table.ForeignKey(
                        name: "FK_dbo.UserInProduct_dbo.Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    FieldId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    DefaultValue = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DobleCapture = table.Column<bool>(nullable: false),
                    FieldName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    FieldTypeId = table.Column<int>(nullable: false),
                    MaxLength = table.Column<int>(nullable: false),
                    ModifiedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Options = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    OrderInSection = table.Column<int>(nullable: false),
                    ParentFieldId = table.Column<int>(nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    SectionId = table.Column<int>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    Title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Validation = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.FieldId);
                    table.ForeignKey(
                        name: "FK_dbo.Fields_dbo.Fields_ParentFieldId",
                        column: x => x.ParentFieldId,
                        principalTable: "Fields",
                        principalColumn: "FieldId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dbo.Fields_dbo.Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaptureHistory",
                columns: table => new
                {
                    CaptureId = table.Column<long>(nullable: false),
                    Completed = table.Column<bool>(nullable: false),
                    CompletedSections = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    RegisterType = table.Column<int>(nullable: false),
                    TypingProcessId = table.Column<int>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaptureHistory", x => x.CaptureId);
                    table.ForeignKey(
                        name: "FK_dbo.CaptureHistory_dbo.TypingProcesses_TypingProcessId_ProductId",
                        columns: x => new { x.TypingProcessId, x.ProductId },
                        principalTable: "TypingProcesses",
                        principalColumns: new[] { "TypingProcessId", "ProductId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Captures",
                columns: table => new
                {
                    CaptureId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Completed = table.Column<bool>(nullable: false),
                    CompletedSections = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    RegisterType = table.Column<int>(nullable: false),
                    TypingProcessId = table.Column<int>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Captures", x => x.CaptureId);
                    table.ForeignKey(
                        name: "FK_dbo.Captures_dbo.TypingProcesses_TypingProcessId_ProductId",
                        columns: x => new { x.TypingProcessId, x.ProductId },
                        principalTable: "TypingProcesses",
                        principalColumns: new[] { "TypingProcessId", "ProductId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductStateHistory",
                columns: table => new
                {
                    ProductStateHistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Observations = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    TypingProcessId = table.Column<int>(unicode: false, maxLength: 50, nullable: false),
                    TypingState = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStateHistory", x => x.ProductStateHistoryId);
                    table.ForeignKey(
                        name: "FK_dbo.ProductStateHistory_dbo.TypingProcesses_TypingProcessId_ProductId",
                        columns: x => new { x.TypingProcessId, x.ProductId },
                        principalTable: "TypingProcesses",
                        principalColumns: new[] { "TypingProcessId", "ProductId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaptureHistoryDetails",
                columns: table => new
                {
                    FieldId = table.Column<int>(nullable: false),
                    CaptureId = table.Column<long>(nullable: false),
                    Value = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaptureHistoryDetails", x => new { x.FieldId, x.CaptureId });
                    table.ForeignKey(
                        name: "FK_dbo.CaptureHistoryDetails_dbo.CaptureHistory_CaptureId",
                        column: x => x.CaptureId,
                        principalTable: "CaptureHistory",
                        principalColumn: "CaptureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaptureDetails",
                columns: table => new
                {
                    FieldId = table.Column<int>(nullable: false),
                    CaptureId = table.Column<long>(nullable: false),
                    Value = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaptureDetails", x => new { x.FieldId, x.CaptureId });
                    table.ForeignKey(
                        name: "FK_dbo.CaptureDetails_dbo.Captures_CaptureId",
                        column: x => x.CaptureId,
                        principalTable: "Captures",
                        principalColumn: "CaptureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FieldDataSourceId",
                table: "FieldDataSourceDetails",
                column: "FieldDataSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentFieldId",
                table: "Fields",
                column: "ParentFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionId",
                table: "Fields",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CaptureId",
                table: "CaptureDetails",
                column: "CaptureId");

            migrationBuilder.CreateIndex(
                name: "IX_TypingProcessId_ProductId",
                table: "CaptureHistory",
                columns: new[] { "TypingProcessId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_CaptureId",
                table: "CaptureHistoryDetails",
                column: "CaptureId");

            migrationBuilder.CreateIndex(
                name: "IX_TypingProcessId_ProductId",
                table: "Captures",
                columns: new[] { "TypingProcessId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerId",
                table: "Products",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TypingProcessId_ProductId",
                table: "ProductStateHistory",
                columns: new[] { "TypingProcessId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductId",
                table: "Sections",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductId",
                table: "TypingProcesses",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductId",
                table: "UserInProduct",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldDataSourceDetails");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "CaptureDetails");

            migrationBuilder.DropTable(
                name: "CaptureHistoryDetails");

            migrationBuilder.DropTable(
                name: "ProductStateHistory");

            migrationBuilder.DropTable(
                name: "UserInProduct");

            migrationBuilder.DropTable(
                name: "FieldDataSources");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Captures");

            migrationBuilder.DropTable(
                name: "CaptureHistory");

            migrationBuilder.DropTable(
                name: "TypingProcesses");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
