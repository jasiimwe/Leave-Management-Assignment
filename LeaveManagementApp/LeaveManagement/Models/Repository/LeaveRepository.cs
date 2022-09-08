using System;
using LeaveManagement.Controllers.Validations;
using LeaveManagement.Interfaces.Repositories;
using LeaveManagement.Persistent;

using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Models.Repository
{
    public class LeaveRepository : BaseRepository, ILeaveRequestRepository
    {
        

        public LeaveRepository(AppDbContext context) : base(context)
        {
           
        }

        public void Delete(LeaveRequest entity)
        {
            _context.LeaveRequest.Remove(entity);
        }

        public async Task<IEnumerable<LeaveRequest>> GetAll() => await _context.LeaveRequest.Include(e => e.EmployeeId).AsNoTracking().ToListAsync();


        public async Task<IEnumerable<LeaveRequest>> GetAllLeaveRequestForEmployee(LeaveRequest leaveRequest) => await _context.LeaveRequest.Where(x => x.EmployeeId == leaveRequest.EmployeeId).ToListAsync();


        public async Task<LeaveRequest> GetById(int id) => await _context.LeaveRequest.Include(e => e.EmployeeId).AsNoTracking().FirstOrDefaultAsync(i => i.LeaveRequestId == id);



        public async Task InsertAsync(LeaveRequest entity)
        {
            await _context.LeaveRequest.AddAsync(entity);
        }

        public void Update(LeaveRequest entity)
        {
            _context.LeaveRequest.Update(entity);
        }
    }
}

