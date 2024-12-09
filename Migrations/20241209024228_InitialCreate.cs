using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskNumber = table.Column<string>(type: "TEXT", nullable: false),
                    TaskType = table.Column<string>(type: "TEXT", nullable: false),
                    AssignedTo = table.Column<string>(type: "TEXT", nullable: false),
                    TaskStatus = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: false),
                    LicensePhoto = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostalAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Suburb = table.Column<string>(type: "TEXT", nullable: false),
                    PostCode = table.Column<string>(type: "TEXT", nullable: false),
                    RouteCode = table.Column<string>(type: "TEXT", nullable: false),
                    Zone = table.Column<string>(type: "TEXT", nullable: false),
                    RemoteArea = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeliveryServiceProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ActivationPlan = table.Column<string>(type: "TEXT", nullable: false),
                    LMShippingStation = table.Column<string>(type: "TEXT", nullable: false),
                    LMDeliveryStation = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoutePlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlanName = table.Column<string>(type: "TEXT", nullable: false),
                    DriverId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutePlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskWaybills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskNumber = table.Column<string>(type: "TEXT", nullable: false),
                    WaybillNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskWaybills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Waybills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WaybillNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Sender = table.Column<string>(type: "TEXT", nullable: false),
                    SenderAddress = table.Column<string>(type: "TEXT", nullable: false),
                    SenderPhone = table.Column<string>(type: "TEXT", nullable: false),
                    Recipient = table.Column<string>(type: "TEXT", nullable: false),
                    RecipientAddress = table.Column<string>(type: "TEXT", nullable: false),
                    RecipientPhone = table.Column<string>(type: "TEXT", nullable: false),
                    PostCode = table.Column<string>(type: "TEXT", nullable: false),
                    RouteCode = table.Column<string>(type: "TEXT", nullable: false),
                    ParcelValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    DeliveryTime = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waybills", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryTasks");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "PostalAreas");

            migrationBuilder.DropTable(
                name: "RoutePlans");

            migrationBuilder.DropTable(
                name: "TaskWaybills");

            migrationBuilder.DropTable(
                name: "Waybills");
        }
    }
}
