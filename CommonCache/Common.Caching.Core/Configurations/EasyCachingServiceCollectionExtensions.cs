namespace Microsoft.Extensions.DependencyInjection
{
    using Common.Caching.Core;
    using Common.Caching.Core.Configurations;
    using System;

    /// <summary>
    /// EasyCaching service collection extensions.
    /// </summary>
    public static class EasyCachingServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the easycaching.
        /// </summary>
        /// <returns>The easy caching.</returns>
        /// <param name="services">Services.</param>
        /// <param name="setupAction">Setup action.</param>
        public static IServiceCollection CachingService(this IServiceCollection services, Action<CachingServiceOptions> setupAction)
        {
            ArgumentCheck.NotNull(setupAction, nameof(setupAction));

            //Options and extension service
            var options = new CachingServiceOptions();
            setupAction(options);
            foreach (var serviceExtension in options.Extensions)
            {
                serviceExtension.AddServices(services);
            }
            services.AddSingleton(options);

            return services;
        }
    }
}
