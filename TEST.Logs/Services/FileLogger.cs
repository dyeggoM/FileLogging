using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TEST.Logs.Services
{
    public class FileLogger : ILogger
    {
        private readonly IHostEnvironment _env;
        public FileLogger(IHostEnvironment env)
        {
            _env = env;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            var now = DateTime.UtcNow.AddHours(-5);
            var stateResult = exception != null ? exception.StackTrace : "";
            var logRecord = $"[{now:yyyy-MM-dd HH:mm:ss+00:00}] [{logLevel}] {formatter(state, exception)} {stateResult}";
            var directoryPath = Path.Combine(_env.ContentRootPath, "Logs");
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            var fileName = $"{now:yyyyMMdd}_Logs.txt";
            var filePath = Path.Combine(directoryPath, fileName);
            using (var streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine(logRecord);
            }
        }
    }
}
