using System;
using LeaveManagement.Models;

namespace LeaveManagement.Communication
{
    public class LeaveRequestResponse : BaseResponse<LeaveRequest>
    {
        public LeaveRequestResponse(LeaveRequest leaveRequest) :base(leaveRequest)
        {
        }

        public LeaveRequestResponse(string message) : base(message) { }
    }
}

