using CrudApp.BLL;
using CrudApp.BLL.Abstract;
using CrudApp.Cache;
using CrudApp.Cache.Abstract;
using CrudApp.Data.Helpers;
using CrudApp.Data.Service;
using CrudApp.Data.Service.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CrudApp.API
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
            services.AddControllers();

            services.AddScoped<IBookManager, BookManager>();
            services.AddScoped<IBookService, Dapper_BookService>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<ICacheService, RedisCacheService>();
            SqlHelper.ConnectionString = Configuration.GetConnectionString("CrudApp");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
