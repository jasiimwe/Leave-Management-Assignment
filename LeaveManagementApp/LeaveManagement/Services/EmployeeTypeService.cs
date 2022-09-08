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
            var employeeType = await _unitOfWork.employeeTypeRepository.GetById(id);
            if (employeeType == null)
                return new EmployeeTypeResponse("Employee with ID doesn't exist");

            try
            {
                _unitOfWork.employeeTypeRepository.Delete(employeeType);
                await _unitOfWork.CompleteAsync();

                return new EmployeeTypeResponse("Succesfully Deleted record");
            }
            catch (Exception ex)
            {
                return new EmployeeTypeResponse($"Somehting went wrong: {ex.Message}");
            }


        }

        public async Task<IEnumerable<EmployeeType>> ListAsync()
        {

            return await _unitOfWork.employeeTypeRepository.GetAll();
            
        }

        public async Task<EmployeeType> ListById(int id)
        {
            return await _unitOfWork.employeeTypeRepository.GetById(id);
            
        }

        public async Task<EmployeeTypeResponse> SaveAsync(EmployeeType employeeType)
        {

            try
            {
                await _unitOfWork.employeeTypeRepository.InsertAsync(employeeType);
                await _unitOfWork.CompleteAsync();

                return new EmployeeTypeResponse(employeeType);
            }catch(Exception ex)
            {
                return new EmployeeTypeResponse($"Somehting went wrong: {ex.Message}");
            }
        }

        public async Task<EmployeeTypeResponse> UpdateAsync(int id, EmployeeType employeeType)
        {
            var employee = await _unitOfWork.employeeTypeRepository.GetById(id);
            if (employee == null)
                return new EmployeeTypeResponse("Employee with ID doesn't exist");

            employee.EmployeeTypeName = employeeType.EmployeeTypeName;


            try
            {
                
                await _unitOfWork.CompleteAsync();

                return new EmployeeTypeResponse(employeeType);
            }
            catch (Exception ex)
            {
                return new EmployeeTypeResponse($"Somehting went wrong: {ex.Message}");
            }


        }
    }
}

