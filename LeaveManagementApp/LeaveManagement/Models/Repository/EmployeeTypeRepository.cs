using System;
using LeaveManagement.Interfaces.Repositories;
using LeaveManagement.Persistent;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Models.Repository
{
    public class EmployeeTypeRepository : BaseRepository, IEmployeeTypeRepository
    {

        public EmployeeTypeRepository(AppDbContext context):base(context)
        {

        }
        public void Delete(EmployeeType entity)
        {
            _context.EmployeeType.Remove(entity);
        }

        public async Task<IEnumerable<EmployeeType>> GetAll()
        {
            return await _context.EmployeeType.AsNoTracking().ToListAsync();
        }

        public async Task<EmployeeType> GetById(int id)
        {
            return await _context.EmployeeType.FindAsync(id);
        }

        public async Task InsertAsync(EmployeeType entity)
        {
            await _context.EmployeeType.AddAsync(entity);
        }

        public void Update(EmployeeType entity)
        {
            _context.EmployeeType.Update(entity);
        }
    }

}

