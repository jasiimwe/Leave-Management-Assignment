using System;
using LeaveManagement.Communication;
using LeaveManagement.Models;

namespace LeaveManagement.Interfaces.Services
{
    public interface IEmployeeTypeService
    {
        Task<IEnumerable<EmployeeType>> ListAsync();
        Task<EmployeeType> ListById(int id);
        Task<EmployeeTypeResponse> SaveAsync(EmployeeType employeeType);
        Task<EmployeeTypeResponse> UpdateAsync(int id, EmployeeType employeeType);
        Task<EmployeeTypeResponse> DeleteAsync(int id);
    }
}

