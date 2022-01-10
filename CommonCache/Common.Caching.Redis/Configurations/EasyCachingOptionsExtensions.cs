namespace Microsoft.Extensions.DependencyInjection
{
    using Common.Caching.Core;
    using Common.Caching.Core.Configurations;
    using Common.Caching.Redis;
    using Microsoft.Extensions.Configuration;
    using System;

    /// <summary>
    /// EasyCaching options extensions.
    /// </summary>
    public static class EasyCachingOptionsExtensions
    {
        /// <summary>
        /// Uses the SERedis provider (specify the config via hard code).
        /// </summary>        
        /// <param name="options">Options.</param>
        /// <param name="configure">Configure provider settings.</param>
        /// <param name="name">The name of this provider instance.</param>
        public static CachingServiceOptions UseRedis(
            this CachingServiceOptions options
            , Action<RedisOptions> configure
            , string name = EasyCachingConstValue.DefaultRedisName
            )
        {
            ArgumentCheck.NotNull(configure, nameof(configure));

            options.RegisterExtension(new RedisOptionsExtension(name, configure));
            return options;
        }

        /// <summary>
        /// Uses the SERedis provider (read config from configuration file).
        /// </summary>        
        /// <param name="options">Options.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="name">The name of this provider instance.</param>
        /// <param name="sectionName">The section name in the configuration file.</param>
        public static CachingServiceOptions UseRedis(
            this CachingServiceOptions options
            , IConfiguration configuration
            , string name = EasyCachingConstValue.DefaultRedisName
            , string sectionName = EasyCachingConstValue.RedisSection
            )
        {
            var dbConfig = configuration.GetSection(sectionName);
            var redisOptions = new RedisOptions();
            dbConfig.Bind(redisOptions);

            void configure(RedisOptions x)
            {
                x.EnableLogging = redisOptions.EnableLogging;
                x.MaxRdSecond = redisOptions.MaxRdSecond;
                x.LockMs = redisOptions.LockMs;
                x.SleepMs = redisOptions.SleepMs;
                x.SerializerName = redisOptions.SerializerName;
                x.CacheNulls = redisOptions.CacheNulls;
                x.DBConfig = redisOptions.DBConfig;
            }

            options.RegisterExtension(new RedisOptionsExtension(name, configure));
            return options;
        }
    }
}
