using System;
namespace LeaveManagement.Controllers.Validations
{
	public class LeaveRequestValidation
	{
		public LeaveRequestValidation()
		{
		}

		//validation One
		public static bool IsDateGreaterThan(DateTime startDate, DateTime endDate)
        {
			//return DateTime.Compare(startDate, endDate) > 0;
			return startDate > endDate;
        }

		//validation Two
		public static bool HasOverlap(DateTime startDate1, DateTime endDate1, DateTime startDate2, DateTime endDate2)
		{
			return startDate1 < endDate2 && endDate1 > startDate2;
		}

		public static bool IsLessThanMonth(DateTime startDate, DateTime endDate)
        {
			return (startDate - endDate).TotalDays < 30;
        }
		
	}
}

