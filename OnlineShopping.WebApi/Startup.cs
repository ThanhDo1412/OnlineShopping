using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineShopping.Database;
using OnlineShopping.Database.Entity;
using OnlineShopping.Database.Repository;
using OnlineShopping.Service;
using OnlineShopping.Service.Interface;
using OnlineShopping.Service.Mapper;
using OnlineShopping.WebApi.Middleware;

namespace OnlineShopping.WebApi
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
            //Configuration for BD
            services.AddDbContext<OnlineShoppingContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("OnlineShoppingConnection")));

            services.AddAutoMapper(typeof(ProductMapper));

            services.AddLogging();

            services.AddScoped<IReponsitory<Activity>, OnlineShoppingRepository<Activity>>();
            services.AddScoped<IReponsitory<ActivitySession>, OnlineShoppingRepository<ActivitySession>>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IActivityService, ActivityService>();

            services.AddControllers();
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

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
