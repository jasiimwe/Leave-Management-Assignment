using System;
namespace LeaveManagement.Controllers.Validations
{
	public class LeaveRequestValidation
	{
		public LeaveRequestValidation()
		{
		}


		public static bool HasOverlap(DateTime startDate1, DateTime endDate1, DateTime startDate2, DateTime endDate2)
		{
			return startDate1 < endDate2 && endDate1 > startDate2;
		}
	}
}

