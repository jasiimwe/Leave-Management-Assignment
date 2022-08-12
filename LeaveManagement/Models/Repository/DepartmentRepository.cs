using System;
using LeaveManagement.Persistent;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Models.Repository
{
    public class DepartmentRepository : IRepository<Department, int>
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context) => _context = context;




        public async Task<IEnumerable<Department>> GetAll() => await _context.Department.ToListAsync();

        public async Task<Department> GetById(int id) => await _context.Department.FindAsync(id);
        

        public async Task<Department> Insert(Department entity)
        {
            await _context.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            var department = await _context.Department.FirstOrDefaultAsync(b => b.DepartmentId == id);
            if(department != null)
            {
                _context.Remove(department);
            }
        }

        public async Task Update(Department entity)
        {
            _context.Department.Update(entity);

        }




    }
}

