using JobsOpenings.Controllers;
using JobsOpenings.Data;
using JobsOpenings.Models;
using JobsOpenings.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace JobsOpenings.NUnit
{
    public class DepartmentsRepositoryTests
    {
        private AppDbContext _dbContext;
        private DepartmentsRepository repo;

        [SetUp]
        public void Setup()
        {
            // Set up an in-memory database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new AppDbContext(options);

            // Initialize the repository with the in-memory database
            repo = new DepartmentsRepository(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose of the in-memory database after each test
            _dbContext.Dispose();
        }

        [TestCase]
        public void CreateDepartmentTrueTestCase1()
        {
            // Arrange
            var request = new Models.Department
            {
                Id = 1,
                title = "TEst 1"
            };

            // Act
            var result = repo.AddDepartments(request);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase]
        public void CreateDepartmentFalseTestCase1()
        {
            // Arrange
            Department request = null;

            // Act
            var result = repo.AddDepartments(request);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void UpdateDepartmentsTrueTestCase()
        {
            // Arrange
            var existingDept = new Department
            {
                Id = 1,
                title = "Test 1"

            };
            _dbContext.Department.Add(existingDept);
            _dbContext.SaveChanges();

            var id = existingDept.Id;
            var request = new Models.Department
            {
                title = "XYZ"
            };

            // Act
            var result = repo.UpdateDepartments(id, request);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateDepartmentsFalseTestCase()
        {// Arrange
            var existingDept = new Department
            {
                Id = 1,
                title = "Test 1"

            };
            _dbContext.Department.Add(existingDept);
            _dbContext.SaveChanges();

            var id = 2;
            var request = new Models.Department
            {
                title = "XYZ"
            };

            // Act
            var result = repo.UpdateDepartments(id, request);

            // Assert
            Assert.IsFalse(result);
        }
        [TestCase]
        public void GetDepartmentsTestCase()
        {
            // Arrange
            var existingDept = new Department
            {
                Id = 1,
                title = "Test 2"
            };
            _dbContext.Department.Add(existingDept);
            _dbContext.SaveChanges();

            var id = existingDept.Id;

            // Act
            var result = repo.GetDepartments();

            // Assert
            Assert.IsInstanceOf<List<Department>>(result);

        }
    }
}
