using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISample.Migrations
{
    /// <inheritdoc />
    public partial class AddDescripinToPointOInerest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PointOfIntrests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PointOfIntrests");
        }
    }
}
