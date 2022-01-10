namespace Common.Caching.SQLLite
{
    using Common.Caching.Core;
    using Common.Caching.Core.Configurations;

    public class SQLLiteOptions: BaseProviderOptions
    {
        public SQLLiteOptions()
        {

        }

        public SQLLiteDBOptions DBConfig { get; set; } = new SQLLiteDBOptions();
    }
}
