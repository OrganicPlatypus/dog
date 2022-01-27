using domesticOrganizationGuru.SignalR;
using domesticOrganizationGuru.Validation.Registration;
using DomesticOrganizationGuru.Api.StartupKernel;
using DomesticOrganizationGuru.Api.StartupKernel.RegisterMappers;
using DomesticOrganizationGuru.Api.StartupKernel.RegistraterUtilityContainers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using static DomesticOrganizationGuru.Api.StartupKernel.RegistraterUtilityContainers.ValidationErrorsHelper.ValidationErrorsCustomResponseHelper;

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
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer
                .Connect(Configuration.GetValue<string>("RedisConnection:ConnectionString"))
                );

            services.RegisterServices();
            services.RegisterRopositories();
            services.RegisterMappers();

            services.AddSignalR();

            services
                .AddControllers(options =>
                {
                    options.Filters.Add<HttpResponseExceptionFilter>();
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        return ValidationErrorsResponse(actionContext);
                    };
                })
                .RegisterValidators();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "domesticOrganizationGuru.Api", Version = "v1" });
            });

            services.AddCors(o => o.AddPolicy(PolicyName, builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    builder.AllowCredentials();
                })
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
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
                endpoints.MapHub<NotesHub>(string.Format($"/{nameof(NotesHub).ToLowerInvariant()}"));
            });
        }
    }
}
