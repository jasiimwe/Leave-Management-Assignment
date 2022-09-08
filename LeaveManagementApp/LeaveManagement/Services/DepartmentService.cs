using System;
using LeaveManagement.Communication;
using LeaveManagement.Interfaces;
using LeaveManagement.Interfaces.Repositories;
using LeaveManagement.Interfaces.Services;
using LeaveManagement.Models;

namespace LeaveManagement.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DepartmentResponse> DeleteAsync(int id)
        {
            var getDepartment = await _unitOfWork.departmentRepositoty.GetById(id);
            if (getDepartment == null)
                return new DepartmentResponse("Department doesn't exist");

            try
            {
                _unitOfWork.departmentRepositoty.Delete(getDepartment);
                await _unitOfWork.CompleteAsync();

                return new DepartmentResponse("succesfully deleted department");
            }catch(Exception ex)
            {
                return new DepartmentResponse($"Something went wrong: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Department>> ListAsync()
        {
            return await _unitOfWork.departmentRepositoty.GetAll();
            
        }

        public async Task<Department> ListById(int id)
        {

            var department = await _unitOfWork.departmentRepositoty.GetById(id);
            return department;
        }

        public async Task<DepartmentResponse> SaveAsync(Department department)
        {
            

            try
            {
                await _unitOfWork.departmentRepositoty.InsertAsync(department);
                await _unitOfWork.CompleteAsync();

                return new DepartmentResponse(department);
            }catch(Exception ex)
            {
                return new DepartmentResponse($"Something went wrong: {ex.Message}");
            }
        }

        public async Task<DepartmentResponse> UpdateAsync(int id, Department department)
        {
            var getDepartment = await _unitOfWork.departmentRepositoty.GetById(id);
            if (getDepartment == null)
                return new DepartmentResponse("Department doesn't exist");

            getDepartment.DepartmentName = department.DepartmentName;

            try
            {
                
                await _unitOfWork.CompleteAsync();

                return new DepartmentResponse(department);
            }catch(Exception ex)
            {
                return new DepartmentResponse($"Something went wrong: {ex.Message}");
            }
        }
    }
}

