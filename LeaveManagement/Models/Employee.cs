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
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DisplayName("Employee Type")]
        public int EmployeeTypeId { get; set; }
        public EmployeeType? EmployeeType { get; set; }

        

        
 
	}
}

