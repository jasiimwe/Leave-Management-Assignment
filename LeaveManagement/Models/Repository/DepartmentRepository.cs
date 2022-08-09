using System;
using LeaveManagement.Persistent;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Models.Repository
{
    public class DepartmentRepository : IRepository<Department, int>
    {
        private readonly AppDbContext context;

        public DepartmentRepository(AppDbContext context) => this.context = context;




        public async Task<IEnumerable<Department>> GetAll() => await context.Department.ToListAsync();

        public async Task<Department> GetById(int id) => await context.Department.FindAsync(id);
        

        public async Task<Department> Insert(Department entity)
        {
            await context.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            var department = await context.Department.FirstOrDefaultAsync(b => b.DepartmentId == id);
            if(department != null)
            {
                context.Remove(department);
            }
        }

        public async Task Update(Department entity)
        {
            context.Entry(entity).State = EntityState.Modified;

        }




    }
}

