using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Serilog;

namespace OnnoRokom.BDJobs.Web.SerilogHelper
{
    public class Logger
    {
        private readonly ILogger _errorLogger;

        public Logger()
        {
            _errorLogger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .WriteTo.File("D:\\log.txt")
               .CreateLogger();
        }

        public ILogger GetLogger => _errorLogger;
    }
}