using System;
using LeaveManagement.Models;

namespace LeaveManagement.Communication
{
    public class EmployeeTypeResponse : BaseResponse<EmployeeType>
    {
        public EmployeeTypeResponse(EmployeeType employeeType) : base(employeeType)
        {
        }

        public EmployeeTypeResponse(string message) : base(message) { }
    }
}

