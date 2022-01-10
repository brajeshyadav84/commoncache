namespace Common.Caching.Redis
{
    using Common.Caching.Core;
    using Common.Caching.Core.Configurations;

    public class RedisOptions: BaseProviderOptions
    {
        public RedisOptions()
        {

        }

        public RedisDBOptions DBConfig { get; set; } = new RedisDBOptions();
    }
}
