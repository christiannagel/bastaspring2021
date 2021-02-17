using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebAppForAzureAppConfig.Pages
{
    public class IndexAppSettings
    {
        public string? Setting1 { get; set; }
        public string? BackgroundColor { get; set; }
        public string? ForegroundColor { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IndexAppSettings? _settings;

        public IndexModel(IOptionsSnapshot<IndexAppSettings> options, ILogger<IndexModel> logger)
        {
            _logger = logger;
            Settings = options.Value;
        }

        public IndexAppSettings? Settings { get; }

        public void OnGet()
        {

        }
    }
}
