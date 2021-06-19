using LogViewer.Models;
using LogViewer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog.Events;
using System;
using System.Linq;

namespace LogViewer.Controllers
{
    [Route("/")]
    public class LogController : Controller
    {
        #region props
        private readonly ILogRepo _logRepo;
        [BindProperty(SupportsGet = true, Name = "SelectedLogLevel")]
        public string SelectedLogLevel { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? SelectedFrom { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? SelectedTo { get; set; }
        #endregion

        public LogController(ILogRepo logRepo)
        {
            _logRepo = logRepo;
        }

        public IActionResult Index()
        {
            SelectedLogLevel ??= "Debug";
            var selectedLogLevel = Enum.Parse<LogEventLevel>(SelectedLogLevel);

            var entities = _logRepo.GetAllLogEvents(selectedLogLevel, SelectedFrom, SelectedTo);
            
            var models = entities.Select(e => new LogModelEvent
            {
                Time = e.Timestamp.DateTime.ToString("yyyy.MM.dd hh:mm.ss.fff"),
                Level = e.Level.ToString(),
                Message = e.Message,
                IsDarkMode = false
            });

            var levels = Enum.GetNames(typeof(LogEventLevel));
            var model = new LogPageModel
            {
                LogEvents = models,
                LogLevels = new SelectList(levels, "Debug")
            };

            return View(model);
        }
    }
}
