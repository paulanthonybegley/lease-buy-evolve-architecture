using System.Data;

namespace EvolutionaryArchitecture.LeaseBuyArch.Comparison.DataAccess;

internal interface IDatabaseConnectionFactory
{
    IDbConnection Create();
}
