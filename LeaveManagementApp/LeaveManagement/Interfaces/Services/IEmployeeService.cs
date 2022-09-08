using System;
using LeaveManagement.Communication;
using LeaveManagement.Models;

namespace LeaveManagement.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> ListAsync();
        Task<Employee> ListById(int id);
        Task<EmployeeResponse> SaveAsync(Employee employee);
        Task<EmployeeResponse> UpdateAsync(int id, Employee employee);
        Task<EmployeeResponse> DeleteAsync(int id);
    }
}

