namespace EvolutionaryArchitecture.LeaseBuyArch.Vehicles.Data;

internal sealed class Vehicle
{
    public Guid Id { get; init; }
    public string Make { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public int Year { get; init; }
    public decimal Msrp { get; init; }
    public decimal ResidualPercentageAt36Months { get; init; }
    public string Status { get; private set; } = "Available";

    private Vehicle(Guid id, string make, string model, int year, decimal msrp,
        decimal residualPercentageAt36Months, string status)
    {
        Id = id;
        Make = make;
        Model = model;
        Year = year;
        Msrp = msrp;
        ResidualPercentageAt36Months = residualPercentageAt36Months;
        Status = status;
    }

    internal static Vehicle Register(string make, string model, int year, decimal msrp,
        decimal residualPercentageAt36Months) =>
        new(Guid.NewGuid(), make, model, year, msrp, residualPercentageAt36Months, "Available");

    internal void MarkAsLeased() => Status = "Leased";

    internal void MarkAsOwned() => Status = "Owned";
}
