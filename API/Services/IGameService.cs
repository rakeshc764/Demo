using mongodb_dotnet_example.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mongodb_dotnet_example.Services
{
    public interface IGameService
    {

        Task UpdateAsync(string id, Game updatedGame);
        Task DeleteAsync(Game gameForDeletion);
        Task DeleteAsync(string id);
        Task<Game> CreateAsync(Game game);
        Task<Game?> GetAsync(string id);
        Task<List<Game>> GetAllAsync();


    }
}
