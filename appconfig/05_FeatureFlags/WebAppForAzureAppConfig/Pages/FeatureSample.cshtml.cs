using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement;

namespace WebAppForAzureAppConfig.Pages
{
    public class FeatureSampleModel : PageModel
    {
        private readonly IFeatureManager _featureManager;
        public FeatureSampleModel(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }

        public string? FeatureXText { get; set; }
        public async void OnGet()
        {
            bool featureFlag = await _featureManager.IsEnabledAsync("FeatureX");
            string featureText = featureFlag ? "is" : "is not";
            FeatureXText = $"Feature X is {featureText} available";
        }
    }
}
