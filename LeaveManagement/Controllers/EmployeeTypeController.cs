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
using LeaveManagement.Models.Repository;
using LeaveManagement.Interfaces;
using LeaveManagement.Interfaces.Services;
using LeaveManagement.Services;

namespace LeaveManagement.Controllers
{
    public class EmployeeTypeController : Controller
    {
        private readonly IEmployeeTypeService _employeeTypeService;

        public EmployeeTypeController(IEmployeeTypeService employeeTypeService)
        {
            _employeeTypeService = employeeTypeService;
        }

        // GET: EmployeeType
        public async Task<IActionResult> Index()
        {
            return View(await _employeeTypeService.ListAsync());
        }

        // GET: EmployeeType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeType = await _employeeTypeService.ListById((int)id);
            if (employeeType == null)
            {
                return NotFound();
            }

            return View(employeeType);
        }

        // GET: EmployeeType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeTypeId,EmployeeTypeName")] EmployeeType employeeType)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeTypeService.SaveAsync(employeeType);
                if (result.Success)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", result.Message);
                return View();
            }
            return View(employeeType);
        }

        // GET: EmployeeType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeType = await _employeeTypeService.ListById((int)id);
            if (employeeType == null)
            {
                return NotFound();
            }
            return View(employeeType);
        }

        // POST: EmployeeType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeTypeId, EmployeeTypeName")] EmployeeType employeeType)
        {
            if (id != employeeType.EmployeeTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _employeeTypeService.UpdateAsync(id, employeeType);
                if (result.Success)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", result.Message);
                return View();

            }
            return View(employeeType);
        }


        // GET: EmployeeType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeType = await _employeeTypeService.ListById((int)id);
            if (employeeType == null)
            {
                return NotFound();
            }

            return View(employeeType);
        }

        // POST: EmployeeType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            await _employeeTypeService.DeleteAsync((int)id);
           
            return RedirectToAction(nameof(Index));
        }

        
    }
}
