namespace Common.Caching.Core.Interceptor
{
    using System;

    /// <summary>
    /// Easycaching able attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CacheableAttribute : CachingServiceInterceptorAttribute
    {
        /// <summary>
        /// Gets or sets the expiration. The default value is 30 second.
        /// </summary>
        /// <value>The expiration.</value>
        public int Expiration { get; set; } = 30;

        /// <summary>
        /// Dynamically overrides the expiration based from a datetime field existing in the 
        /// resulting complex object of the method in context. Provide the exact string property name of the datetime field.
        /// </summary>
        public string ExpirationBasedFromResult { get; set; }
    }     
}
