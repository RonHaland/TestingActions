using Microsoft.Azure.Cosmos.Table;
using Serilog.Events;
using System;
using System.Collections.Generic;

namespace LogViewer.Entities
{
    public class MyLogEntity : ITableEntity
    {
        public MyLogEntity()
        {
        }

        public LogEventLevel Level { get; set; }
        public string Message { get; set; }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string ETag { get; set; }

        public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            Message = properties["RenderedMessage"].StringValue;
            if (properties.ContainsKey("Exception"))
                Message += Environment.NewLine + properties["Exception"].StringValue;
            Level = Enum.Parse<LogEventLevel>(properties["Level"].StringValue);
        }

        public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            //We don't plan on saving anything :D
            throw new NotImplementedException();
        }
    }
}
