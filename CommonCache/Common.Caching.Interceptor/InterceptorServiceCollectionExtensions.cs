namespace Common.Caching.Interceptor
{
    using Common.Caching.Core.Configurations;
    using Common.Caching.Core.Interceptor;
    using global::AspectCore.Configuration;
    using global::AspectCore.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Aspectcore interceptor service collection extensions.
    /// </summary>
    public static class InterceptorServiceCollectionExtensions
    {
        /// <summary>
        /// Configures the AspectCore interceptor.
        /// </summary>
        /// <returns>The aspect core interceptor.</returns>
        /// <param name="services">Services.</param>
        /// <param name="options">Easycaching Interceptor config</param>        
        public static void ConfigureAspectCoreInterceptor(this IServiceCollection services, Action<EasyCachingInterceptorOptions> options)
        {
            services.TryAddSingleton<IEasyCachingKeyGenerator, DefaultEasyCachingKeyGenerator>();
            services.Configure(options);

            services.ConfigureDynamicProxy(config =>
            {
                bool all(MethodInfo x) => x.CustomAttributes.Any(data => typeof(CachingServiceInterceptorAttribute).GetTypeInfo().IsAssignableFrom(data.AttributeType));

                config.Interceptors.AddTyped<EasyCachingInterceptor>(all);
            });
        }
    }
}
