using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagement.Models
{
    public class EmployeeType
    {
        [Key]
        public int EmployeeTypeId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? EmployeeTypeName { get; set; }


    }
}

