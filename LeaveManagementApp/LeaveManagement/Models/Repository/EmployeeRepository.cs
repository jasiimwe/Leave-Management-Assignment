using System;
using LeaveManagement.Interfaces.Repositories;
using LeaveManagement.Persistent;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Models.Repository
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context):base(context)
        {
            
        }

        public void Delete(Employee entity)
        {
            _context.Employee.Remove(entity);
        }

        public async Task<IEnumerable<Employee>> GetAll() => await _context.Employee.Include(e => e.Department).Include(e => e.EmployeeType).AsNoTracking().ToListAsync();
        

        public async Task<Employee> GetById(int id) => await _context.Employee.Include(e => e.Department).Include(e => e.EmployeeType).FirstOrDefaultAsync(i => i.EmployeeId == id);
        

        public async Task InsertAsync(Employee entity)
        {
            await _context.Employee.AddAsync(entity);
        }

        public void Update(Employee entity)
        {
            _context.Employee.Update(entity);
        }
    }
}

