using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//#if Test_Logging
//#if Test_Serilog
        using Serilog;
        using Serilog.Formatting.Json;
//#endif
//#endif
    
namespace Dopamine_API_DOTNET5
{
    public class Program
    {
        public static void Main(string[] args)
        {
//#if Test_Logging
//#if Test_Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(new JsonFormatter(), "Logs/Serilog-Common-" + ".txt",
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                    rollingInterval: RollingInterval.Day)
                .WriteTo.File("Logs/Serilog-Errors-" + DateTime.UtcNow.ToString("yyyyMMdd") + ".txt",
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
                .CreateLogger();
//#endif
//#endif
            CreateHostBuilder(args).Build().Run();

//#if Test_Logging
//#if Test_Serilog
            Log.Information("Nu er vores Web Api startet op !!!");
//#endif
//#endif
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
//#if Test_Logging
//#if Test_Serilog
                    .UseSerilog()
//#endif
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventLog();
                    logging.AddTraceSource("Information, ActivityTracing");
                    logging.AddFile("Logs/mylog-{Date}.txt");
#if Test_Seqlog
                        logging.AddSeq();
#endif
                })
//#endif
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
