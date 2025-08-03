using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    TagNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssetName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AssetType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Size = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    InstallationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AreaCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeckPlatformCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FacilitySector = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ServiceDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Site = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Subsystem = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    System = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FunctionalClassID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TagFormatID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ValidationStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CMIMSRequired = table.Column<bool>(type: "bit", nullable: true),
                    FoundInADiagrams = table.Column<bool>(type: "bit", nullable: true),
                    FoundInADL = table.Column<bool>(type: "bit", nullable: true),
                    FoundInAEngineering = table.Column<bool>(type: "bit", nullable: true),
                    FoundInAVEVAE3D = table.Column<bool>(type: "bit", nullable: true),
                    FoundInAVEVAElectricalAndInstrumentation = table.Column<bool>(type: "bit", nullable: true),
                    FoundInEDMS = table.Column<bool>(type: "bit", nullable: true),
                    FoundInPiVision = table.Column<bool>(type: "bit", nullable: true),
                    LastMaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextMaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.TagNumber);
                });

            migrationBuilder.CreateTable(
                name: "Asset2DModels",
                columns: table => new
                {
                    Model2DID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModelFormat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FileSizeKB = table.Column<int>(type: "int", nullable: true),
                    UploadedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset2DModels", x => x.Model2DID);
                    table.ForeignKey(
                        name: "FK_Asset2DModels_Assets_TagNumber",
                        column: x => x.TagNumber,
                        principalTable: "Assets",
                        principalColumn: "TagNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asset3DModels",
                columns: table => new
                {
                    Model3DID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ModelFormat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FileSizeKB = table.Column<int>(type: "int", nullable: true),
                    UploadedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset3DModels", x => x.Model3DID);
                    table.ForeignKey(
                        name: "FK_Asset3DModels_Assets_TagNumber",
                        column: x => x.TagNumber,
                        principalTable: "Assets",
                        principalColumn: "TagNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetDocuments",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetDocuments", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_AssetDocuments_Assets_TagNumber",
                        column: x => x.TagNumber,
                        principalTable: "Assets",
                        principalColumn: "TagNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asset2DModels_TagNumber",
                table: "Asset2DModels",
                column: "TagNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Asset3DModels_TagNumber",
                table: "Asset3DModels",
                column: "TagNumber");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDocuments_TagNumber",
                table: "AssetDocuments",
                column: "TagNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asset2DModels");

            migrationBuilder.DropTable(
                name: "Asset3DModels");

            migrationBuilder.DropTable(
                name: "AssetDocuments");

            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
