using System;
using LeaveManagement.Communication;
using LeaveManagement.Interfaces;
using LeaveManagement.Interfaces.Services;

using LeaveManagement.Models;
using LeaveManagement.Models.Repository;
using LeaveManagement.Persistent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LeaveManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        Messages messages = new Messages();
        

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }

        //Date of Bith Validation
        protected bool CheckDateOfBirth(Employee employee)
        {
            if (employee.DateOfBirth > DateTime.Now || employee.DateOfBirth.AddYears(18) >= DateTime.Now)
                return false;

            return true;

            
        }


        #region Service Implementation
        public async Task<IEnumerable<Employee>> ListAsync()
        {
            return await _unitOfWork.employeeRepository.GetAll();
            
        }

        public async Task<EmployeeResponse> SaveAsync(Employee employee)
        {
            if (!CheckDateOfBirth(employee))
                return new EmployeeResponse(messages.checkDateOfBirthErrorMessage);

            try
            {
                await _unitOfWork.employeeRepository.InsertAsync(employee);
                await _unitOfWork.CompleteAsync();

                return new EmployeeResponse(employee);
            }catch(Exception ex)
            {
                return new EmployeeResponse($"An error occured: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> UpdateAsync(int id, Employee employee)
        {
            var existingEmployee = await _unitOfWork.employeeRepository.GetById(id);
            if (existingEmployee == null)
            {
                return new EmployeeResponse("Employee Not found");
            }

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.DepartmentId = employee.DepartmentId;
            existingEmployee.EmployeeTypeId = employee.EmployeeTypeId;
            existingEmployee.DateOfBirth = employee.DateOfBirth;
            

            try
            {
                
                await _unitOfWork.CompleteAsync();

                return new EmployeeResponse(existingEmployee);
            }catch(Exception ex)
            {
                return new EmployeeResponse($"An error occured when updating employee: {ex.Message}");
            }
            
        }

        public async Task<EmployeeResponse> DeleteAsync(int id)
        {
            var existingEmployee = await _unitOfWork.employeeRepository.GetById(id);

            if (existingEmployee == null)
                return new EmployeeResponse("Emplopyee not found.");

            try
            {
                _unitOfWork.employeeRepository.Delete(existingEmployee);
                await _unitOfWork.CompleteAsync();

                return new EmployeeResponse("Employee deleted successfully");
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new EmployeeResponse($"An error occurred when deleting the Employee: {ex.Message}");
            }
        }

        public Task<Employee> ListById(int id)
        {
            var employee = _unitOfWork.employeeRepository.GetById(id);
            return employee;
        }
        #endregion
    }
}

