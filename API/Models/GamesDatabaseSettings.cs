using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace mongodb_dotnet_example.Models
{
    public class GamesDatabaseSettings  
    {
        public const string sectionName = "GamesDatabaseSettings";
        public string GamesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    
    public class GamesDatabaseSettingsOptions : IConfigureOptions<GamesDatabaseSettings>
    {
        IConfiguration _config;
        public GamesDatabaseSettingsOptions(IConfiguration config)
        {
            _config=config;
        }

        public void Configure(GamesDatabaseSettings options)
        {
            _config
          .GetSection(GamesDatabaseSettings.sectionName)
          .Bind(options);
        }
    }
}