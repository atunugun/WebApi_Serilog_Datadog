using System;
using System.Runtime.CompilerServices;
using Serilog;
using Serilog.Sinks.Datadog.Logs;

namespace WebApi_Serilog_Datadog
{
    public static class LoggerExtensions
    {
        public static ILogger Enrich(this ILogger logger,
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return logger
                .ForContext("MemberName", memberName)
                .ForContext("LineNumber", sourceLineNumber);
        }

        public static ILogger Logger<T>()
        {
            return new LoggerConfiguration().WriteTo.DatadogLogs(
                "0d7f982529cc9294103e779de718e44d",
                service: "Users",
                source: "WebAPI",
                host: "localhost"
            ).CreateLogger();
        }
    }
}
