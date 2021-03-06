using Actio.Common.Mongo;
using Actio.Common.RabbitMq;
using Actio.Services.Activities.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Actio.Services.Activities
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
            services.AddLogging()
                .AddMongoDb(Configuration)
                .AddRabbitMq(Configuration)
                .AddCreateActivityService()
                .AddCategoryRepository()
                .AddActivityRepository()
                .AddCustomMongoSeeder()
                .AddActivityService()
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Actio.Services.Activities", Version = "v1" });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                    .UseSwagger()
                    .UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Actio.Services.Activities v1"));
            }

            app.ApplicationServices.GetService<IDatabaseInitializer>()?.InitializeAsync();

            app.UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
