using System;
using LeaveManagement.Models;

namespace LeaveManagement.Interfaces.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee, int>
    {
    }
}

