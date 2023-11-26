using JobsOpenings.Controllers;
using JobsOpenings.Data;
using JobsOpenings.Interfaces;
using JobsOpenings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System.Data.Entity.Core.Objects;
using System.Runtime.CompilerServices;
using System.Xml;
using static JobsOpenings.Models.JobsModel;
using Assert = NUnit.Framework.Assert;

namespace JobsOpenings.NUnit
{
    [TestClass]
    public class DepartmentsControllerTests
    {
        #region AddDepartments
        [TestCase]
        public async Task CreateDepartmentsTrueTestCase1()
        {
            // Arrange
            var deptRepositoryMock = new Mock<IDepartmentRepository>();
            deptRepositoryMock.Setup(repo => repo.AddDepartments(It.IsAny<Department>()))
                .Returns(true);

            var controller = new DepartmentsController(deptRepositoryMock.Object);

            var request = new Models.Department
            {
                Id = 1,
                title = "Software Developer"
            };

            // Act
            var result = await controller.CreateDepartments(request);

            // Assert
            var createdResult = result as CreatedResult;
            Assert.AreEqual(201, createdResult.StatusCode);
        }

        public async Task CreateDepartmentsFalseTestCase1()
        {
            // Arrange
            var deptRepositoryMock = new Mock<IDepartmentRepository>();
            deptRepositoryMock.Setup(repo => repo.AddDepartments(It.IsAny<Department>()))
                .Returns(false);

            var controller = new DepartmentsController(deptRepositoryMock.Object);

            var request = new Models.Department
            {
                Id = 1,
                title = "Software Developer"
            };

            // Act
            var result = await controller.CreateDepartments(request);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }
        #endregion

        #region UpdateDepartments
        [TestCase]
        public async Task UpdateDepartmentsTestCase()
        {
            // Arrange
            var deptRepositoryMock = new Mock<IDepartmentRepository>();
            deptRepositoryMock.Setup(repo => repo.UpdateDepartments(It.IsAny<int>(), It.IsAny<Department>()))
                .Returns(true);

            var controller = new DepartmentsController(deptRepositoryMock.Object);
            
            var request = new Models.Department
            {
                title = "Test 2"
            };

            // Act
            var result = await controller.UpdateDepartments(1, request);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            var okResult = (OkResult)result;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task UpdateDepartmentsInvalidTestCase()
        {
            // Arrange
            var deptRepositoryMock = new Mock<IDepartmentRepository>();
            deptRepositoryMock.Setup(repo => repo.UpdateDepartments(It.IsAny<int>(), It.IsAny<Department>()))
                .Returns(false);
            var controller = new DepartmentsController(deptRepositoryMock.Object);
            var id = 2;
            var request = new Models.Department
            {
                title = "Test 2"
            };

            // Act
            var result = await controller.UpdateDepartments(id, request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region GetDepartments
        [TestCase]
        public async Task GetDepartmentsTestCase()
        {
            // Arrange
            var deptsRepositoryMock = new Mock<IDepartmentRepository>();
            deptsRepositoryMock.Setup(repo => repo.GetDepartments())
                .Returns(new List<Department>());

            var controller = new DepartmentsController(deptsRepositoryMock.Object);

            // Act
            var result = await controller.GetDepartments();

            // Assert
            Assert.IsInstanceOf<ActionResult<Department>>(result);
            Assert.IsNotNull(result.Result);
        }

        [TestCase]
        public async Task GetDepartmentsNullTestCase()
        {
            // Arrange
            var deptsRepositoryMock = new Mock<IDepartmentRepository>();
            deptsRepositoryMock.Setup(repo => repo.GetDepartments())
                .Returns(() => throw new Exception());

            var controller = new DepartmentsController(deptsRepositoryMock.Object);

            // Act
            var result = await controller.GetDepartments();

            // Assert
            Assert.AreEqual(result.Value,null);
        }
        #endregion
    }
}
