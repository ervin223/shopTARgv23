using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopTARgv23.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRealEstateIdToFileToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RealEstateId",
                table: "FileToDatabases",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealEstateId",
                table: "FileToDatabases");
        }
    }
}