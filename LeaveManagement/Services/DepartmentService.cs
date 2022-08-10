using System;
using LeaveManagement.Communication;
using LeaveManagement.Interfaces;
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
            var getDepartment = await _unitOfWork.DepartmentRepositoty.GetById(id);
            if (getDepartment == null)
                return new DepartmentResponse("Department doesn't exist");

            try
            {
                await _unitOfWork.DepartmentRepositoty.Delete(id);
                await _unitOfWork.SaveAsync();

                return new DepartmentResponse("succesfully deleted department");
            }catch(Exception ex)
            {
                return new DepartmentResponse($"Something went wrong: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Department>> ListAsync()
        {
            var department = await _unitOfWork.DepartmentRepositoty.GetAll();
            return department;
        }

        public async Task<Department> ListById(int id)
        {

            var department = await _unitOfWork.DepartmentRepositoty.GetById(id);
            return department;
        }

        public async Task<DepartmentResponse> SaveAsync(Department department)
        {
            var getDepartment = await _unitOfWork.DepartmentRepositoty.GetById(department.DepartmentId);
            if (getDepartment != null)
                return new DepartmentResponse("Department already exists");

            try
            {
                await _unitOfWork.DepartmentRepositoty.Insert(department);
                await _unitOfWork.SaveAsync();

                return new DepartmentResponse(department);
            }catch(Exception ex)
            {
                return new DepartmentResponse($"Something went wrong: {ex.Message}");
            }
        }

        public async Task<DepartmentResponse> UpdateAsync(int id, Department department)
        {
            var getDepartment = await _unitOfWork.DepartmentRepositoty.GetById(id);
            if (getDepartment == null)
                return new DepartmentResponse("Department doesn't exist");

            try
            {
                await _unitOfWork.DepartmentRepositoty.Update(department);
                await _unitOfWork.SaveAsync();

                return new DepartmentResponse(department);
            }catch(Exception ex)
            {
                return new DepartmentResponse($"Something went wrong: {ex.Message}");
            }
        }
    }
}

