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
    public class DepartmentController : Controller
    {
        //private readonly AppDbContext _context;
        //private readonly IRepository<Department, int> _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.DepartmentRepositoty.GetAll());
        }

        

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.DepartmentRepositoty.Insert(department);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

       

        
        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _unitOfWork.DepartmentRepositoty.GetById((int)id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _unitOfWork.DepartmentRepositoty.Delete(id);


            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
