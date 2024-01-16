using mongodb_dotnet_example.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;

namespace mongodb_dotnet_example.Services
{
    public class GamesService : IGameService
    {
        private readonly IMongoCollection<Game> _games;
        private readonly IMongoClient _mongoClient;
        private readonly GamesDatabaseSettings _settings;
        public readonly ILogger<GamesService> _logger;

        public GamesService(IOptions<GamesDatabaseSettings> settings, IMongoClient mongoClient, ILogger<GamesService> logger )
        {
            _settings = settings.Value;
            _mongoClient = mongoClient;
            var mongoDatabase = _mongoClient.GetDatabase(_settings.DatabaseName);
            _games = mongoDatabase.GetCollection<Game>(_settings.GamesCollectionName);
            _logger=logger;
            _logger.LogInformation($"Client connected to {_settings.ConnectionString} with {_settings.DatabaseName}");

        }


        public async Task<List<Game>> GetAllAsync() =>
        await _games.Find(_ => true).ToListAsync();


        public async Task<Game?> GetAsync(string id)
        {
            return await _games.Find(x => x.Id == id).FirstOrDefaultAsync();

        }
        public async Task<Game> CreateAsync(Game game)
        {
            await _games.InsertOneAsync(game);
            return game;
        }

        public async Task UpdateAsync(string id, Game updatedGame) => await _games.ReplaceOneAsync(game => game.Id == id, updatedGame);

        public async Task DeleteAsync(Game gameForDeletion) => await _games.DeleteOneAsync(game => game.Id == gameForDeletion.Id);

        public async Task DeleteAsync(string id) => await _games.DeleteOneAsync(game => game.Id == id);
    }
}