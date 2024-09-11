using HRMS.Attendance.AggregrateRoot.Models;
using HRMS.Attendance.DTOs;
using HRMS.Attendance.Handler.Services;
using HRMS.Attendance.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class LeaveRequestService : ILeaveRequestService
{
    private readonly IGenericRepository<LeaveRequest> _leaveRequestRepo;

    public LeaveRequestService(IGenericRepository<LeaveRequest> leaveRequestRepo)
    {
        _leaveRequestRepo = leaveRequestRepo;
    }

    public async Task<IEnumerable<LeaveRequestDto>> GetAllPendingRequests()
    {
        var pendingRequests = await _leaveRequestRepo.GetQueryable()
            .Include(r => r.Employee) // Load Employee data
            .Where(r => r.Status == "Pending")
            .ToListAsync();

        // Manual mapping
        var pendingRequestDtos = pendingRequests.Select(r => new LeaveRequestDto
        {
            ID = r.ID,
            EmployeeID = r.EmployeeID,
            EmployeeName = r.Employee?.Name ?? string.Empty,
            EmployeeDepartment = r.Employee?.Department ?? string.Empty,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            Reason = r.Reason,
            Status = r.Status
        });

        return pendingRequestDtos;
    }

    public async Task<IEnumerable<LeaveRequestDto>> GetAllApprovedRequests()
    {
        var approvedRequests = await _leaveRequestRepo.GetQueryable()
            .Include(r => r.Employee) // Load Employee data
            .Where(r => r.Status == "Approved")
            .ToListAsync();

        // Manual mapping
        var approvedRequestDtos = approvedRequests.Select(r => new LeaveRequestDto
        {
            ID = r.ID,
            EmployeeID = r.EmployeeID,
            EmployeeName = r.Employee?.Name ?? string.Empty,
            EmployeeDepartment = r.Employee?.Department ?? string.Empty,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            Reason = r.Reason,
            Status = r.Status
        });

        return approvedRequestDtos;
    }

    public async Task ApproveLeaveRequest(int id)
    {
        var leaveRequest = await _leaveRequestRepo.GetById(id);
        if (leaveRequest != null)
        {
            leaveRequest.Status = "Approved";
            await _leaveRequestRepo.Update(leaveRequest);
        }
    }

    public async Task DeleteLeaveRequest(int id)
    {
        await _leaveRequestRepo.Delete(id);
    }

    public async Task UpdateApprovedLeaveRequest(LeaveRequestDto dto)
    {
        var leaveRequest = await _leaveRequestRepo.GetById(dto.ID);
        if (leaveRequest != null)
        {
            leaveRequest.StartDate = dto.StartDate;
            leaveRequest.EndDate = dto.EndDate;
            leaveRequest.Reason = dto.Reason;
            await _leaveRequestRepo.Update(leaveRequest);
        }
    }

    public async Task<LeaveRequestDto> GetById(int id)
    {
        var leaveRequest = await _leaveRequestRepo.GetQueryable()
            .Include(r => r.Employee) // Load Employee data
            .FirstOrDefaultAsync(r => r.ID == id);

        if (leaveRequest == null)
        {
            return null; // or throw an exception based on your preference
        }

        // Manual mapping
        return new LeaveRequestDto
        {
            ID = leaveRequest.ID,
            EmployeeID = leaveRequest.EmployeeID,
            EmployeeName = leaveRequest.Employee?.Name ?? string.Empty,
            EmployeeDepartment = leaveRequest.Employee?.Department ?? string.Empty,
            StartDate = leaveRequest.StartDate,
            EndDate = leaveRequest.EndDate,
            Reason = leaveRequest.Reason,
            Status = leaveRequest.Status
        };
    }
}
