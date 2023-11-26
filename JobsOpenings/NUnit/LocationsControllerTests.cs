using JobsOpenings.Controllers;
using JobsOpenings.Data;
using JobsOpenings.Interfaces;
using JobsOpenings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using static JobsOpenings.Models.JobsModel;

namespace JobsOpenings.NUnit
{
    public class LocationsControllerTests
    {
        #region AddLocations
        [TestCase]
        public async Task CreateLocationsTestCase1()
        {
            // Arrange
            var locRepositoryMock = new Mock<ILocationsRepository>();
            locRepositoryMock.Setup(repo => repo.AddLocation(It.IsAny<Location>()))
                .Returns(true);

            var controller = new LocationsController(locRepositoryMock.Object);

            var request = new Models.Location
            {
                Id = 1,
                title = "US Head Office",
                city = "Test1",
                state = "ABC",
                country = "United States",
                zip = 403705
            };

            // Act
            var result = await controller.CreateLocations(request);

            // Assert
            var createdAtResult = (CreatedResult)result;
            Assert.AreEqual(201, createdAtResult.StatusCode);
        }

        [TestCase]
        public async Task CreateLocationsFalseTestCase1()
        {
            // Arrange
            var locRepositoryMock = new Mock<ILocationsRepository>();
            locRepositoryMock.Setup(repo => repo.AddLocation(It.IsAny<Location>()))
                .Returns(false);

            var controller = new LocationsController(locRepositoryMock.Object);

            var request = new Models.Location
            {
                Id = 1,
                title = "US Head Office",
                city = "Test1",
                state = "ABC",
                country = "United States",
                zip = 403705
            };

            // Act
            var result = await controller.CreateLocations(request);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }
        #endregion

        #region UpdateLocations
        [Test]
        public async Task UpdateLocationsTestCase()
        {
            // Arrange
            var locRepositoryMock = new Mock<ILocationsRepository>();
            locRepositoryMock.Setup(repo => repo.UpdateLocations(It.IsAny<int>(), It.IsAny<Location>()))
                .Returns(true);

            var controller = new LocationsController(locRepositoryMock.Object);

            var request = new Models.Location
            {
                state = "XYZ"
            };

            // Act
            var result = await controller.UpdateLocations(1, request);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            var okResult = (OkResult)result;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task UpdateLocationsInvalidTestCase()
        {
            // Arrange
            var locRepositoryMock = new Mock<ILocationsRepository>();
            locRepositoryMock.Setup(repo => repo.UpdateLocations(It.IsAny<int>(), It.IsAny<Location>()))
                .Returns(false);

            var controller = new LocationsController(locRepositoryMock.Object);


            var id = 2;
            var request = new Models.Location
            {
                state = "XYZ"
            };

            // Act
            var result = await controller.UpdateLocations(id, request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetLocations
        [TestCase]
        public async Task GetLocationsTestCase()
        {
            // Arrange
            var locRepositoryMock = new Mock<ILocationsRepository>();
            locRepositoryMock.Setup(repo => repo.GetLocations())
                .Returns(new List<Location>());

            var controller = new LocationsController(locRepositoryMock.Object);

            // Act
            var result = await controller.GetLocations();

            // Assert
            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOf<ActionResult<Location>>(result);
        }
        #endregion
    }
}
