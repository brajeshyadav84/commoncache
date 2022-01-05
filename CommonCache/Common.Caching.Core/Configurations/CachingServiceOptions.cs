﻿namespace Common.Caching.Core.Configurations
{
    using System.Collections.Generic;

    /// <summary>
    /// EasyCaching options.
    /// </summary>
    public class CachingServiceOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:EasyCaching.Core.EasyCachingOptions"/> class.
        /// </summary>
        public CachingServiceOptions()
        {
            Extensions = new List<IEasyCachingOptionsExtension>();
        }

        /// <summary>
        /// Gets the extensions.
        /// </summary>
        /// <value>The extensions.</value>
        internal IList<IEasyCachingOptionsExtension> Extensions { get; }

        /// <summary>
        /// Registers the extension.
        /// </summary>
        /// <param name="extension">Extension.</param>
        public void RegisterExtension(IEasyCachingOptionsExtension extension)
        {
            ArgumentCheck.NotNull(extension, nameof(extension));

            Extensions.Add(extension);
        }
    }
}
