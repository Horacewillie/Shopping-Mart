using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShoppingMart.Domain.Profiles;
using ShoppingMart.Infastructure;
using ShoppingMart.Infastructure.Data;
using ShoppingMart.Infastructure.Model;
using ShoppingMart.Logic.Managers;
using System;
using WatchDog;
using WatchDog.src.Enums;

namespace ShoppingMart.ProductAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private static bool IsLive => !AppConfig.IsDev;

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ShoppingMartDbContext>(options =>
            {
                options.UseSqlServer(GetShoppingMartDbConnection()).EnableSensitiveDataLogging();
            });

            IMapper mapper = MappingConfig.RegisterMap().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ProductManager>();
            services.AddScoped<CategoryManager>();
            InfastructureProvider.ConfigureServices(services, Configuration);
            services.AddWatchDogServices(opt =>
            {
                opt.IsAutoClear = true;
                opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Monthly;
            });

            services.AddControllers()
                .AddFluentValidation(x =>
                {
                    x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShoppingMart.ProductAPI", Version = "v1" });
            });
            services.AddLogging();
            services.AddSingleton(typeof(ILogger), typeof(Logger<Startup>));
        }


        //private string GetShoppingMartDbConnection() => IsLive ? AppConfig.ShoppingMartDbConnection
        //    : Configuration.GetConnectionString("ShoppingMartConfig");

        private string GetShoppingMartDbConnection() => IsLive ? AppConfig.ShoppingMartDbConnection
            : Configuration.GetSection("ShoppingMartConfig").Get<ShoppingMartConfig>().DbConnectionString;


        //This way of accessing environmental variable is valid

        //private string GetShoppingMartDbConnection() => IsLive ? AppConfig.ShoppingMartDbConnection
        //    : Configuration["ShoppingMartConfig:DbConnectionString"];



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var result = Environment.GetEnvironmentVariable("AppEnviron");

            bool IsDev = string.Equals(Environment.GetEnvironmentVariable("AppEnviron"), "dev", StringComparison.OrdinalIgnoreCase);

            if (env.IsDevelopment() || IsDev)
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingMart.ProductAPI v1"));
            }
            app.UseWatchDog(opt =>
            {
                opt.WatchPageUsername = Configuration["ShoppingMartConfig:WatchPageUsername"];
                opt.WatchPagePassword = Configuration["ShoppingMartConfig:WatchPagePassword"];
            });

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
