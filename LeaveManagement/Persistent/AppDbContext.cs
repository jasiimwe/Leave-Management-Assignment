using System;
using LeaveManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Persistent
{
	public class AppDbContext : DbContext
	{
		public DbSet<Employee> Employee { get; set; }
		public DbSet<LeaveRequest> LeaveRequest { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}


		/*
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Employee>().ToTable("Employee");
			builder.Entity<Employee>().HasKey(p => p.EmployeeId);
			builder.Entity<Employee>().Property(p => p.EmployeeId).IsRequired().ValueGeneratedOnAdd();
			builder.Entity<Employee>().Property(p => p.FirstName).IsRequired().HasMaxLength(100);
			builder.Entity<Employee>().Property(p => p.LastName).IsRequired().HasMaxLength(100);
			builder.Entity<Employee>().Property(p => p.Department).IsRequired().HasMaxLength(50);
			builder.Entity<Employee>().Property(p => p.DateOfBirth).IsRequired();
			builder.Entity<Employee>().Property(p => p.EmpoyeeType).IsRequired().HasMaxLength(100);
			//builder.Entity<Category>().HasMany(p => p.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);
			

			builder.Entity<LeaveRequest>().ToTable("Leave Request");
			builder.Entity<LeaveRequest>().HasKey(p => p.LeaveRequestId);
			builder.Entity<LeaveRequest>().Property(p => p.LeaveRequestId).IsRequired().ValueGeneratedOnAdd();
			builder.Entity<LeaveRequest>().HasOne(p => p.Employee).WithOne(p => p.EmployeeId).HasForeignKey<Employee>(p => p.EmployeeId);
			builder.Entity<LeaveRequest>().Property(p => p.LeaveStartDate).IsRequired();
			builder.Entity<LeaveRequest>().Property(p => p.LeaveEndDate).IsRequired();
			

		}
		*/
	}
}

