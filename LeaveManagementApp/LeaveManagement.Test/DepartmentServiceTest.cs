using LeaveManagement.Communication;
using LeaveManagement.Interfaces;
using LeaveManagement.Interfaces.Repositories;
using LeaveManagement.Models;
using LeaveManagement.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Test
{
    public class DepartmentServiceTest
    {
        [Fact]
        public async Task Department_Service_Returns_List_Of_Department()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            mockService.Setup(s => s.departmentRepositoty.GetAll())
                .ReturnsAsync(GetDepartments());
            var service = new DepartmentService(mockService.Object);
            //Act

            var result =  await service.ListAsync();
            //Assert
            var viewResult = Assert.IsType<List<Department>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Department>>(viewResult);
            Assert.Equal(2, model.Count());
        }

        
        [Fact]
        public async Task Department_Service_Returns_No_List_Of_Department()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            mockService.Setup(s => s.departmentRepositoty.GetAll())
                .ReturnsAsync(GetNoDepartments());
            var service = new DepartmentService(mockService.Object);
            //Act

            var result = await service.ListAsync();
            //Assert
            var viewResult = Assert.IsType<List<Department>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Department>>(viewResult);
            Assert.Empty(model);
        }

        [Fact]
        public async Task Department_Service_Returns_One_Department_Given_Id()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();
            
            mockService.Setup(s => s.departmentRepositoty.GetById(GetTestDepartment().DepartmentId))
                .ReturnsAsync(GetTestDepartment());
            var service = new DepartmentService(mockService.Object);
            //Act

            var result = await service.ListById(GetTestDepartment().DepartmentId);
            //Assert
            var viewResult = Assert.IsType<Department>(result);
            var model = Assert.IsAssignableFrom<Department>(viewResult);
            Assert.IsType<Department>(model);
        }

        [Fact]
        public async Task Department_Service_Add_Department()
        {
            //Arrange
            var mockService = new Mock<IUnitOfWork>();

            mockService.Setup(s => s.departmentRepositoty.InsertAsync(It.IsAny<Department>()))
                .Verifiable();
            var service = new DepartmentService(mockService.Object);
            //Act

            var result = await service.SaveAsync(GetTestDepartment());
            //Assert
            var viewResult = Assert.IsType<DepartmentResponse>(result);
            mockService.Verify();
        }








        #region MocK Data

        


        private Department GetTestDepartment()
        {
            return new Department()
            {
                DepartmentId = 1,
                DepartmentName = "Test Department Name"
            };
        }

        private List<Department> GetDepartments()
        {
            return new List<Department>()
            {
               
                new Department
                {
                    DepartmentId = 1,
                    DepartmentName = "testing department one"
                },

                new Department
                {
                    DepartmentId = 2,
                    DepartmentName = "testing Department two"
                },
                
            };
        }

        private List<Department> GetNoDepartments()
        {
            return new List<Department>();
        }
        #endregion
    }
}
