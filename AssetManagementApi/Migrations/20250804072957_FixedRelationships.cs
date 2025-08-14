using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class FixedRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetRelationships_Assets_AssociatedTagNumber",
                table: "AssetRelationships");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetRelationships_Assets_AssociatedTagNumber",
                table: "AssetRelationships",
                column: "AssociatedTagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetRelationships_Assets_AssociatedTagNumber",
                table: "AssetRelationships");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetRelationships_Assets_AssociatedTagNumber",
                table: "AssetRelationships",
                column: "AssociatedTagNumber",
                principalTable: "Assets",
                principalColumn: "TagNumber");
        }
    }
}
