using LogViewer.Entities;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Cosmos.Table.Queryable;
using Microsoft.Extensions.Configuration;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogViewer.Repository
{
    public class LogRepo : ILogRepo
    {
        
        public LogRepo(IConfiguration config)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(config.GetValue<string>("TableConnection"));

            var tableClient = storageAccount.CreateCloudTableClient();
            var tableRef = tableClient.GetTableReference("LogEventEntity");
            tableRef.CreateIfNotExists();
            TableRef = tableRef;
        }

        public IEnumerable<MyLogEntity> GetAllLogEvents(LogEventLevel logLevel = LogEventLevel.Debug, DateTime? from = null, DateTime? to = null) 
        {
            var allLogEvents = new[] { LogEventLevel.Verbose, LogEventLevel.Debug, LogEventLevel.Information, LogEventLevel.Warning, LogEventLevel.Error, LogEventLevel.Fatal };
            var validLogEvents = allLogEvents.Where(e => ((int)e) >= ((int)logLevel));

            var q = new TableQuery<MyLogEntity>();

            var filterString = "";

            // Can actually Query these as DateTime.Ticks compared to PartitionKey
            // PartitionKey le 637597305301674816
            if (from != null)
                filterString += (filterString.Length > 0 ? " and " : "") + $"Timestamp ge datetime'{from.Value:s}'";
            if (to != null)
                filterString += (filterString.Length > 0 ? " and " : "") + $"Timestamp le datetime'{to.Value:s}'";

            q = q.Where(filterString);

            var entities = TableRef
                .ExecuteQuery(q)
                .Where(l => validLogEvents
                    .Contains(l.Level));

            //if (from != null)
            //    entities = entities.Where(l => l.Timestamp >= from);

            //if (to != null)
            //    entities = entities.Where(l => l.Timestamp <= to);

            if (entities == null)
                return Enumerable.Empty<MyLogEntity>();

            return entities.OrderByDescending(p => p.Timestamp);
        }

        public CloudTable TableRef { get; set; }
    }
}
