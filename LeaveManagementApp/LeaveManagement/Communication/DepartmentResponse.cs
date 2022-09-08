using System;
using LeaveManagement.Models;

namespace LeaveManagement.Communication
{
    public class DepartmentResponse : BaseResponse<Department>
    {
        public DepartmentResponse(Department department) : base(department)
        {
        }

        public DepartmentResponse(string message) : base(message) { }
    }
}

