using Sample.Models;
using System;
using System.Threading.Tasks;

namespace Sample.Repository
{
    public interface IStationRepository
    {
       
        Task<Station[]> GetStations();

  
        Task<Station> GetStation(Guid id);
        Task UpdateStation(Guid id, Station station);
        Task<Station> AddStation(Station station);
        Task<Station> RemoveStation(Guid id);
        bool StationExists(Guid id);

        
        Task<bool> PopulateTestData();

        
        Task<Station> GetByStation(Station station);
        
    }
}
