#nullable disable

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EvolutionaryArchitecture.LeaseBuyArch.Purchasing.Data.Database.Migrations;

[ExcludeFromCodeCoverage]
public partial class Create_Purchases_Table : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(name: "Purchasing");

        migrationBuilder.CreateTable(
            name: "Purchases",
            schema: "Purchasing",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                VehicleMsrp = table.Column<decimal>(type: "numeric", nullable: false),
                DownPayment = table.Column<decimal>(type: "numeric", nullable: false),
                Apr = table.Column<decimal>(type: "numeric", nullable: false),
                TermMonths = table.Column<int>(type: "integer", nullable: false),
                MonthlyPayment = table.Column<decimal>(type: "numeric", nullable: false),
                PreparedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                CompletedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_Purchases", x => x.Id));
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Purchases", schema: "Purchasing");
    }
}
