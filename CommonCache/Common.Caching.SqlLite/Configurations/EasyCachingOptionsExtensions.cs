namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using Common.Caching.Core;
    using Common.Caching.Core.Configurations;
    using Common.Caching.SQLLite;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// EasyCaching options extensions.
    /// </summary>
    public static class EasyCachingOptionsExtensions
    {
        /// <summary>
        /// Uses the SQLite provider (specify the config via hard code).
        /// </summary>
        /// <param name="options">Options.</param>
        /// <param name="configure">Configure provider settings.</param>
        /// <param name="name">The name of this provider instance.</param>
        public static CachingServiceOptions UseSQLite(
            this CachingServiceOptions options
            , Action<SQLLiteOptions> configure
            , string name = EasyCachingConstValue.DefaultSQLiteName
            )
        {
            ArgumentCheck.NotNull(configure, nameof(configure));

            options.RegisterExtension(new SQLLiteOptionsExtension(name, configure));
            return options;
        }

        /// <summary>
        /// Uses the SQLite provider (read config from configuration file).
        /// </summary>
        /// <param name="options">Options.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="name">The name of this provider instance.</param>
        /// <param name="sectionName">The section name in the configuration file.</param>
        public static CachingServiceOptions UseSQLite(
            this CachingServiceOptions options
            , IConfiguration configuration
            , string name = EasyCachingConstValue.DefaultSQLiteName
            , string sectionName = EasyCachingConstValue.SQLiteSection
            )
        {
            var dbConfig = configuration.GetSection(sectionName);
            var sqliteOptions = new SQLLiteOptions();
            dbConfig.Bind(sqliteOptions);

            void configure(SQLLiteOptions x)
            {
                x.EnableLogging = sqliteOptions.EnableLogging;
                x.MaxRdSecond = sqliteOptions.MaxRdSecond;             
                x.LockMs = sqliteOptions.LockMs;
                x.SleepMs = sqliteOptions.SleepMs;
                x.SerializerName = sqliteOptions.SerializerName;
                x.CacheNulls = sqliteOptions.CacheNulls;
                x.DBConfig = sqliteOptions.DBConfig;
            }

            options.RegisterExtension(new SQLLiteOptionsExtension(name, configure));
            return options;
        }
    }
}
