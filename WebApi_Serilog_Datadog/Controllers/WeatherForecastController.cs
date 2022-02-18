using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Sinks.Datadog.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace WebApi_Serilog_Datadog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly ILogger Logger = LoggerExtensions.Logger<WeatherForecastController>();

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly string[] Exceptions = new[]
        {
            "InvalidCastException", "IndexOutOfRangeException", "NullReferenceException", "InvalidCastException", "OutOfMemoryException"
        };

        private static readonly string[] Warnings = new[]
        {
            "Variable never used", "Use await instead", "Unreachable code",
        };

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            try
            {
                var rng = new Random();

                for (int i = 0; i < 50; i++)
                {
                    Logger.Warning("Warning WarningDescription:{@WarningDescription} MethodName:{@MethodName} LineNumber:{@LineNumber}", 
                        Warnings[rng.Next(Warnings.Length)], "WeatherForecastController.Get", (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                    Thread.Sleep(1000);

                    Logger.Information("Getting Weather Forecast Temparature:{@Temparature} Summary:{@Summary} MethodName:{@MethodName} LineNumber:{@LineNumber}",
                        rng.Next(-20, 55), Summaries[rng.Next(Summaries.Length)], "WeatherForecastController.Get", (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                    Thread.Sleep(1000);

                    Logger.Error("Error ErrorDescription:{@ErrorDescription} MethodName:{@MethodName} LineNumber:{@LineNumber}", 
                        Exceptions[rng.Next(Exceptions.Length)], "WeatherForecastController.Get", (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber());
                    Thread.Sleep(1000);
                }
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            }
            catch(Exception ex)
            {
            }
            return Enumerable.Empty<WeatherForecast>();
        }
    }
}
