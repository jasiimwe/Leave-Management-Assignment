using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Models
{
	public class LeaveRequest
	{
		[Key]
		public int LeaveRequestId { get; set; }

		[ForeignKey("EmployeeId")]
		public int EmployeeId { get; set; }
		
		public virtual Employee? Employee { get; set; }

        [Column(TypeName = "date")]
		public DateTime LeaveStartDate { get; set; }

		[Column(TypeName = "date")]
		public DateTime LeaveEndDate { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(255)")]
		public string ReasonForLeave { get; set; }


        
	}
}

