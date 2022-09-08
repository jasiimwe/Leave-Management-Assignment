using System;
using LeaveManagement.Communication;
using LeaveManagement.Models;

namespace LeaveManagement.Interfaces.Services
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<LeaveRequest>> ListAsync();
        Task<LeaveRequest> ListById(int id);
        Task<LeaveRequestResponse> SaveAsync(LeaveRequest leaveRequest);
        Task<LeaveRequestResponse> UpdateAsync(int id, LeaveRequest leaveRequest);
        Task<LeaveRequestResponse> DeleteAsync(int id);
    }
}

