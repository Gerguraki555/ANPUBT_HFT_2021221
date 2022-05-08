using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ANPUBT_HFT_2021221.Endpoint.Services;
using ANPUBT_HFT_2021221.Logic;
using ANPUBT_HFT_2021221.Repository;
using ZC7ADM_HFT_2021221.Data;

namespace ANPUBT_HFT_2021221.Endpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IEmployeeLogic,EmployeeLogic>();
            services.AddTransient<IRestaurantLogic, RestaurantLogic>();
            services.AddTransient<IGuestLogic,GuestLogic>();
           
            services.AddTransient<IGuestRepository,GuestRepository>();
            services.AddTransient<IEmployeeRepository,EmployeeRepository>();
            services.AddTransient<IRestaurantRepository,RestaurantRepository>();
            services.AddTransient<IRestaurantRepository,RestaurantRepository>();

            services.AddTransient<RestaurantDbContext, RestaurantDbContext>();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x=>x.AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:8512")); //Lehet, hogy át kell még írni

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
                
            });
        }
    }
}
