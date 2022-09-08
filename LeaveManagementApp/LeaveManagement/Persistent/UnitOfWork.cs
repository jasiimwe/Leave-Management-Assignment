using System;
using LeaveManagement.Interfaces;
using LeaveManagement.Interfaces.Repositories;
using LeaveManagement.Models;
using LeaveManagement.Models.Repository;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LeaveManagement.Persistent
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
       
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            
        }

        public IDepartmentRepository departmentRepositoty => new DepartmentRepository(_context);

        public IEmployeeRepository employeeRepository => new EmployeeRepository(_context);

        public IEmployeeTypeRepository employeeTypeRepository => new EmployeeTypeRepository(_context);

        public ILeaveRequestRepository leaveRequestRepository => new LeaveRepository(_context);

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

