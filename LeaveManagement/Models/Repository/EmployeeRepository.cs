using System;
using LeaveManagement.Persistent;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Models.Repository
{
    public class EmployeeRepository : IRepository<Employee, int>
    {
        private readonly AppDbContext _context;
       
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        

        public async Task<IEnumerable<Employee>> GetAll() => await _context.Employee.Include(e => e.Department).Include(e => e.EmployeeType).ToListAsync();


        public async Task<Employee> GetById(int id) => await _context.Employee.Include(e => e.Department).Include(e => e.EmployeeType).FirstOrDefaultAsync(i => i.EmployeeId == id);
        

        public async Task<Employee> Insert(Employee entity)
        {
                await _context.Employee.AddAsync(entity);
                return entity;
        }

        public async Task Delete(int id)
        {
            var employee = await _context.Employee.FirstOrDefaultAsync(b => b.EmployeeId == id);
            if (employee != null)
            {
                _context.Remove(employee);
            }
        }

        public async Task Update(Employee entity)
        {
           _context.Entry(entity).State = EntityState.Modified;
            
        }
    }
}

