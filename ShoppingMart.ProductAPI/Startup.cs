using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShoppingMart.Domain.Profiles;
using ShoppingMart.Infastructure.Data;
using ShoppingMart.Infastructure.Model;
using System;

namespace ShoppingMart.ProductAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public bool IsLive => !AppConfig.IsDev;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

           
            services.AddDbContext<ShoppingMartDbContext>(options =>
            {
                options.UseSqlServer(GetShoppingMartDbConnection());
            });

            IMapper mapper = MappingConfig.RegisterMap().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShoppingMart.ProductAPI", Version = "v1" });
            });
        }

       
        private string GetShoppingMartDbConnection() => IsLive ? AppConfig.ShoppingMartDbConnection
            : Configuration.GetConnectionString("ShoppingMartDb");
       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var result = Environment.GetEnvironmentVariable("AppEnviron");
            Console.WriteLine("Hery");
            Console.WriteLine(result);

            bool IsDev = string.Equals(Environment.GetEnvironmentVariable("AppEnviron"), "dev", StringComparison.OrdinalIgnoreCase);

            if (env.IsDevelopment() || IsDev)
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingMart.ProductAPI v1"));
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
