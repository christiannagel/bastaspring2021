using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace WebAppForAzureAppConfig.Pages
{
    public class TheSecretModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public TheSecretModel(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }

        public string? TheAnswer { get; private set; }
        public void OnGet()
        { 
            TheAnswer = _configuration["theultimateanswer"];
        }
    }
}
