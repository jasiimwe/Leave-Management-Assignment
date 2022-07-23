using System;
namespace LeaveManagement.Controllers.Validations
{
	public class LeaveRequestValidation
	{
		public LeaveRequestValidation()
		{
		}


		public static bool HasOverlap(DateTime inputStartDate, DateTime savedStartDate, DateTime inputEndDate, DateTime savedEndDate)
		{
			return inputStartDate < savedEndDate && savedStartDate > inputEndDate;
		}
	}
}

