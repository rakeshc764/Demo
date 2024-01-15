using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MongoTests
{
    public class ControllerTests
    {

        private readonly Mock<IGameService> mockGameService;
        private readonly GamesController controller;

        public   ControllerTests()
        {
            // Initialize common objects in the constructor
            mockGameService = new Mock<IGameService>();
            controller = new GamesController(mockGameService.Object);
        }
       
        [Fact]
        public async Task GetAll_ReturnsOkResultWithListOfGames()
        {
            // Arrange
            var expectedGames = new List<Game>
        {
            new Game { Id = "", Name = "Game 1" },
            new Game { Id = "", Name = "Game 2" },
            // Add more games as needed
        };

            // Set up the mock to return the expected list of games
            mockGameService.Setup(service => service.GetAllAsync()).ReturnsAsync(expectedGames);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var actualGames = okResult.Value as List<Game>;
            Assert.NotNull(actualGames);

            Assert.Equal(expectedGames.Count, actualGames.Count);
            // Add more assertions based on your specific requirements

            // Example: Asserting the values of the first game
            Assert.Equal(expectedGames[0].Id, actualGames[0].Id);
            Assert.Equal(expectedGames[0].Name, actualGames[0].Name);
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsOkResultWithGame()
        {
            // Arrange
            var validId = "65a4da126d23c72cfe6819b0";
            mockGameService.Setup(service => service.GetAsync(validId)).ReturnsAsync(new Game { Id = validId });

            // Act
            var result = await controller.Get(validId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsType<Game>(okResult.Value);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidId = "65a4da126d23c72cfe6819b02";
            mockGameService.Setup(service => service.GetAsync(invalidId)).ReturnsAsync(null as Game);

            // Act
            var result = await controller.Get(invalidId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}