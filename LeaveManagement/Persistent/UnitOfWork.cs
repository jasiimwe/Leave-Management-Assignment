using System;
using LeaveManagement.Interfaces;
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

        public IRepository<Department, int> DepartmentRepositoty => new DepartmentRepository(_context);

        public IRepository<EmployeeType, int> EmployeeTypeRepository => new EmployeeTypeRepository(_context);

        public IRepository<Employee, int> EmployeeRepository => new EmployeeRepository(_context);

        public ILeaveRepository LeaveRepository => new LeaveRepository(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

