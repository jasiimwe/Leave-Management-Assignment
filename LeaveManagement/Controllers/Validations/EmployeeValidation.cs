using System;
namespace LeaveManagement.Controllers.Validations
{
	public class EmployeeValidation
	{

		public static bool CheckDateOfBirth(DateTime dob)
        {

			return (dob > DateTime.Now) || (dob < DateTime.Now.AddYears(-18));
            
        }
	}
}

