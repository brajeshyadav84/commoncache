namespace Common.Caching.Core.Interceptor
{
    using System.Reflection;

    /// <summary>
    /// Easycaching key generator.
    /// </summary>
    public interface IEasyCachingKeyGenerator
    {
        /// <summary>
        /// Gets the cache key.
        /// </summary>
        /// <returns>The cache key.</returns>
        /// <param name="methodInfo">Method info.</param>
        /// <param name="args">Arguments.</param>
        /// <param name="prefix">Prefix.</param>
        /// <param name="key">Key.</param>
        string GetCacheKey(MethodInfo methodInfo, object[] args, string prefix,string key);

        /// <summary>
        /// Gets the cache key prefix.
        /// </summary>
        /// <returns>The cache key prefix.</returns>
        /// <param name="methodInfo">Method info.</param>
        /// <param name="prefix">Prefix.</param>
        string GetCacheKeyPrefix(MethodInfo methodInfo, string prefix);
    }
}
