using System;
namespace LeaveManagement.Communication
{
    public class Messages
    {
        public String startDateNotLessThanEndDateErrorMessage = "Start Date can't be greater than end date";
        public String leaveRequestHasOverlapAsyncErrorMessagae = "Leave Request has Overlapping Dates";
        public String leaveRequestHasOverlapInDepartmentAsyncErrorMessage = "Your Leave Request Overlaps with another member in your Department";
        public String checkLastLeaveLessThanThirtyDaysErorrMessage = "You can't make another leave request in the given period";
        public String checkLeaveDaysErrorMessage = "Please Revise the period for your leave Request";
        
    }
}

