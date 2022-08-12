using System;
using LeaveManagement.Persistent;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Models.Repository
{
    public class EmployeeTypeRepository : IRepository<EmployeeType, int>
    {
        private readonly AppDbContext _context;
        public EmployeeTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var employeeType = await _context.EmployeeType.FirstOrDefaultAsync(b => b.EmployeeTypeId == id);
            if (employeeType != null)
            {
                _context.Remove(employeeType);
            }
        }

        public async Task<IEnumerable<EmployeeType>> GetAll() => await _context.EmployeeType.ToListAsync();


        public async Task<EmployeeType> GetById(int id) => await _context.EmployeeType.FirstOrDefaultAsync(b => b.EmployeeTypeId == id);
        

        public async Task<EmployeeType> Insert(EmployeeType entity)
        {
           await _context.AddAsync(entity);
            return entity;
        }

        public async Task Update(EmployeeType entity)
        {
            _context.EmployeeType.Update(entity);

        }


    }
}

