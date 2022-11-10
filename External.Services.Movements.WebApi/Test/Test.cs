using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Security.Principal;
using Xunit;
using Moq;
using External.Services.Movements.WebApi.Business;
using System.Net.Mime;
using System.Text;
using System.Security.Policy;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch.Internal;
using External.Services.Movements.WebApi.Controllers;
using External.Services.Movements.WebApi.Models;
using External.Services.Movements.WebApi.Test.Helpers;

namespace External.Services.Movements.WebApi.Test
{
    public class Test : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program>_factory;

        private readonly Mock<IMovementsManager> _mockRepo;
        private readonly MovementsController _controller;
        private readonly ILogger<MovementsController> _logger;

        public Test(
            CustomWebApplicationFactory<Program> factory)
        {
            //Configuring WebApplication for tests with Factory 
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _client.BaseAddress = new Uri("http://localhost:7136/v1/Movements");

            //Creating mock MovementsController
            _mockRepo = new Mock<IMovementsManager>();
            _controller = new MovementsController(_logger, _mockRepo.Object);
        }

        [Fact]
        public async Task Get_IncomingMovements()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetMovements(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.Is<EnumMovementType>(t=>t.Equals(EnumMovementType.Incoming)), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
            .Returns(new PagedMovements()
            {
                Movements = new List<Movement>()
                {
                    new Movement
                    {
                        MovementId = 1 * 6 + 1004,
                        Account = "NL54FAKE0062046111",
                        MovementType = EnumMovementType.Incoming,
                        Amount = (decimal)500 + 1,
                        AccountFrom = "NL96NMFK0208212218",
                        AccountTo = "NL54FAKE0062046111"
                    }
                }
            });
            //Act
            var result = _mockRepo.Object.GetMovements(1, 10, "NL54FAKE0062046111", EnumMovementType.Incoming, "NL96NMFK0208212218", "NL54FAKE0062046111", 400, 600);
            //Assert
            Assert.Equal(EnumMovementType.Incoming, result.Movements.First().MovementType);
        }

        [Fact]
        public void Get_FiscalMovements()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetMovements(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.Is<EnumMovementType>(t => t.Equals(EnumMovementType.FiscalTransfer)), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<decimal>()))
            .Returns(new PagedMovements()
            {
                Movements = new List<Movement>()
                {
                    new Movement
                    {
                        MovementId = 1 * 6 + 1004,
                        Account = "NL54FAKE0062046111",
                        MovementType = EnumMovementType.FiscalTransfer,
                        Amount = (decimal)500 + 1,
                        AccountFrom = "NL96NMFK0208212218",
                        AccountTo = "NL54FAKE0062046111"
                    }
                }
            });
            //Act
            var result = _mockRepo.Object.GetMovements(1, 10, "NL54FAKE0062046111", EnumMovementType.FiscalTransfer, "NL96NMFK0208212218", "NL54FAKE0062046111", 400, 600);
            //Assert
            Assert.Equal(EnumMovementType.FiscalTransfer, result.Movements.First().MovementType);
        }

        [Fact]
        public void Get_InterestMovements()
        {
            // Arrange
            //Act
            var response =  _client.GetAsync($"?pageNumber=1&pageSize=10&account=NL54FAKE0062046111&movementType=Interest").Result;
            var result = ConvertResultToPagedMovements(response);

            //Assert
            foreach (var res in result.Movements)
            {
                Assert.Equal(EnumMovementType.Interest, res.MovementType);
            }            
        }

        //Method for converting http result to PagedMovements object
        private PagedMovements ConvertResultToPagedMovements(HttpResponseMessage response)
        {
            string responseBody = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<PagedMovements>(responseBody);    
        }
    }
}