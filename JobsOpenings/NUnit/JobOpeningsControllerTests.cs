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
    public class JobOpeningsControllerTests
    {
        #region AddJobs
        [TestCase]
        public async Task CreateJobsTrueTestCase()
        {
            // Arrange
            var jobsRepositoryMock = new Mock<IJobsRepository>();
            jobsRepositoryMock.Setup(repo => repo.AddJobs(It.IsAny<tblJobs>()))
                .Returns(true);
            var controller = new JobOpeningsController(jobsRepositoryMock.Object);

            var request = new Models.JobsModel.JobOpenings
            {
                Title = "Test",
                Description = "Test1",
                ClosingDate = DateTime.Now,
                DepartmentId = 2,
                LocationId = 1,
            };

            // Act
            var result = await controller.CreateJobs(request);

            // Assert
            var createdAtResult = (CreatedResult)result;
            Assert.AreEqual(201, createdAtResult.StatusCode);
        }
        [TestCase]
        public async Task CreateJobsFalseTestCase()
        {
            // Arrange
            var jobsRepositoryMock = new Mock<IJobsRepository>();
            jobsRepositoryMock.Setup(repo => repo.AddJobs(It.IsAny<tblJobs>()))
                .Returns(false);
            var controller = new JobOpeningsController(jobsRepositoryMock.Object);

            var request = new Models.JobsModel.JobOpenings
            {
                Title = "Test",
                Description = "Test1",
                ClosingDate = DateTime.Now,
                DepartmentId = 2,
                LocationId = 1,
            };

            // Act
            var result = await controller.CreateJobs(request);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }
        #endregion

        #region UpdateJobs
        [TestCase]
        public async Task UpdateJobsTestCase()
        {
            // Arrange
            var jobsRepositoryMock = new Mock<IJobsRepository>();
            jobsRepositoryMock.Setup(repo => repo.UpdateJobs(It.IsAny<int>(), It.IsAny<JobsModel.JobOpenings>()))
                .Returns(true);

            var request = new JobOpenings
            {
                Description = "ABC"
            };
            var controller = new JobOpeningsController(jobsRepositoryMock.Object);

            // Act
            var result = await controller.UpdateJobs(1, request);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            var okResult = (OkResult)result;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestCase]
        public async Task UpdateJobsInvalidIdTestCase()
        {
            // Arrange
            var jobsRepositoryMock = new Mock<IJobsRepository>();
            jobsRepositoryMock.Setup(repo => repo.UpdateJobs(It.IsAny<int>(), It.IsAny<JobsModel.JobOpenings>()))
                .Returns(false);

            var request = new JobOpenings
            {
                Description = "ABC"
            };
            var controller = new JobOpeningsController(jobsRepositoryMock.Object);

            // Act
            var result = await controller.UpdateJobs(1, request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }
        #endregion

        #region ListJobs
        [TestCase]
        public async Task ListJobsTestCase()
        {
            // Arrange
            var jobsRepositoryMock = new Mock<IJobsRepository>();
            jobsRepositoryMock.Setup(repo => repo.GetJobList(It.IsAny<PageRequest>()))
                .Returns(new PageResponse { Total = 2, data = new List<JobsModel.Data>() { new JobsModel.Data() { } } });

            var controller = new JobOpeningsController(jobsRepositoryMock.Object);


            var pageRequest = new PageRequest
            {
                q = "",
                PageNo = 1,
                PageSize = 10,
                LocationId = 1,
                DepartmentId = 2,
            };

            // Act
            var result = await controller.ListJobs(pageRequest);

            // Assert
            Assert.IsInstanceOf<ActionResult<PageResponse>>(result);
        }
        #endregion

        #region GetJobs
        [TestCase]
        public async Task GetJobsTestCase()
        {
            // Arrange
            var jobsRepositoryMock = new Mock<IJobsRepository>();
            jobsRepositoryMock.Setup(repo => repo.GetJobDetails(It.IsAny<int>()))
                .Returns(new JobsModel.Details());

            var controller = new JobOpeningsController(jobsRepositoryMock.Object);

            // Act
            var result = await controller.JobsDetails(1);

            // Assert
            Assert.IsInstanceOf<ActionResult<Details>>(result);
        }
        #endregion
    }
}
