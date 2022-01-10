namespace Common.Caching.SQLLite
{
    using Microsoft.Data.Sqlite;

    /// <summary>
    /// SQLite database provider.
    /// </summary>
    public interface ISQLLiteDatabaseProvider
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns>The connection.</returns>
        SqliteConnection GetConnection();

        string DBProviderName { get; }
    }
}
