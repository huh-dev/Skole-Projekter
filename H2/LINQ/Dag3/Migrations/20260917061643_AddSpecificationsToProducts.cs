using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dag3.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecificationsToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<JsonDocument>(
                name: "specifications",
                table: "Products",
                type: "json",
                nullable: true,
                defaultValue: "{}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "units_sold",
                table: "Products");
        }
    }
}
