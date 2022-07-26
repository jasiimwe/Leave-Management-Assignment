﻿using System;
using LeaveManagement.Communication;
using LeaveManagement.Controllers.Validations;
using LeaveManagement.Interfaces;
using LeaveManagement.Interfaces.Services;
using LeaveManagement.Models;
using LeaveManagement.Models.Repository;
using LeaveManagement.Persistent;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        Messages message = new Messages();
        

        public LeaveRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;  
        }



        
        #region Validation Rules

        //StartDate and EndDate comparison
        protected bool StartDateNotLessThanEndDate(LeaveRequest leaveRequest)
        {
            if (leaveRequest.LeaveStartDate > leaveRequest.LeaveEndDate)
                return false;
            return true;
            
        }

        //Lookup overlapping dates
        protected async Task<bool> LeaveRequestHasOverlapAsync(LeaveRequest leaveRequest)
        {
            var getLeaveRequests = await _unitOfWork.leaveRequestRepository.GetAllLeaveRequestForEmployee(leaveRequest);
            foreach(var c in getLeaveRequests)
            {
                var isOverlap = LeaveRequestValidation.HasOverlap(c.LeaveStartDate, c.LeaveEndDate, leaveRequest.LeaveStartDate, leaveRequest.LeaveEndDate);
                if (isOverlap)
                    return false;   
            }
            return true;
            
        }

        //Lookup overlapping dates within Department
        protected async Task<bool> LeaveRequestHasOverlapInDepartmentAsync(LeaveRequest leaveRequest)
        {
            var getLeaveRequests = await _unitOfWork.leaveRequestRepository.GetAll();
            
            var getEmployee = await _unitOfWork.employeeRepository.GetById(leaveRequest.EmployeeId);

            foreach(var c in getLeaveRequests.Where(x=>x.Employee.Department == getEmployee.Department))
            {
                var isOverlap = LeaveRequestValidation.HasOverlap(c.LeaveStartDate, c.LeaveEndDate, leaveRequest.LeaveStartDate, leaveRequest.LeaveEndDate);
                if (isOverlap)
                    return false;
            }

            return true;

        }

        //Lookup last leave request to be less than 30 days
        protected async Task<bool> CheckLastLeaveLessThanThirtyDays(LeaveRequest leaveRequest)
        {
            var getLeaveRequests = await _unitOfWork.leaveRequestRepository.GetAllLeaveRequestForEmployee(leaveRequest);

            var lastDate = getLeaveRequests.LastOrDefault();

            if(getLeaveRequests.LastOrDefault() != null)
            {
                if (LeaveRequestValidation.IsLessThanMonth(leaveRequest.LeaveStartDate, lastDate.LeaveEndDate))
                    return false;
            }
            return true;
        }


        //validation managers take leave for 30 and others 21 days
        protected async Task<bool> CheckLeaveDays(LeaveRequest leaveRequest)
        {
            var requestLeaveDays = LeaveRequestValidation.NumberOfLeaveDaysExcludingWeekends(leaveRequest.LeaveStartDate, leaveRequest.LeaveEndDate);
            var getEmployee = await _unitOfWork.employeeRepository.GetById(leaveRequest.EmployeeId);
            if(getEmployee.EmployeeType.EmployeeTypeName == "Manager")
            {
                if (requestLeaveDays > 30) return false;
            }
            else
            {
                if (requestLeaveDays > 20) return false;
            }

            return true;
        }
        #endregion

        
        #region Service Implementation
        public async Task<LeaveRequestResponse> DeleteAsync(int id)
        {
            var existingLeaveRequest = await _unitOfWork.leaveRequestRepository.GetById(id);
            if (existingLeaveRequest == null)
                return new LeaveRequestResponse("Leave Request doesn't exist");
            

            try
            {
                _unitOfWork.leaveRequestRepository.Delete(existingLeaveRequest);
                await _unitOfWork.CompleteAsync();

                return new LeaveRequestResponse("Successfully deleted Leave Request");
            }
            catch (Exception ex)
            {
                return new LeaveRequestResponse($"Issue updating Leave Request: {ex.Message}");
            }
        }

        public async Task<IEnumerable<LeaveRequest>> ListAsync()
        {
            return await _unitOfWork.leaveRequestRepository.GetAll();
            
        }

        public async Task<LeaveRequest> ListById(int id)
        {
            return await _unitOfWork.leaveRequestRepository.GetById(id);
            

        }

        public async Task<LeaveRequestResponse> SaveAsync(LeaveRequest leaveRequest)
        {
            

            if (!StartDateNotLessThanEndDate(leaveRequest))
                return new LeaveRequestResponse(message.startDateNotLessThanEndDateErrorMessage);

            if (!await LeaveRequestHasOverlapAsync(leaveRequest))
                return new LeaveRequestResponse(message.leaveRequestHasOverlapAsyncErrorMessagae);

            if (!await LeaveRequestHasOverlapInDepartmentAsync(leaveRequest))
                return new LeaveRequestResponse(message.leaveRequestHasOverlapInDepartmentAsyncErrorMessage);

            if (!await CheckLastLeaveLessThanThirtyDays(leaveRequest))
                return new LeaveRequestResponse(message.checkLastLeaveLessThanThirtyDaysErorrMessage);

            if (!await CheckLeaveDays(leaveRequest))
                return new LeaveRequestResponse(message.checkLeaveDaysErrorMessage);

            

            try
            {
                await _unitOfWork.leaveRequestRepository.InsertAsync(leaveRequest);
                await _unitOfWork.CompleteAsync();

                return new LeaveRequestResponse(leaveRequest);
            }catch(Exception ex)
            {
                return new LeaveRequestResponse($"Can't create Leave Request: {ex.Message}");
            }
        }

        public async Task<LeaveRequestResponse> UpdateAsync(int id, LeaveRequest leaveRequest)
        {
            var existingLeaveRequest = await _unitOfWork.leaveRequestRepository.GetById(id);
            if (existingLeaveRequest == null)
                return new LeaveRequestResponse("Leave Request doesn't exist");


            existingLeaveRequest.EmployeeId = leaveRequest.EmployeeId;
            existingLeaveRequest.LeaveStartDate = leaveRequest.LeaveStartDate;
            existingLeaveRequest.LeaveEndDate = leaveRequest.LeaveEndDate;
            existingLeaveRequest.ReasonForLeave = leaveRequest.ReasonForLeave;

            try
            {
                
                await _unitOfWork.CompleteAsync();

                return new LeaveRequestResponse(existingLeaveRequest);
            }catch(Exception ex)
            {
                return new LeaveRequestResponse($"Issue updating Leave Request: {ex.Message}");
            }
        }

        #endregion
    }
}

