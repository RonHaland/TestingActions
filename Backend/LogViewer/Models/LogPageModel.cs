using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace LogViewer.Models
{
    public class LogPageModel
    {
        public IEnumerable<LogModelEvent> LogEvents { get; set; }
        public SelectList LogLevels { get; set; }
        public string SelectedLogLevel { get; set; }
        public DateTime? SelectedFrom { get; set; }
        public DateTime? SelectedTo { get; set; }
    }
}
