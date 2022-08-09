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
        //private readonly IDepartmentService _departmentService;
        //private readonly IEmployeeTypeService _employeeTypeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Employee, int> _employeeRepository;
        private readonly ModelStateDictionary _modelStateDictionery;

        

        public EmployeeController(IEmployeeService employeeService, IUnitOfWork unitOfWork)
        {

            _employeeService = employeeService;
            _unitOfWork = unitOfWork;
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

            var employee = await _unitOfWork.EmployeeRepository.GetById((int)id);
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
            var DepartmentCollection = await _unitOfWork.DepartmentRepositoty.GetAll();
            ViewBag.Department = DepartmentCollection.Select(item => new SelectListItem { Text = item.DepartmentName,
            Value = item.DepartmentId.ToString() });
        }

        [NonAction]
        public async void PopulateEmployeeType()
        {
            var employeeType = await _unitOfWork.EmployeeTypeRepository.GetAll();
            ViewBag.EmployeeType = employeeType.Select(item => new SelectListItem { Text = item.EmployeeTypeName,
                Value = item.EmployeeTypeId.ToString()});
        }
    }
}
