using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebAppForAzureAppConfig.Pages
{
    public record IndexAppSettings
    {
        public string? Setting1 { get; init; }
    }

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IOptions<IndexAppSettings> options, ILogger<IndexModel> logger)
        {
            _logger = logger;
            Setting1 = options.Value.Setting1 ?? "no value";
        }

        public string Setting1 { get; }

        public void OnGet()
        {

        }
    }
}
