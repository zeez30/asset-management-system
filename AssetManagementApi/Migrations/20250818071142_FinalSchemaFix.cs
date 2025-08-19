using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class FinalSchemaFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop ALL foreign keys and the primary key that depend on 'TagNumber'
            migrationBuilder.DropForeignKey(
                name: "FK_AssetDocuments_Assets_AssetTagNumber",
                table: "AssetDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetRelationships_Assets_PrimaryTagNumber",
                table: "AssetRelationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset2DModels_Assets_TagNumber",
                table: "Asset2DModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset3DModels_Assets_TagNumber",
                table: "Asset3DModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assets",
                table: "Assets");

            // 2. Drop the column from Asset2DModels and other tables
            migrationBuilder.DropIndex(
                name: "IX_Asset2DModels_TagNumber",
                table: "Asset2DModels");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "DrawingType",
                table: "Asset2DModels");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Asset2DModels");

            migrationBuilder.DropColumn(
                name: "TagNumber",
                table: "Asset2DModels");

            // 3. Alter the 'TagNumber' column and other properties
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
                name: "Status",
                table: "Assets",
                type: "nvarchar(50)",
                maxLength: 50,
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
                name: "FacilitySection",
                table: "Assets",
                type: "nvarchar(50)",
                maxLength: 50,
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
                name: "DeckPlatform",
                table: "Assets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "CMMMSRequired",
                table: "Assets",
                type: "bit",
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

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryTagNumber",
                table: "AssetRelationships",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssetTagNumber",
                table: "AssetRelationships",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssetTagNumber",
                table: "AssetDocuments",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TagNumber",
                table: "Asset3DModels",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssetTagNumber",
                table: "Asset2DModels",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssetTagNumber1",
                table: "Asset2DModels",
                type: "nvarchar(50)",
                nullable: true);

            // 4. Re-add the primary key and all foreign keys
            migrationBuilder.AddPrimaryKey(
                name: "PK_Assets",
                table: "Assets",
                column: "TagNumber");

            migrationBuilder.CreateIndex(
                name: "IX_AssetRelationships_AssetTagNumber",
                table: "AssetRelationships",
                column: "AssetTagNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Asset2DModels_AssetTagNumber",
                table: "Asset2DModels",
                column: "AssetTagNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Asset2DModels_AssetTagNumber1",
                table: "Asset2DModels",
                column: "AssetTagNumber1");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AssetRelationships_Assets_AssetTagNumber",
                table: "AssetRelationships",
                column: "AssetTagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset3DModels_Assets_TagNumber",
                table: "Asset3DModels",
                column: "TagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset2DModels_Assets_AssetTagNumber",
                table: "Asset2DModels",
                column: "AssetTagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber");
            
            migrationBuilder.AddForeignKey(
                name: "FK_Asset2DModels_Assets_AssetTagNumber1",
                table: "Asset2DModels",
                column: "AssetTagNumber1",
                principalTable: "Assets",
                principalColumn: "TagNumber");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1. Drop all the foreign keys that were added in the Up method
            migrationBuilder.DropForeignKey(
                name: "FK_AssetDocuments_Assets_AssetTagNumber",
                table: "AssetDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetRelationships_Assets_PrimaryTagNumber",
                table: "AssetRelationships");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetRelationships_Assets_AssetTagNumber",
                table: "AssetRelationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset3DModels_Assets_TagNumber",
                table: "Asset3DModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset2DModels_Assets_AssetTagNumber",
                table: "Asset2DModels");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset2DModels_Assets_AssetTagNumber1",
                table: "Asset2DModels");

            // 2. Drop the indexes and primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_Assets",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_AssetRelationships_AssetTagNumber",
                table: "AssetRelationships");

            migrationBuilder.DropIndex(
                name: "IX_Asset2DModels_AssetTagNumber",
                table: "Asset2DModels");

            migrationBuilder.DropIndex(
                name: "IX_Asset2DModels_AssetTagNumber1",
                table: "Asset2DModels");

            // 3. Drop the new columns
            migrationBuilder.DropColumn(
                name: "AssetTagNumber",
                table: "AssetRelationships");

            migrationBuilder.DropColumn(
                name: "AssetTagNumber",
                table: "Asset2DModels");

            migrationBuilder.DropColumn(
                name: "AssetTagNumber1",
                table: "Asset2DModels");

            // 4. Alter the columns back to their original state
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
                name: "Status",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
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
                name: "FacilitySection",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
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
                name: "DeckPlatform",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CMMMSRequired",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
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

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Assets",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryTagNumber",
                table: "AssetRelationships",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssetTagNumber",
                table: "AssetDocuments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TagNumber",
                table: "Asset3DModels",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            // 5. Re-create the columns that were dropped
            migrationBuilder.AddColumn<string>(
                name: "DrawingType",
                table: "Asset2DModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Asset2DModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TagNumber",
                table: "Asset2DModels",
                type: "nvarchar(450)",
                nullable: true);

            // 6. Re-create the original indexes and foreign keys
            migrationBuilder.AddPrimaryKey(
                name: "PK_Assets",
                table: "Assets",
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
                principalColumn: "TagNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetRelationships_Assets_PrimaryTagNumber",
                table: "AssetRelationships",
                column: "PrimaryTagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber",
                onDelete: ReferentialAction.Cascade);

        }
    }
}