using System;
using LeaveManagement.Communication;
using LeaveManagement.Interfaces;
using LeaveManagement.Interfaces.Services;
using LeaveManagement.Models;

namespace LeaveManagement.Services
{
    public class EmployeeTypeService : IEmployeeTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeTypeResponse> DeleteAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeTypeRepository.GetById(id);
            if (employee == null)
                return new EmployeeTypeResponse("Employee with ID doesn't exist");

            try
            {
                await _unitOfWork.EmployeeTypeRepository.Delete(id);
                await _unitOfWork.SaveAsync();

                return new EmployeeTypeResponse("Succesfully Deleted record");
            }
            catch (Exception ex)
            {
                return new EmployeeTypeResponse($"Somehting went wrong: {ex.Message}");
            }


        }

        public async Task<IEnumerable<EmployeeType>> ListAsync()
        {

            var employeeType = await _unitOfWork.EmployeeTypeRepository.GetAll();
            return employeeType;
        }

        public async Task<EmployeeTypeResponse> ListById(int id)
        {
            var employeeType = await _unitOfWork.EmployeeTypeRepository.GetById(id);
            if (employeeType == null)
                return new EmployeeTypeResponse("Employee with ID doesn't exist");

            return new EmployeeTypeResponse(employeeType);
        }

        public async Task<EmployeeTypeResponse> SaveAsync(EmployeeType employeeType)
        {

            try
            {
                await _unitOfWork.EmployeeTypeRepository.Insert(employeeType);
                await _unitOfWork.SaveAsync();

                return new EmployeeTypeResponse(employeeType);
            }catch(Exception ex)
            {
                return new EmployeeTypeResponse($"Somehting went wrong: {ex.Message}");
            }
        }

        public async Task<EmployeeTypeResponse> UpdateAsync(int id, EmployeeType employeeType)
        {
            var employee = await _unitOfWork.EmployeeTypeRepository.GetById(id);
            if (employee == null)
                return new EmployeeTypeResponse("Employee with ID doesn't exist");


            try
            {
                await _unitOfWork.EmployeeTypeRepository.Update(employeeType);
                await _unitOfWork.SaveAsync();

                return new EmployeeTypeResponse(employeeType);
            }
            catch (Exception ex)
            {
                return new EmployeeTypeResponse($"Somehting went wrong: {ex.Message}");
            }


        }
    }
}

