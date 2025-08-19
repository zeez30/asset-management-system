using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class AllModelsReady : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetRelationships_Assets_AssociatedTagNumber",
                table: "AssetRelationships");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetRelationships_Assets_ParentTagNumber",
                table: "AssetRelationships");

            migrationBuilder.DropIndex(
                name: "IX_AssetRelationships_AssociatedTagNumber",
                table: "AssetRelationships");

            migrationBuilder.DropIndex(
                name: "IX_AssetRelationships_ParentTagNumber",
                table: "AssetRelationships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Asset3DModels",
                table: "Asset3DModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Asset2DModels",
                table: "Asset2DModels");

            migrationBuilder.DropColumn(
                name: "CMIMSRequired",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "DeckPlatformCode",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "FacilitySector",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "FoundInADL",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "FoundInADiagrams",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "FoundInAEngineering",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "FoundInAVEVAE3D",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "FoundInAVEVAElectricalAndInstrumentation",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "FoundInEDMS",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "FoundInPiVision",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "LastMaintenanceDate",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ServiceDescription",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ParentTagNumber",
                table: "AssetRelationships");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "AssetDocuments");

            migrationBuilder.RenameColumn(
                name: "NextMaintenanceDate",
                table: "Assets",
                newName: "LastMaintenance");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "AssetDocuments",
                newName: "Title");

            migrationBuilder.AlterColumn<string>(
                name: "ValidationStatus",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TagFormatID",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "System",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Subsystem",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Site",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FunctionalClassID",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssetType",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssetName",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "AreaCode",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TagNumber",
                table: "Assets",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "CMMMSRequired",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeckPlatform",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacilitySection",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssociatedTagNumber",
                table: "AssetRelationships",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryTagNumber",
                table: "AssetRelationships",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TagNumber",
                table: "AssetDocuments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "AssetTagNumber",
                table: "AssetDocuments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TagNumber",
                table: "Asset3DModels",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Asset3DModels",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "TagNumber",
                table: "Asset2DModels",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Asset2DModels",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asset3DModels",
                table: "Asset3DModels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asset2DModels",
                table: "Asset2DModels",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AssetRelationships_PrimaryTagNumber",
                table: "AssetRelationships",
                column: "PrimaryTagNumber");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDocuments_AssetTagNumber",
                table: "AssetDocuments",
                column: "AssetTagNumber");

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
                principalColumn: "TagNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset3DModels_Assets_TagNumber",
                table: "Asset3DModels",
                column: "TagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetDocuments_Assets_AssetTagNumber",
                table: "AssetDocuments",
                column: "AssetTagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetRelationships_Assets_PrimaryTagNumber",
                table: "AssetRelationships",
                column: "PrimaryTagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset2DModels_Assets_TagNumber",
                table: "Asset2DModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset3DModels_Assets_TagNumber",
                table: "Asset3DModels");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetDocuments_Assets_AssetTagNumber",
                table: "AssetDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetRelationships_Assets_PrimaryTagNumber",
                table: "AssetRelationships");

            migrationBuilder.DropIndex(
                name: "IX_AssetRelationships_PrimaryTagNumber",
                table: "AssetRelationships");

            migrationBuilder.DropIndex(
                name: "IX_AssetDocuments_AssetTagNumber",
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
                name: "CMMMSRequired",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "DeckPlatform",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "FacilitySection",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "PrimaryTagNumber",
                table: "AssetRelationships");

            migrationBuilder.DropColumn(
                name: "AssetTagNumber",
                table: "AssetDocuments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Asset3DModels");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Asset2DModels");

            migrationBuilder.RenameColumn(
                name: "LastMaintenance",
                table: "Assets",
                newName: "NextMaintenanceDate");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "AssetDocuments",
                newName: "FileName");

            migrationBuilder.AlterColumn<string>(
                name: "ValidationStatus",
                table: "Assets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TagFormatID",
                table: "Assets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "System",
                table: "Assets",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Subsystem",
                table: "Assets",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "Assets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Site",
                table: "Assets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Assets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Assets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "Assets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FunctionalClassID",
                table: "Assets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Assets",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssetType",
                table: "Assets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssetName",
                table: "Assets",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AreaCode",
                table: "Assets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TagNumber",
                table: "Assets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<bool>(
                name: "CMIMSRequired",
                table: "Assets",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentStatus",
                table: "Assets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeckPlatformCode",
                table: "Assets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacilitySector",
                table: "Assets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FoundInADL",
                table: "Assets",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FoundInADiagrams",
                table: "Assets",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FoundInAEngineering",
                table: "Assets",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FoundInAVEVAE3D",
                table: "Assets",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FoundInAVEVAElectricalAndInstrumentation",
                table: "Assets",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FoundInEDMS",
                table: "Assets",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FoundInPiVision",
                table: "Assets",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMaintenanceDate",
                table: "Assets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceDescription",
                table: "Assets",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssociatedTagNumber",
                table: "AssetRelationships",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentTagNumber",
                table: "AssetRelationships",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "TagNumber",
                table: "AssetDocuments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "DocumentType",
                table: "AssetDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TagNumber",
                table: "Asset3DModels",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TagNumber",
                table: "Asset2DModels",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asset3DModels",
                table: "Asset3DModels",
                column: "TagNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asset2DModels",
                table: "Asset2DModels",
                column: "TagNumber");

            migrationBuilder.CreateIndex(
                name: "IX_AssetRelationships_AssociatedTagNumber",
                table: "AssetRelationships",
                column: "AssociatedTagNumber");

            migrationBuilder.CreateIndex(
                name: "IX_AssetRelationships_ParentTagNumber",
                table: "AssetRelationships",
                column: "ParentTagNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetRelationships_Assets_AssociatedTagNumber",
                table: "AssetRelationships",
                column: "AssociatedTagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetRelationships_Assets_ParentTagNumber",
                table: "AssetRelationships",
                column: "ParentTagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber");
        }
    }
}
