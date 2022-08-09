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

namespace LeaveManagement.Controllers
{
    public class EmployeeTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: EmployeeType
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.EmployeeTypeRepository.GetAll());
        }

        // GET: EmployeeType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeType = await _unitOfWork.EmployeeTypeRepository.GetById((int)id);
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
                await _unitOfWork.EmployeeTypeRepository.Insert(employeeType);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
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

            var employeeType = await _unitOfWork.EmployeeTypeRepository.GetById((int)id);
            if (employeeType == null)
            {
                return NotFound();
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

            var employeeType = await _unitOfWork.EmployeeTypeRepository.GetById((int)id);
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
            
            await _unitOfWork.EmployeeTypeRepository.Delete((int)id);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
