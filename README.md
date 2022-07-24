# Leave-Management-Assignment
This is Leave management system that enables an employee register their details and apply for leave
# Overview of Leave Management System
1. Developed in Visual Studio Preview macos
2. Developed Using Asp.Net Core 6 and c#
3. It uses Entity Framework Core for handling storage and related operations.
4. Applies code-first and dependency injection
5. Uses the MVC framework pattern
# How to Install Locally
1. Clone Repo to a specified folder on your computer
2. Open project in visual studio or visual studio code(make sure to have installed c# extension and dotnet)
3. Navigate to appsettings.json and update the DefaultConnect i.e. this project uses sqlite database
4. Open you Package Console Manager and run migrations "Add-Migration {InitialCreate}" or on your command line navigate to the project main folder and run "dotnet ef migrations add {MigrationName}"
4. Open you Package Console Manager and run "Update-Database" or on your command line navigate to the project main folder and run "dotnet ef database update" to update your database
5. Run the project
6. viola!!!
# Assumptions to the assignment
1. A one to many relationship between employee and leave request entities. i.e. An Employee can make multiple Leave Requests and a Leave Request can only be attached to one Employee
2. A user can create an Employee using the provided properties.
3. A Leave Request can be created with special considerations as follows;<br/>

  a)  an employee can't have leave requests that are overlapping; the assumption is that an employee can submit multiple leave requests without considering whether<br/>
      the previous leave request(s) were completed, therfore the sytem perfoms a check to see if the current leave request doesn't conflict with the alreaady existing<br/>
      leave requests by checking if the start date of the previous leave request is less than the end date of current leave request and the end date of the previous <br/>
      request is greater than the start date of the current leave requests. <br/>
      
  b)  An employee should not be allowed to make a leave request that overlaps with another employee of the same department; this takes the same logic as the previous rule<br />
      however, this time, the system fetches all employess and gets the corresponding departments and then compares this against current employee making a leave request,<br />
      if this is true it continues to execute the overlapping logic as described above. <br>
      
  c)   An employee should not be allowed to make a leave request if the end of their previous leave request is less than one month; the assumption is that the leave last leave request is completed <br />
       therefore, the system then checks the last leave request end date and subtracts the start date of the current leave request to get the number days which should be more than a month(30)<br>
  d)  Managers can take up to 30 days while all other staff can take up to 21 days; this assumes that employess have been categorised into manager and other staff. The system checks for the employee type<br />
      and for either type the system calculates the number of days between the start date and end date of leave request and compares against the specified days.<br />
  e). Weekends should not be included when applying for leave. the assumption is the leave request start and end date can't fall on weekends. The system checks the leave request start and dates and to see of any of them fall on the weekends. <br />
  4. The system lists all leave requests and provides an option to export the leave requests to excel. this is achieved using the jquery and bootstrap tables.
  



      
      

