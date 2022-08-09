using System;
using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistent
{
	public class AppDbContext : DbContext
	{
		public DbSet<Employee> Employee { get; set; }
		public DbSet<LeaveRequest> LeaveRequest { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<LeaveManagement.Models.Department> Department { get; set; }

		public DbSet<LeaveManagement.Models.EmployeeType> EmployeeType { get; set; }


		
	}
}

