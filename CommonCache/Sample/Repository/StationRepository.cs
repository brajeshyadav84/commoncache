using Sample.DataAccess;
using Sample.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Repository;
using Common.Caching.Core.Interceptor;

namespace Sample.Repository
{
    public class StationRepository : IStationRepository, IDisposable
    {
        private readonly StationContext _context;
        private bool disposedValue;

        public StationRepository(StationContext context)
        {
            _context = context;
        }

        public async Task<Station[]> GetStations()
        {
            if (_context is null)
                return null;
            var stations = await _context.Stations.ToArrayAsync();

            return stations;
        }

        [Cacheable(Expiration = 10)]
        public async Task<Station> GetStation(Guid id)
        {
            var station = await _context.Stations.FindAsync(id);
            return station;
        }


        public Task<Station> AddStation(Station station)
        {
            throw new NotImplementedException();
        }

        public Task<Station> RemoveStation(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool StationExists(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStation(Guid id, Station station)
        {
            throw new NotImplementedException();
        }
        [CacheableEvict(CacheKeyPrefix = "Weather_Station_Service")]
        public async Task<bool> PopulateTestData() {

            var stations = new List<Station>()
            {
                new Station {ActiveDate = DateTime.Now, City = "Oxenfurt", Id = Guid.NewGuid(), RecordOrigin = "From DB", StationCode = "OXF", Zip = "123456" },
                new Station {ActiveDate = DateTime.Now, City = "Yantra", Id = Guid.NewGuid(), RecordOrigin = "From DB", StationCode = "YAN", Zip = "123457" },
                new Station {ActiveDate = DateTime.Now, City = "Beauclaire", Id = Guid.NewGuid(), RecordOrigin = "From DB", StationCode = "BCR", Zip = "123458" },
                new Station {ActiveDate = DateTime.Now, City = "Novigrad", Id = Guid.NewGuid(), RecordOrigin = "From DB", StationCode = "NOV", Zip = "123459" },
                new Station {ActiveDate = DateTime.Now, City = "Spikeroog", Id = Guid.NewGuid(), RecordOrigin = "From DB", StationCode = "SPK", Zip = "123460" },
            };

            _context.Stations.AddRange(stations);
            await _context.SaveChangesAsync();

            return true;

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~StationRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        [Cacheable(Expiration = 10, Key = "station.StationCode,station.Id")]
        public async Task<Station> GetByStation(Station station)
        {
            await Task.Delay(3000);

            return await _context.Stations.Where(x => x.Id == station.Id).FirstOrDefaultAsync();
        }
    }
}
