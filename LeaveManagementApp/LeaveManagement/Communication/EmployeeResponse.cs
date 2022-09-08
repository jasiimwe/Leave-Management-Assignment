using System;
using LeaveManagement.Models;

namespace LeaveManagement.Communication
{
    public class EmployeeResponse : BaseResponse<Employee>
    {
        public EmployeeResponse(Employee employee) : base(employee)
        {
        }

        public EmployeeResponse(string message) : base(message)
        { }
    }
}

