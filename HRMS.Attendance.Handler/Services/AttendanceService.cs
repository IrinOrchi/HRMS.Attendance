using HRMS.Attendance.AggregrateRoot.Models;
using HRMS.Attendance.DTOs;
using HRMS.Attendance.Repository;
using Microsoft.EntityFrameworkCore;


public class AttendanceService : IAttendanceService
{
    private readonly IGenericRepository<ManageAttendence> _attendanceRepo;
    private readonly IGenericRepository<LeaveRequest> _leaveRequestRepo;
    private readonly IGenericRepository<Employee> _employeeRepo;

    public AttendanceService(IGenericRepository<ManageAttendence> attendanceRepo,
                             IGenericRepository<LeaveRequest> leaveRequestRepo,
                             IGenericRepository<Employee> employeeRepo)
    {
        _attendanceRepo = attendanceRepo;
        _leaveRequestRepo = leaveRequestRepo;
        _employeeRepo = employeeRepo;
    }

    public async Task<IEnumerable<AttendanceDto>> GetAllAttendances()
    {
        var attendances = await _attendanceRepo.GetAll();
        return attendances.Select(a => new AttendanceDto
        {
            ID = a.ID,
            EmployeeID = a.EmployeeID,
            EmployeeName = a.Employee?.Name,
            Department = a.Employee?.Department,
            InTime = a.InTime,
            OutTime = a.OutTime,
            TotalWorkingHours = a.TotalWorkingHours,
            Status = a.Status,
            AttendanceDate = a.AttendanceDate
        }).ToList();
    }

    public async Task<AttendanceDto> GetAttendanceById(int id)
    {
        var attendance = await _attendanceRepo.GetById(id);
        if (attendance != null)
        {
            return new AttendanceDto
            {
                ID = attendance.ID,
                EmployeeID = attendance.EmployeeID,
                EmployeeName = attendance.Employee?.Name,
                Department = attendance.Employee?.Department,
                InTime = attendance.InTime,
                OutTime = attendance.OutTime,
                Status = attendance.Status,
                AttendanceDate = attendance.AttendanceDate,
                TotalWorkingHours = attendance.TotalWorkingHours
            };
        }
        return null;
    }

    public async Task MarkAttendance(int employeeId)
    {
        var currentDate = DateTime.Now;

        var isOnLeave = await IsEmployeeOnLeave(employeeId, currentDate);

        if (!isOnLeave)
        {
            var attendance = new ManageAttendence
            {
                EmployeeID = employeeId,
                InTime = DateTime.Now,
                Status = "Present",
                AttendanceDate = currentDate,
                IsOnLeave = false
            };
            await _attendanceRepo.Add(attendance);
        }
    }

    public async Task<bool> IsEmployeeOnLeave(int employeeId, DateTime date)
    {
        var leaveRequests = await _leaveRequestRepo.GetQueryable()
            .Where(lr => lr.EmployeeID == employeeId &&
                         lr.Status == "Approved" &&
                         lr.StartDate <= date &&
                         lr.EndDate >= date)
            .ToListAsync();

        return leaveRequests.Any();
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        return await _employeeRepo.GetAll();
    }
}
