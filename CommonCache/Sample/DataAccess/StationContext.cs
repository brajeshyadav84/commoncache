using Microsoft.EntityFrameworkCore;
using Sample.Models;

namespace Sample.DataAccess
{
    public class StationContext: DbContext
    {
        public StationContext(DbContextOptions<StationContext> options):base(options){}

        public DbSet<Station> Stations { get; set; }
    }
}
