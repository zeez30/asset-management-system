using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRequiredFieldsOnModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset3DModels_Assets_TagNumber",
                table: "Asset3DModels");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Asset3DModels");

            migrationBuilder.DropColumn(
                name: "ModelType",
                table: "Asset3DModels");

            migrationBuilder.RenameColumn(
                name: "TagNumber",
                table: "Asset3DModels",
                newName: "AssetTagNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Asset3DModels_TagNumber",
                table: "Asset3DModels",
                newName: "IX_Asset3DModels_AssetTagNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset3DModels_Assets_AssetTagNumber",
                table: "Asset3DModels",
                column: "AssetTagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset3DModels_Assets_AssetTagNumber",
                table: "Asset3DModels");

            migrationBuilder.RenameColumn(
                name: "AssetTagNumber",
                table: "Asset3DModels",
                newName: "TagNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Asset3DModels_AssetTagNumber",
                table: "Asset3DModels",
                newName: "IX_Asset3DModels_TagNumber");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Asset3DModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelType",
                table: "Asset3DModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset3DModels_Assets_TagNumber",
                table: "Asset3DModels",
                column: "TagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber");
        }
    }
}
