using System;
using LeaveManagement.Interfaces.Repositories;
using LeaveManagement.Persistent;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Models.Repository
{
    public class DepartmentRepository : BaseRepository, IDepartmentRepository
    {

        public DepartmentRepository(AppDbContext context) : base(context)
        {
          
        }

        public void Delete(Department entity)
        {
            _context.Department.Remove(entity);
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            var department = await _context.Department.AsNoTracking().ToListAsync();
            return department;
        }

        public async Task<Department> GetById(int id)
        {
            var getDepartment = await _context.Department.FindAsync(id);
            return getDepartment;
        }

        public async Task InsertAsync(Department entity)
        {
            await _context.Department.AddAsync(entity);
        }

        public void Update(Department entity)
        {
            _context.Department.Update(entity);
        }
    }
}

