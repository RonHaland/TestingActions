using LogViewer.Helpers;

namespace LogViewer.Models
{
    public class LogModelEvent
    {
        public string Time { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public bool IsDarkMode { get; set; }
        public string Color => ColorHelper.GetBootstrapClass(Level, IsDarkMode);
    }
}
