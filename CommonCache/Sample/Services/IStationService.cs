using Sample.Models;
using System.Threading.Tasks;
using Common.Caching.Core.Interceptor;

namespace Sample.Services
{
    public interface IStationService
    {
        [Cacheable(Expiration = 20, CacheKeyPrefix = "Weather_Station_",ExpirationBasedFromResult = "{CacheExpiry}")]
        Task<StationList> ReadStations();

        [CacheableEvict(CacheKeyPrefix = "Weather_Station_")]
        Task<bool> FillStations();

        //[Cacheable(Expiration = 10, CacheKeyPrefix = "Weather_Station_", Key = "brajesh")]
        //Task<Station> CompareStations(StationList first, Station second, int third);

        [Cacheable(Expiration = 10, CacheKeyPrefix = "Weather_Station_", Key = "{StationList.Id},{Station.Id}")]
        Task<Station> CompareStations(StationList first, Station second, int third);
    }
}
