using System;
namespace LeaveManagement.Models.Repository
{
    public interface ILeaveRepository : IRepository<LeaveRequest, int>
    {
        Task<IEnumerable<LeaveRequest>> GetAllLeaveRequestForEmployee(LeaveRequest leaveRequest);
    }
}

