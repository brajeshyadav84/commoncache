using Sample.Models;
using System.Threading.Tasks;
using Common.Caching.Core.Interceptor;

namespace Sample.Services
{
    public interface IStationService
    {
        [Cacheable(Expiration = 20, CacheKeyPrefix = "Station_", ExpirationBasedFromResult = "{CacheExpiry}")]
        Task<StationList> ReadStations();

        [Cacheable(Expiration = 20, CacheKeyPrefix = "Station_", Key ="{data}", ExpirationBasedFromResult = "{CacheExpiry}")]
        Task<StationList> ReadStationsData(string data);

        [CacheableEvict(CacheKeyPrefix = "Station_")]
        Task<bool> FillStations();

        [Cacheable(Expiration = 10, CacheKeyPrefix = "Station_", Key = "{StationList.Id},{Station.Id}")]
        Task<Station> CompareStations(StationList first, Station second, int third);
    }
}
