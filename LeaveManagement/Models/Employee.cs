using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Models
{
	public class Employee
	{
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage ="This First Name is required")]
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Department")]
        public string Department { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Employee Type")]
        [EnumDataType(typeof(EmployeeTypeChoices))]
        public EmployeeTypeChoices EmployeeType { get; set; }

        public virtual List<LeaveRequest>? LeaveRequests { get; set; }


        public enum EmployeeTypeChoices
        {
            Managers= 1,
            OtherStaff = 2
         
        }

        
 
	}
}

