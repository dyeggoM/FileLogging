using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEST.Logs.Services
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly IHostEnvironment _env;
        public FileLoggerProvider(IHostEnvironment env)
        {
            _env = env;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_env);
        }

        public void Dispose()
        {
        }
    }
}
