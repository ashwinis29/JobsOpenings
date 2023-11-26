using JobsOpenings.Controllers;
using JobsOpenings.Data;
using JobsOpenings.Models;
using JobsOpenings.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Data.Entity;
using static JobsOpenings.Models.JobsModel;

namespace JobsOpenings.NUnit
{
    public class JobsRepositoryTests
    {
        private AppDbContext _dbContext;
        private JobsRepository repo;

        [SetUp]
        public void Setup()
        {
            // Set up an in-memory database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new AppDbContext(options);

            // Initialize the repository with the in-memory database
            repo = new JobsRepository(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            // Dispose of the in-memory database after each test
            _dbContext.Dispose();
        }

        [TestCase]
        public void AddJobsTrueTestCase()
        {
            // Arrange
            var job = new tblJobs
            {
                Id = 1,
                title = "Test",
                description = "Test1",
                closingDate = DateTime.Now,
                departmentId = 2,
                locationId = 1,
                code = "JOB-12"
            };

            // Act
            var result = repo.AddJobs(job);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase]
        public void AddJobsFalseTestCase()
        {
            // Arrange
            tblJobs job = null;

            // Act
            var result = repo.AddJobs(job);

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase]
        public void AddJobsExceptionTestCase()
        {
            // Arrange
            var job = new tblJobs
            {
                Id = 1,
                title = "Test",
                description = "Test1",
                closingDate = DateTime.Now,
                departmentId = 2,
                locationId = 1,
                code = "JOB-12"
            };

            // Act
            var result = repo.AddJobs(job);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase]
        public void UpdateJobsTrueTestCase()
        {
            // Arrange
            var existingJob = new tblJobs
            {
                Id = 1,
                title = "Test",
                description = "Test1",
                closingDate = DateTime.Now,
                departmentId = 2,
                locationId = 1,
                code = "JOB-12"
            };
            _dbContext.tblJobs.Add(existingJob);
            _dbContext.SaveChangesAsync();

            var id = existingJob.Id;
            var request = new JobOpenings
            {
                Description = "ABC"
            };

            // Act
            var result = repo.UpdateJobs(id, request);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase]
        public void UpdateJobsFalseTestCase()
        {
            // Arrange
            var existingJob = new tblJobs
            {
                Id = 1,
                title = "Test",
                description = "Test1",
                closingDate = DateTime.Now,
                departmentId = 2,
                locationId = 1,
                code = "JOB-12"
            };
            _dbContext.tblJobs.Add(existingJob);
            _dbContext.SaveChangesAsync();

            var id = 2;
            var request = new JobOpenings
            {
                Description = "ABC"
            };

            // Act
            var result = repo.UpdateJobs(id, request);

            // Assert
            Assert.IsFalse(result);
        }

        [TestCase]
        public void ListJobsTestCase()
        {
            // Arrange
            // Add sample data into the database
            var job1 = new tblJobs
            {
                Id = 1,
                title = "Job 1",
                description = "Test1",
                closingDate = DateTime.Now,
                departmentId = 2,
                locationId = 1,
                code = "JOB-12"
            };
            var job2 = new tblJobs
            {
                Id = 2,
                title = "Job 2",
                description = "Test2",
                closingDate = DateTime.Now,
                departmentId = 2,
                locationId = 1,
                code = "JOB-23"
            };

            _dbContext.tblJobs.AddRange(job1, job2);
            _dbContext.SaveChanges();

            var pageRequest = new PageRequest
            {
                q = "",
                PageNo = 1,
                PageSize = 10,
                LocationId = 1,
                DepartmentId = 2,
            };

            // Act
            var result = repo.GetJobList(pageRequest);

            // Assert
            Assert.IsInstanceOf<PageResponse>(result);
        }
    }
 }
