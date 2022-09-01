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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using LeaveManagement.Interfaces.Services;
using LeaveManagement.Interfaces;

namespace LeaveManagement.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestController(ILeaveRequestService leaveRequest, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _leaveRequestService = leaveRequest;
        }

        // GET: LeaveRequest
        public async Task<IActionResult> Index()
        {
            return View(await _leaveRequestService.ListAsync());
        }

        // GET: LeaveRequest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _leaveRequestService.ListById((int)id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            

            return View(leaveRequest);
        }

        // GET: LeaveRequest/Create
        public IActionResult Create()
        {
            PopulateEmployee();
            return View();
        }

        // POST: LeaveRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveRequestId,EmployeeId,LeaveStartDate,LeaveEndDate,ReasonForLeave")] LeaveRequest leaveRequest)
        {

            if (ModelState.IsValid)
            {

                var result = await _leaveRequestService.SaveAsync(leaveRequest);
                if(result.Success)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
                PopulateEmployee();
                return View();


            }
            return View(leaveRequest);
        }

        // GET: LeaveRequest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _leaveRequestService.ListById((int)id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            PopulateEmployee();
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
                var result = await _leaveRequestService.UpdateAsync(id, leaveRequest);
                if (result.Success)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
                PopulateEmployee();
                return View();
            }

            PopulateEmployee();
            return View(leaveRequest);
        }

        // GET: LeaveRequest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequest = await _leaveRequestService.ListById((int)id);
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
            var leaveRequest = await _leaveRequestService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        public async void PopulateEmployee()
        {
            var EmployeeCollection = await _employeeService.ListAsync();
            ViewBag.Employee = EmployeeCollection.Select(item => new SelectListItem
            {
                Text = item.FirstName,
                Value = item.EmployeeId.ToString()
            });
        }


    }
        
}
