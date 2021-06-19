using LogViewer.Entities;
using Serilog.Events;
using System;
using System.Collections.Generic;

namespace LogViewer.Repository
{
    public interface ILogRepo
    {
        IEnumerable<MyLogEntity> GetAllLogEvents(LogEventLevel logLevel = LogEventLevel.Debug, DateTime? from = null, DateTime? to = null);
    }
}
