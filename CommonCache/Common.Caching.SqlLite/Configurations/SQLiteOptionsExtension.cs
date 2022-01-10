namespace Common.Caching.SQLLite
{
    using Common.Caching.Core;
    using Common.Caching.Core.Configurations;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;

    /// <summary>
    /// SQLite options extension.
    /// </summary>
    internal sealed class SQLLiteOptionsExtension : IEasyCachingOptionsExtension
    {
        /// <summary>
        /// The name.
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// The configure.
        /// </summary>
        private readonly Action<SQLLiteOptions> configure;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:EasyCaching.SQLite.SQLLiteOptionsExtension"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="configure">Configure.</param>
        public SQLLiteOptionsExtension(string name, Action<SQLLiteOptions> configure)
        {
            this._name = name;
            this.configure = configure;
        }

        /// <summary>
        /// Adds the services.
        /// </summary>
        /// <param name="services">Services.</param>
        public void AddServices(IServiceCollection services)
        {
            services.AddOptions();

            services.Configure(_name, configure);

            services.TryAddSingleton<IEasyCachingProviderFactory, DefaultEasyCachingProviderFactory>();
            services.AddSingleton<ISQLLiteDatabaseProvider, SQLLiteDatabaseProvider>(x =>
            {
                var optionsMon = x.GetRequiredService<IOptionsMonitor<SQLLiteOptions>>();
                var options = optionsMon.Get(_name);
                return new SQLLiteDatabaseProvider(_name, options);
            });

            services.AddSingleton<IEasyCachingProvider, SQLLiteCachingProvider>(x =>
            {
                var dbProviders = x.GetServices<ISQLLiteDatabaseProvider>();
                var optionsMon = x.GetRequiredService<IOptionsMonitor<SQLLiteOptions>>();
                var options = optionsMon.Get(_name);
                var factory = x.GetService<ILoggerFactory>();
                return new SQLLiteCachingProvider(_name, dbProviders, options, factory);
            });
        }
    }
}
