namespace Common.Caching.Core.Interceptor
{
    using Common.Caching.Core.Configurations;
    using System;

    /// <summary>
    /// Easycaching interceptor attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CachingServiceInterceptorAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether is hybrid provider.
        /// </summary>
        /// <value><c>true</c> if is hybrid provider; otherwise, <c>false</c>.</value>
        public bool IsHybridProvider { get; set; } = false;

        /// <summary>
        /// Gets or sets the cache key prefix. 
        /// </summary>
        /// <value>The cache key prefix.</value>
        public string CacheKeyPrefix { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the cache provider name. The default value is <seealso cref="EasyCachingInterceptorOptions.CacheProviderName"/> 
        /// </summary>
        /// <value>The cache key prefix.</value>
        public string CacheProviderName { get; set; }

        /// <summary>
        ///  Prevent cache provider errors from affecting business
        /// </summary>
        /// <value>The cache key prefix.</value>
        public bool IsHighAvailability { get; set; } = true;

        /// <summary>
        ///  Custome Key Names taken from complex paramters using specific fields and their corresponding values.
        /// </summary>
        /// <value>Comma separated fields e.g. {complexParam.field}</value>
        public string Key { get; set; } = string.Empty;
    }
}
