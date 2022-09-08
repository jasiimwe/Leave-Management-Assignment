using System;
using LeaveManagement.Models;
using LeaveManagement.Models.Repository;

namespace LeaveManagement.Interfaces.Repositories
{
    public interface IDepartmentRepository : IBaseRepository<Department, int>
    {
    }
}

