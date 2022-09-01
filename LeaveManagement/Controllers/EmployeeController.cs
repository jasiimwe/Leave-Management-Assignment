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
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LeaveManagement.Controllers
{
    public class EmployeeController : Controller
    {
        
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeTypeService _employeeTypeService;
        
        
        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, IEmployeeTypeService employeeTypeService)
        {

            _employeeService = employeeService;
            _departmentService = departmentService;
            _employeeTypeService = employeeTypeService;
            
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {

            return View(await _employeeService.ListAsync());
            
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.ListById((int)id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {

            PopulateDepartment();
            PopulateEmployeeType();
            
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,DepartmentId,DateOfBirth,EmployeeTypeId")] Employee employee)
        {
             if (ModelState.IsValid)
             {
                var result = await _employeeService.SaveAsync(employee);
                if (result.Success)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", result.Message);
                PopulateDepartment();
                PopulateEmployeeType();
                return View();
                
            }
            return View(employee);
        }


        // GET: Employee/Edit
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var employeeRequest = await _employeeService.ListById((int)id);
            if (employeeRequest == null)
            {
                return NotFound();
            }
            PopulateDepartment();
            PopulateEmployeeType();
            return View(employeeRequest);
        }


        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,DepartmentId,DateOfBirth,EmployeeTypeId")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _employeeService.UpdateAsync(id, employee);
                if (result.Success)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", result.Message);
                PopulateDepartment();
                PopulateEmployeeType();
                return View();

            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeService.ListById((int)id);
            if(employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        

        [NonAction]
        public async void PopulateDepartment()
        {
            var DepartmentCollection = await _departmentService.ListAsync();
            ViewBag.Department = DepartmentCollection.Select(item => new SelectListItem { Text = item.DepartmentName,
            Value = item.DepartmentId.ToString() });
        }

        [NonAction]
        public async void PopulateEmployeeType()
        {
            var employeeType = await _employeeTypeService.ListAsync();
            ViewBag.EmployeeType = employeeType.Select(item => new SelectListItem { Text = item.EmployeeTypeName,
                Value = item.EmployeeTypeId.ToString()});
        }
    }
}
