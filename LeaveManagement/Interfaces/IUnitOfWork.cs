using System;
using LeaveManagement.Models;
using LeaveManagement.Models.Repository;

namespace LeaveManagement.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Department, int> DepartmentRepositoty { get; }
        IRepository<EmployeeType, int> EmployeeTypeRepository { get; }
        IRepository<Employee, int> EmployeeRepository { get; }
        //IRepository<LeaveRequest, int> LeaveRepository { get;  }
        ILeaveRepository LeaveRepository {get; }

        Task<bool> SaveAsync();
        
    }
}

