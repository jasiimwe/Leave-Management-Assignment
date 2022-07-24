using System;
namespace LeaveManagement.Controllers.Validations
{
	public class EmployeeValidation
	{

		public static bool CheckDateOfBirth(DateTime dob)
        {

			return (dob > DateTime.Now) || (dob.AddYears(18) >= DateTime.Now);
            
        }

		public static bool GreaterThan18(DateTime dob)
        {
			return (dob.AddYears(18) >= DateTime.Now);
        }
	}
}

