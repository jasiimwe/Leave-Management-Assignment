#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagement.Models;
using LeaveManagement.Persistent;
using LeaveManagement.Controllers.Validations;

namespace LeaveManagement.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly AppDbContext _context;

        public LeaveRequestController(AppDbContext context)
        {
            _context = context;
        }

        // GET: LeaveRequest
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.LeaveRequest.Include(l => l.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: LeaveRequest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequest
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LeaveRequestId == id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            

            return View(leaveRequest);
        }

        // GET: LeaveRequest/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName");
            return View();
        }

        // POST: LeaveRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveRequestId,EmployeeId,LeaveStartDate,LeaveEndDate,ReasonForLeave")] LeaveRequest leaveRequest)
        {

            #region Leave Request Rules

            var getEmployeeLeave = _context.LeaveRequest.Where(x => x.EmployeeId == leaveRequest.EmployeeId).ToList();
            var getEmployee = _context.Employee.FirstOrDefault(x => x.EmployeeId == leaveRequest.EmployeeId);
            
            var startDate = Convert.ToDateTime(leaveRequest.LeaveStartDate);
            var endDate = Convert.ToDateTime(leaveRequest.LeaveEndDate);
            var managerLeaveDays = 30;
            var otherStaffLeaveDays = 20;
            var daysRequested = (int)(endDate - startDate).TotalDays;

            //StartDate and EndDate comparison

            if(LeaveRequestValidation.IsDateGreaterThan(startDate, endDate))
            {
                ModelState.AddModelError("", "Leave Start date cannot be greater than the leave end date");
                ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", leaveRequest.EmployeeId);
                return View(leaveRequest);
            }

            //weekend validation
            if((startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday) || (endDate.DayOfWeek == DayOfWeek.Saturday || endDate.DayOfWeek == DayOfWeek.Sunday))
            {
                ModelState.AddModelError("", "Leave Start date and end Date can't fall on Weekends");
                ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", leaveRequest.EmployeeId);
                return View(leaveRequest);
            }

            

            //Lookup overlapping dates
            foreach( var c in getEmployeeLeave)
            {

                var isOverlap = LeaveRequestValidation.HasOverlap(c.LeaveStartDate, c.LeaveEndDate, startDate, endDate);

                if (isOverlap)
                {
                    ModelState.AddModelError("", "Leave dates are overlapping. please select other dates");
                    ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", leaveRequest.EmployeeId);
                    return View(leaveRequest);
                }

                
            }

            //Lookup overlapping dates with other employers in othe departments
            var getAllLeaveRequests = _context.LeaveRequest.Where(x=>x.Employee.Department == getEmployee.Department);
            foreach(var f in getAllLeaveRequests)
            {
                var isOverlap =  LeaveRequestValidation.HasOverlap(f.LeaveStartDate, f.LeaveEndDate, startDate, endDate);

                if (isOverlap)
                {
                    ModelState.AddModelError("", "Leave dates are overlapping with other people in the department. please select other dates");
                    ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", leaveRequest.EmployeeId);
                    return View(leaveRequest);
                }
                
            }




            //Lookup last leave request to be less than 30 days
            var getLastEmployeeLeave = getEmployeeLeave.LastOrDefault();
            if(getLastEmployeeLeave != null)
            {
                if (LeaveRequestValidation.IsLessThanMonth(getLastEmployeeLeave.LeaveEndDate, startDate))
                {
                    ModelState.AddModelError("", "You can't make another Leave request");
                    ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", leaveRequest.EmployeeId);
                    return View(leaveRequest);
                }
                
            }
            
            //validation managers take leave for 30 and others 21 days
            if(getEmployee.EmployeeType == Employee.EmployeeTypeChoices.Managers)
            {
                if(daysRequested > managerLeaveDays)
                {
                    ModelState.AddModelError("", "Leave request denied becuase you are over the limit (30 days for a manager)");
                    ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", leaveRequest.EmployeeId);
                    return View(leaveRequest);
                }
            }
            else
            {
                if (daysRequested > otherStaffLeaveDays)
                {
                    ModelState.AddModelError("", "Leave request denied becuase you are over the limit (21 days for other staff)");
                    ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", leaveRequest.EmployeeId);
                    return View(leaveRequest);
                }
            }



            #endregion


            if (ModelState.IsValid)
            {
                
                _context.LeaveRequest.Add(leaveRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FirstName", leaveRequest.EmployeeId);
            return View(leaveRequest);
        }

        // GET: LeaveRequest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequest.FindAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Department", leaveRequest.EmployeeId);
            return View(leaveRequest);
        }

        // POST: LeaveRequest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveRequestId,EmployeeId,LeaveStartDate,LeaveEndDate,ReasonForLeave")] LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.LeaveRequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveRequestExists(leaveRequest.LeaveRequestId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Department", leaveRequest.EmployeeId);
            return View(leaveRequest);
        }

        // GET: LeaveRequest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequest
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.LeaveRequestId == id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        // POST: LeaveRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveRequest = await _context.LeaveRequest.FindAsync(id);
            _context.LeaveRequest.Remove(leaveRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveRequestExists(int id)
        {
            return _context.LeaveRequest.Any(e => e.LeaveRequestId == id);
        }

        

    }
}
