using Sample.Models;
using Sample.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Services
{
    public class StationService : IStationService, IDisposable
    {
        private readonly IStationRepository _repository;
        private bool disposedValue;

        public StationService(IStationRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> FillStations()
        {
            await _repository.PopulateTestData();

            return true;
        }

        public async Task<StationList> ReadStations()
        {
            System.Diagnostics.Debug.WriteLine("Actual GetStations Method Executed...");
            //simulate DB Latency: 
            await Task.Delay(3000);
            var list = await _repository.GetStations();

            return new StationList{Id = Guid.NewGuid(), Stations = list, CacheExpiry = DateTime.Now.AddMinutes(1) };
        }

        public async Task<StationList> ReadStationsData(string Data)
        {
            System.Diagnostics.Debug.WriteLine("Actual GetStations Method Executed...");
            //simulate DB Latency: 
            await Task.Delay(3000);
            var list = await _repository.GetStations();

            return new StationList { Id = Guid.NewGuid(), Stations = list, CacheExpiry = DateTime.Now.AddMinutes(1) };
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                }

                disposedValue = true;
            }
        }

        

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async Task<Station> CompareStations(StationList first, Station second, int third)
        {
            await Task.Delay(3000);

            var result = await _repository.GetByStation(second);

            return result;
        }
    }
}
