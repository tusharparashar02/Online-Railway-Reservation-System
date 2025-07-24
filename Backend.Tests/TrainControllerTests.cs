using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using Backend.Controllers;
// using Backend.Services;
// using Backend.Models;

namespace Backend.Tests.Controllers
{
    [TestFixture]
    public class TrainsControllerTests
    {
        private Mock<ITrainService> _mockService;
        private TrainsController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<ITrainService>();
            _controller = new TrainsController(_mockService.Object);
        }

        [Test]
        public async Task GetAll_ReturnsOkResult_WithListOfTrains()
        {
            _mockService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<TrainDto> { new TrainDto { Id = 1, Name = "Express" } });

            var result = await _controller.GetAll();

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Get_ReturnsNotFound_WhenTrainDoesNotExist()
        {
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync((TrainDto)null);

            var result = await _controller.Get(1);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Get_ReturnsOk_WhenTrainExists()
        {
            _mockService.Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(new TrainDto { Id = 1, Name = "Express" });

            var result = await _controller.Get(1);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task GetTrainsByRoute_ReturnsOk_WithTrains()
        {
            _mockService.Setup(s => s.GetTrainsByRouteAsync("A", "B"))
                .ReturnsAsync(new List<TrainDto> { new TrainDto { Id = 1, Name = "RouteTrain" } });

            var result = await _controller.GetTrainsByRoute("A", "B");

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Create_ReturnsCreatedAtAction()
        {
            var dto = new TrainDto { Name = "New Train" };
            _mockService.Setup(s => s.CreateAsync(dto))
                .ReturnsAsync(new TrainDto { Id = 1, Name = "New Train" });

            var result = await _controller.Create(dto);

            Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());
        }

        [Test]
        public async Task Update_ReturnsNotFound_WhenTrainDoesNotExist()
        {
            _mockService.Setup(s => s.UpdateAsync(1, It.IsAny<TrainDto>()))
                .ReturnsAsync((TrainDto)null);

            var result = await _controller.Update(1, new TrainDto());

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

        [Test]
        public async Task Update_ReturnsOk_WhenTrainExists()
        {
            var updatedDto = new TrainDto { Id = 1, Name = "Updated Train" };
            _mockService.Setup(s => s.UpdateAsync(1, It.IsAny<TrainDto>()))
                .ReturnsAsync(updatedDto);

            var result = await _controller.Update(1, new TrainDto());

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Delete_ReturnsNoContent_WhenSuccessful()
        {
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _controller.Delete(1);

            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task Delete_ReturnsNotFound_WhenUnsuccessful()
        {
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(false);

            var result = await _controller.Delete(1);

            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }
    }
}
