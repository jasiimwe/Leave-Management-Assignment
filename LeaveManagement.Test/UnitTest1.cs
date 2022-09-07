using LeaveManagement.Controllers;
using LeaveManagement.Interfaces.Services;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Moq;

namespace LeaveManagement.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task IndexReturnsAViewResultWithAListOfDepartments()
        {
            //Arrange
            var mockService = new Mock<IDepartmentService>();
            mockService.Setup(ser => ser.ListAsync())
                .ReturnsAsync(GetTestDepartments());
            var controller = new DepartmentController(mockService.Object);
            //Act
            var result = await controller.Index();
            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Department>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task CreateDepartment_ReturnsBadRequestResult_WhenModelStateIsInValid()
        {
            var mockService = new Mock<IDepartmentService>();
            mockService.Setup(ser => ser.ListAsync())
                .ReturnsAsync(GetTestDepartments());
            var controller = new DepartmentController(mockService.Object);
            controller.ModelState.AddModelError("Department Name", "Required");
            var newDepartment = GetTestDepartment();

            //Act
            var result = await controller.Create(newDepartment);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }


        [Fact]
        public async Task CreateDepartment_ReturnsRedirectAndAddDepartment_WhenModelStateIsValid()
        {
            //Arrange
            var mockService = new Mock<IDepartmentService>();
            mockService.Setup(sm => sm.SaveAsync(It.IsAny<Department>()))
                .Verifiable();

            var controller = new DepartmentController(mockService.Object);
            var newDepartment = GetTestDepartment();

            //Act
            var result = await controller.Create(GetTestDepartment());

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockService.Verify();

        }

        private Department GetTestDepartment()
        {
            return new Department()
            {
                DepartmentId = 1,
                DepartmentName = "Test Department Name"
            };
        }


        private List<Department> GetTestDepartments()
        {
            return new List<Department>()
            {
                new Department
                {
                    DepartmentId = 1,
                    DepartmentName = ""
                },

                new Department
                {
                    DepartmentId = 2,
                    DepartmentName = "testing Department two"
                },
            };
        }
    }
}