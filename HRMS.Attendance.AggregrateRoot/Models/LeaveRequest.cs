using HRMS.Attendance.DTOs;

public class LeaveRequest
{
    public int ID { get; set; }
    public int EmployeeID { get; set; }
    public virtual Employee? Employee { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required string Reason { get; set; }
    public required string Status { get; set; }

    // DTO Mapping methods
    public static LeaveRequestDto ToDto(LeaveRequest leaveRequest)
    {
        return new LeaveRequestDto
        {
            ID = leaveRequest.ID,
            EmployeeID = leaveRequest.EmployeeID,
            StartDate = leaveRequest.StartDate,
            EndDate = leaveRequest.EndDate,
            Reason = leaveRequest.Reason,
            Status = leaveRequest.Status,
            EmployeeName = leaveRequest.Employee?.Name ?? string.Empty,
            EmployeeDepartment = leaveRequest.Employee?.Department ?? string.Empty,
        };
    }

    public static LeaveRequest FromDto(LeaveRequestDto dto)
    {
        return new LeaveRequest
        {
            ID = dto.ID,
            EmployeeID = dto.EmployeeID,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Reason = dto.Reason,
            Status = dto.Status
        };
    }
}
