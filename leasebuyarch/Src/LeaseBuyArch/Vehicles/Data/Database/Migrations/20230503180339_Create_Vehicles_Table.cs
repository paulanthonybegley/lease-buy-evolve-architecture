#nullable disable

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Data.Database.Migrations;

[ExcludeFromCodeCoverage]
public partial class Create_Vehicles_Table : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(name: "Vehicles");

        migrationBuilder.CreateTable(
            name: "Vehicles",
            schema: "Vehicles",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Make = table.Column<string>(type: "text", nullable: false),
                Model = table.Column<string>(type: "text", nullable: false),
                Year = table.Column<int>(type: "integer", nullable: false),
                Msrp = table.Column<decimal>(type: "numeric", nullable: false),
                ResidualPercentageAt36Months = table.Column<decimal>(type: "numeric", nullable: false),
                Status = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Vehicles", x => x.Id));
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Vehicles", schema: "Vehicles");
    }
}
