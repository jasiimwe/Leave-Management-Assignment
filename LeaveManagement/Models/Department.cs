using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? DepartmentName { get; set; }



    }
}

