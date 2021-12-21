using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Templates;

namespace domesticOrganizationGuru.Logger
{
    public class SerilogConfiguration
    {
        private static LoggerConfiguration _loggerConfiguration;

        static SerilogConfiguration()
        {
            _loggerConfiguration = new LoggerConfiguration();
        }

        public static void ConfigureLogger()
        {
            ExpressionTemplate formatter = new ExpressionTemplate(
                    "[{@t:HH:mm:ss} {@l:u3} " +
                    "{Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)}] {@m}\n{@x}");
            Log.Logger = _loggerConfiguration
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Information)
                .WriteTo.File(formatter, "dog.log_.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
        public static ILoggingBuilder AddCustomLogger(ILoggingBuilder loggingBuilder)
        {
            return loggingBuilder.AddSerilog(dispose: true);
        }
    }
}
