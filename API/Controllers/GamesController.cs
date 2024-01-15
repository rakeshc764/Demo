using mongodb_dotnet_example.Models;
using mongodb_dotnet_example.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace mongodb_dotnet_example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gamesService)
        {
            _gameService = gamesService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Game>))]
        public async Task<IActionResult> GetAll()
        {
            var list = await _gameService.GetAllAsync();
            return Ok(list);
        }


        [HttpGet("{id:length(24)}", Name = "GetGame")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
        public async Task<IActionResult> Get(string id)
        {
            var game = await _gameService.GetAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Game game)
        {
            if (game == null)
                return BadRequest("invalid input");

            await _gameService.CreateAsync(game);

            return CreatedAtRoute("GetGame", new { id = game.Id.ToString() }, game);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Game))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(string id, [FromBody] Game gameIn)
        {
            if (gameIn == null)
                return BadRequest("invalid input");

            var game = await _gameService.GetAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            await _gameService.UpdateAsync(id, gameIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("invalid input");
            
            var game = await _gameService.GetAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            _gameService.DeleteAsync(game.Id);

            return NoContent();
        }
    }
}