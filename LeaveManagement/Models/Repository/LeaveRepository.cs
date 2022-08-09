using System;
using LeaveManagement.Controllers.Validations;
using LeaveManagement.Persistent;
using LeaveManagement.Utils;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Models.Repository
{
    public class LeaveRepository : ILeaveRepository
    {
        private readonly AppDbContext _context;

        public LeaveRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var leaveRequest = await _context.LeaveRequest.FirstOrDefaultAsync(e => e.LeaveRequestId == id);
            if (leaveRequest != null)
            {
                _context.Remove(leaveRequest);
            }
        }

        public async Task<IEnumerable<LeaveRequest>> GetAll() => await _context.LeaveRequest.Include(e => e.Employee).ToListAsync();
        

        public async Task<IEnumerable<LeaveRequest>> GetAllLeaveRequestForEmployee(LeaveRequest leaveRequest) => await _context.LeaveRequest.Where(x => x.EmployeeId == leaveRequest.EmployeeId).ToListAsync();
       
        public async Task<LeaveRequest> GetById(int id)
        {
            return await _context.LeaveRequest.FindAsync(id);
        }

        public async Task<LeaveRequest> Insert(LeaveRequest entity)
        {
            await _context.LeaveRequest.AddAsync(entity);
            return entity; ;
        }

        public async Task Update(LeaveRequest entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

       

    }
}

