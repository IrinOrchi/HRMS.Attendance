using HRMS.Attendance.AggregrateRoot.Models;
using HRMS.Attendance.DTOs;
using HRMS.Attendance.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class AttendanceService : IAttendanceService
{
    private readonly IGenericRepository<ManageAttendance> _attendanceRepo;
    private readonly IGenericRepository<Employee> _employeeRepo;
    private readonly IGenericRepository<LeaveRequest> _leaveRequestRepo;

    public AttendanceService(IGenericRepository<ManageAttendance> attendanceRepo,
                             IGenericRepository<Employee> employeeRepo,
                             IGenericRepository<LeaveRequest> leaveRequestRepo)
    {
        _attendanceRepo = attendanceRepo;
        _employeeRepo = employeeRepo;
        _leaveRequestRepo = leaveRequestRepo;
    }

    // 1. Get employee list for marking attendance
    public async Task<IEnumerable<EmployeeDto>> GetEmployeeListForMarkAttendance()
    {
        var employees = await _employeeRepo.GetAll();

        return employees.Select(e => e.ToDto());
    }

    // 2. Get attendance list with InTime
    public async Task<IEnumerable<AttendanceDto>> GetAttendanceListWithInTime()
    {
        var attendances = await _attendanceRepo.GetQueryable()
            .Where(a => a.InTime != null)
            .Include(a => a.Employee) // Include the related Employee entity
            .ToListAsync();

        return attendances.Select(a => a.ToDto());
    }

    // 3. Get final attendance list with both InTime and OutTime
    public async Task<IEnumerable<AttendanceDto>> GetFinalAttendanceList()
    {
        var attendances = await _attendanceRepo.GetQueryable()
            .Include(a => a.Employee) // Ensure Employee details are included
            .ToListAsync();

        return attendances.Select(a => new AttendanceDto
        {
            ID = a.ID,
            EmployeeName = a.Employee.Name, // Map the employee name
            Department = a.Employee.Department, // Map the department
            InTime = a.InTime,
            OutTime = a.OutTime,
            TotalWorkingHours = a.TotalWorkingHours,
            Status = a.Status
        });
    }

    // 4. Mark attendance for an employee
    public async Task MarkAttendance(int employeeId)
    {
        var employee = await _employeeRepo.GetById(employeeId);
        var isOnLeave = await IsEmployeeOnLeave(employeeId, DateTime.Now);

        if (!isOnLeave)
        {
            var attendance = ManageAttendance.CreateAttendance(employee);
            await _attendanceRepo.Add(attendance);
        }
    }

    // 5. Mark exit time for an attendance
    public async Task ExitAttendance(int attendanceId)
    {
        var attendance = await _attendanceRepo.GetById(attendanceId);
        if (attendance != null)
        {
            attendance.OutTime = DateTime.Now;
            await _attendanceRepo.Update(attendance);
        }
    }

    // Check if employee is on leave
    public async Task<bool> IsEmployeeOnLeave(int employeeId, DateTime date)
    {
        var leaveRequests = await _leaveRequestRepo.GetQueryable()
            .Where(lr => lr.EmployeeID == employeeId && lr.Status == "Approved" &&
                         lr.StartDate <= date && lr.EndDate >= date)
            .ToListAsync();

        return leaveRequests.Any();
    }
}
