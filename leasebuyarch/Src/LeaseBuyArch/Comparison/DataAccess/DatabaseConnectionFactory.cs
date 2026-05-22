using System.Data;
using Npgsql;

namespace EvolutionaryArchitecture.LeaseBuyArch.Comparison.DataAccess;

internal sealed class DatabaseConnectionFactory : IDatabaseConnectionFactory
{
    private readonly string _connectionString;

    public DatabaseConnectionFactory(string connectionString) => _connectionString = connectionString;

    public IDbConnection Create() => new NpgsqlConnection(_connectionString);
}
