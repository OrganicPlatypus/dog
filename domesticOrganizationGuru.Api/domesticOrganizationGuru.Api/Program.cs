using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using static domesticOrganizationGuru.Logger.SerilogConfiguration;

namespace domesticOrganizationGuru.Api
{
    public class Program
    {
        //private static Lazy<ConnectionMultiplexer> lazyConnection = CreateConnection();

        public static void Main(string[] args)
        {
            ConfigureLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging((hostContext, loggingBuilder) =>
                AddCustomLogger(loggingBuilder))
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });


        //public static ConnectionMultiplexer Connection
        //{
        //    get
        //    {
        //        return lazyConnection.Value;
        //    }
        //}

        //private static Lazy<ConnectionMultiplexer> CreateConnection()
        //{
        //    return new Lazy<ConnectionMultiplexer>(() =>
        //    {
        //        string cacheConnection = Configuration[SecretName];
        //        return ConnectionMultiplexer.Connect(cacheConnection);
        //    });
        //}
    }
}
