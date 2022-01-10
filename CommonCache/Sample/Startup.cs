using Common.Caching.Core.Configurations;
using Common.Caching.Interceptor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.DataAccess;
using Sample.Repository;
using Sample.Services;

namespace Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StationContext>(opt => opt.UseInMemoryDatabase("StationDB"));
            services.AddControllers();

            #region Sqllite Cache
            //services.CachingService(options =>
            //    {
            //        options.UseSQLite(config =>
            //        {
            //            config.DBConfig = new SQLiteDBOptions { FileName = "my.db" };
            //        }, "SQLLite");
            //    });
            //services.ConfigureAspectCoreInterceptor(options => options.CacheProviderName = "SQLLite"); 
            #endregion

            #region Redis Cache
            services.CachingService(options =>
            {
                //options.UseRedis(Configuration, "redis1");
                options.UseRedis(config =>
                {
                    config.DBConfig.Endpoints.Add(new ServerEndPoint("127.0.0.1", 6379));
                }, "redis1");
            });
            services.ConfigureAspectCoreInterceptor(options => options.CacheProviderName = "redis1");
            #endregion

            #region InMemory Cache
            //services.CachingService(options =>
            //    {
            //        options.UseInMemory("m1");
            //    });
            //services.ConfigureAspectCoreInterceptor(options => options.CacheProviderName = "m1");
            #endregion


            services.AddScoped<IStationRepository, StationRepository>();
            services.AddScoped<IStationService, StationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
