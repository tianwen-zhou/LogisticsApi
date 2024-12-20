using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsApi.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToWayBills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Waybills",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Waybills");
        }
    }
}
