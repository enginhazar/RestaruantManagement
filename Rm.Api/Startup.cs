using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Rm.Core.Interfaces;
using Rm.Infrastructure;
using Rm.Services;
using Microsoft.EntityFrameworkCore;
using Rm.Core;

namespace Rm.Api
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

            services.AddApplicationCore();

            //            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rm.Api", Version = "v1" });
            });
            //services.AddDbContext<RmDbContext>(opt => opt.UseInMemoryDatabase("TestDB"));

            services.AddDbContext<RmDbContext>(options =>
       options.UseSqlServer(Configuration.GetConnectionString("RmDb"),
       b => b.MigrationsAssembly("Rm.Api")));



            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IReservationService), typeof(ReservationService));
            services.AddScoped(typeof(ITableService), typeof(TableService));
            services.AddScoped(typeof(ISmsService), typeof(SmsService));
            services.AddScoped(typeof(ISmsProvider), typeof(SmsProvider));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rm.Api v1"));
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
