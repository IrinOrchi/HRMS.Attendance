using HRMS.Attendance.AggregrateRoot.Models;
using HRMS.Attendance.DTOs;

public interface IAttendanceService
{
    Task<IEnumerable<EmployeeDto>> GetEmployeeListForMarkAttendance();

    // 2. Get attendance list with InTime
    Task<IEnumerable<AttendanceDto>> GetAttendanceListWithInTime();

    // 3. Get final attendance list with both InTime and OutTime
    Task<IEnumerable<AttendanceDto>> GetFinalAttendanceList();

    // 4. Mark attendance for an employee
    Task MarkAttendance(int employeeId);

    // 5. Mark exit time for an attendance
    Task ExitAttendance(int attendanceId);

    // Check if employee is on leave
    Task<bool> IsEmployeeOnLeave(int employeeId, DateTime date);
}
