using System;
using LeaveManagement.Communication;
using LeaveManagement.Models;

namespace LeaveManagement.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> ListAsync();
        Task<Department> ListById(int id);
        Task<DepartmentResponse> SaveAsync(Department department);
        Task<DepartmentResponse> UpdateAsync(int id, Department department);
        Task<DepartmentResponse> DeleteAsync(int id);
    }
}

