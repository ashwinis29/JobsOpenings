using JobsOpenings.Controllers;
using JobsOpenings.Data;
using JobsOpenings.Models;
using JobsOpenings.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace JobsOpenings.NUnit
{
    public class LocationsRepositoryTests
    {
        private AppDbContext _dbContext;
        private LocationRepository repo;

        [SetUp]
        public void Setup()
        {
            // Set up an in-memory database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new AppDbContext(options);

            // Initialize the repository with the in-memory database
            repo = new LocationRepository(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose of the in-memory database after each test
            _dbContext.Dispose();
        }

        [TestCase]
        public void reateLocationsTrueTestCase1()
        {
            // Arrange
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
            var result = repo.AddLocation(request);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase]
        public void CreateLocationFalseTestCase1()
        {
            // Arrange
            Location request = null;

            // Act
            var result = repo.AddLocation(request);

            // Assert
            Assert.IsFalse(result);
        }
        [TestCase]
        public void UpdateLocationsTrueTestCase()
        {
            // Arrange
            var existingLocation = new Location
            {
                Id = 1,
                title = "US Head Office",
                city = "Test1",
                state = "ABC",
                country = "United States",
                zip = 403705

            };
            _dbContext.Location.Add(existingLocation);
            _dbContext.SaveChanges();

            var id = existingLocation.Id;
            var request = new Models.Location
            {
                Id = 1,
                state = "XYZ"
            };

            // Act
            var result = repo.UpdateLocations(id, request);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase]
        public void UpdateLocationsFalseTestCase()
        {
            // Arrange
            var existingLocation = new Location
            {
                Id = 1,
                title = "US Head Office",
                city = "Test1",
                state = "ABC",
                country = "United States",
                zip = 403705

            };
            _dbContext.Location.Add(existingLocation);
            _dbContext.SaveChanges();

            var id = 2;
            var request = new Models.Location
            {
                state = "XYZ"
            };

            // Act
            var result = repo.UpdateLocations(id, request);

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase]
        public void GetLocationsTestCase()
        {
            // Arrange
            var existingLocation = new Location
            {
                Id = 1,
                title = "US Head Office",
                city = "Test1",
                state = "ABC",
                country = "United States",
                zip = 403705
            };
            _dbContext.Location.Add(existingLocation);
            _dbContext.SaveChanges();

            var id = existingLocation.Id;

            // Act
            var result = repo.GetLocations();

            // Assert
            Assert.IsNotNull(result);

        }
    }
}
