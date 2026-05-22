#nullable disable

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EvolutionaryArchitecture.LeaseBuyArch.Leasing.Data.Database.Migrations;

[ExcludeFromCodeCoverage]
public partial class Create_Leases_Table : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(name: "Leasing");

        migrationBuilder.CreateTable(
            name: "Leases",
            schema: "Leasing",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                VehicleMsrp = table.Column<decimal>(type: "numeric", nullable: false),
                ResidualPercentage = table.Column<decimal>(type: "numeric", nullable: false),
                MoneyFactor = table.Column<decimal>(type: "numeric", nullable: false),
                TermMonths = table.Column<int>(type: "integer", nullable: false),
                AnnualMileageLimit = table.Column<int>(type: "integer", nullable: false),
                MonthlyPayment = table.Column<decimal>(type: "numeric", nullable: false),
                PreparedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                SignedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_Leases", x => x.Id));
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Leases", schema: "Leasing");
    }
}
