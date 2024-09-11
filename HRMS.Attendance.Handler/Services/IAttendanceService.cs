using HRMS.Attendance.AggregrateRoot.Models;
using HRMS.Attendance.DTOs;

public interface IAttendanceService
{
    Task<IEnumerable<AttendanceDto>> GetAllAttendances();
    Task<AttendanceDto> GetAttendanceById(int id);
    Task MarkAttendance(int employeeId);
    Task<bool> IsEmployeeOnLeave(int employeeId, DateTime date);
    Task<IEnumerable<Employee>> GetAllEmployees();
}
