using DomesticOrganizationGuru.Api.Kernel.RegistrationContainers;
using DomesticOrganizationGuru.Api.Kernel.RegisterMappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace domesticOrganizationGuru.Api
{
    public class Startup
    {
        private const string PolicyName = "DomesticOrganizationGuruPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            services.RegisterServices();
            services.RegisterRopositories();
            services.RegisterMappers();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "domesticOrganizationGuru.Api", Version = "v1" });
            });
            services.AddCors(o => o.AddPolicy(PolicyName, builder =>
                {
                    //WithOrigins("http://localhost:4200")
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                })
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "domesticOrganizationGuru.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(PolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
