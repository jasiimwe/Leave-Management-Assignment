using System;
using LeaveManagement.Interfaces.Repositories;
using LeaveManagement.Models;
using LeaveManagement.Models.Repository;

namespace LeaveManagement.Interfaces
{
    public interface IUnitOfWork
    {
        IDepartmentRepository departmentRepositoty { get; }
        IEmployeeRepository employeeRepository { get; }
        IEmployeeTypeRepository employeeTypeRepository { get; }
        ILeaveRequestRepository leaveRequestRepository { get; }

        Task CompleteAsync();

    }
}

