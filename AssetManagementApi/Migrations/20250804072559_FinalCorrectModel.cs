using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class FinalCorrectModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset2DModels_Assets_TagNumber",
                table: "Asset2DModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset3DModels_Assets_TagNumber",
                table: "Asset3DModels");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetDocuments_Assets_TagNumber",
                table: "AssetDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetDocuments",
                table: "AssetDocuments");

            migrationBuilder.DropIndex(
                name: "IX_AssetDocuments_TagNumber",
                table: "AssetDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Asset3DModels",
                table: "Asset3DModels");

            migrationBuilder.DropIndex(
                name: "IX_Asset3DModels_TagNumber",
                table: "Asset3DModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Asset2DModels",
                table: "Asset2DModels");

            migrationBuilder.DropIndex(
                name: "IX_Asset2DModels_TagNumber",
                table: "Asset2DModels");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "AssetDocuments");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "AssetDocuments");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "AssetDocuments");

            migrationBuilder.DropColumn(
                name: "Model3DID",
                table: "Asset3DModels");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Asset3DModels");

            migrationBuilder.DropColumn(
                name: "FileSizeKB",
                table: "Asset3DModels");

            migrationBuilder.DropColumn(
                name: "ModelFormat",
                table: "Asset3DModels");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "Asset3DModels");

            migrationBuilder.DropColumn(
                name: "UploadedBy",
                table: "Asset3DModels");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Asset3DModels");

            migrationBuilder.DropColumn(
                name: "Model2DID",
                table: "Asset2DModels");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Asset2DModels");

            migrationBuilder.DropColumn(
                name: "FileSizeKB",
                table: "Asset2DModels");

            migrationBuilder.DropColumn(
                name: "ModelFormat",
                table: "Asset2DModels");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "Asset2DModels");

            migrationBuilder.DropColumn(
                name: "UploadedBy",
                table: "Asset2DModels");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Asset2DModels");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "AssetDocuments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "AssetDocuments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "DocumentType",
                table: "AssetDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Asset3DModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Asset3DModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "ModelType",
                table: "Asset3DModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Asset2DModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Asset2DModels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "DrawingType",
                table: "Asset2DModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetDocuments",
                table: "AssetDocuments",
                column: "TagNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asset3DModels",
                table: "Asset3DModels",
                column: "TagNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asset2DModels",
                table: "Asset2DModels",
                column: "TagNumber");

            migrationBuilder.CreateTable(
                name: "AssetRelationships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentTagNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssociatedTagNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RelationshipType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetRelationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetRelationships_Assets_AssociatedTagNumber",
                        column: x => x.AssociatedTagNumber,
                        principalTable: "Assets",
                        principalColumn: "TagNumber");
                    table.ForeignKey(
                        name: "FK_AssetRelationships_Assets_ParentTagNumber",
                        column: x => x.ParentTagNumber,
                        principalTable: "Assets",
                        principalColumn: "TagNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetRelationships_AssociatedTagNumber",
                table: "AssetRelationships",
                column: "AssociatedTagNumber");

            migrationBuilder.CreateIndex(
                name: "IX_AssetRelationships_ParentTagNumber",
                table: "AssetRelationships",
                column: "ParentTagNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetRelationships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetDocuments",
                table: "AssetDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Asset3DModels",
                table: "Asset3DModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Asset2DModels",
                table: "Asset2DModels");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "AssetDocuments");

            migrationBuilder.DropColumn(
                name: "ModelType",
                table: "Asset3DModels");

            migrationBuilder.DropColumn(
                name: "DrawingType",
                table: "Asset2DModels");

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "AssetDocuments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "AssetDocuments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentId",
                table: "AssetDocuments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "AssetDocuments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "AssetDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Asset3DModels",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Asset3DModels",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Model3DID",
                table: "Asset3DModels",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Asset3DModels",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FileSizeKB",
                table: "Asset3DModels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelFormat",
                table: "Asset3DModels",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "Asset3DModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UploadedBy",
                table: "Asset3DModels",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Asset3DModels",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Asset2DModels",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Asset2DModels",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Model2DID",
                table: "Asset2DModels",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Asset2DModels",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FileSizeKB",
                table: "Asset2DModels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelFormat",
                table: "Asset2DModels",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "Asset2DModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UploadedBy",
                table: "Asset2DModels",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Asset2DModels",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetDocuments",
                table: "AssetDocuments",
                column: "DocumentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asset3DModels",
                table: "Asset3DModels",
                column: "Model3DID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asset2DModels",
                table: "Asset2DModels",
                column: "Model2DID");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDocuments_TagNumber",
                table: "AssetDocuments",
                column: "TagNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Asset3DModels_TagNumber",
                table: "Asset3DModels",
                column: "TagNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Asset2DModels_TagNumber",
                table: "Asset2DModels",
                column: "TagNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset2DModels_Assets_TagNumber",
                table: "Asset2DModels",
                column: "TagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset3DModels_Assets_TagNumber",
                table: "Asset3DModels",
                column: "TagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetDocuments_Assets_TagNumber",
                table: "AssetDocuments",
                column: "TagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
