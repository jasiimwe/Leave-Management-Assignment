using System;
using LeaveManagement.Models;

namespace LeaveManagement.Interfaces.Repositories
{
    public interface ILeaveRequestRepository : IBaseRepository<LeaveRequest, int>
    {
        Task<IEnumerable<LeaveRequest>> GetAllLeaveRequestForEmployee(LeaveRequest leaveRequest);
    }
}

