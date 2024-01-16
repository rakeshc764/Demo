using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace mongodb_dotnet_example.Models
{

    public class CORSOptions
    {
        public string[] AllowedHosts { get; set; }

    }
    public class CORSSettingsOptions : IConfigureOptions<CORSOptions>
    {
        public const string SectionName = "CORS";

        private readonly IConfiguration _configuration;

        public CORSSettingsOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(CORSOptions options)
        {
            _configuration
           .GetSection(SectionName)
           .Bind(options);
        }
    }
} 