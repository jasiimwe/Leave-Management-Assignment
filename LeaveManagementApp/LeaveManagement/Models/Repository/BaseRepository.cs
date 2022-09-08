using System;
using LeaveManagement.Persistent;

namespace LeaveManagement.Models.Repository
{
    public class BaseRepository
    {
        protected readonly AppDbContext _context;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}

