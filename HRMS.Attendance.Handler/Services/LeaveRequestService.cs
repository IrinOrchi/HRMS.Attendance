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
            .Include(r => r.Employee)
            .Where(r => r.Status == "Pending")
            .ToListAsync();

        return pendingRequests.Select(LeaveRequest.ToDto);
    }

    public async Task<IEnumerable<LeaveRequestDto>> GetAllApprovedRequests()
    {
        var approvedRequests = await _leaveRequestRepo.GetQueryable()
            .Include(r => r.Employee)
            .Where(r => r.Status == "Approved")
            .ToListAsync();

        return approvedRequests.Select(LeaveRequest.ToDto);
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
            .Include(r => r.Employee)
            .FirstOrDefaultAsync(r => r.ID == id);

        if (leaveRequest == null)
        {
            throw new InvalidOperationException("Leave request not found");
        }
        return LeaveRequest.ToDto(leaveRequest);
    }
}
