using HRMS.Attendance.DTOs;
using HRMS.Attendance.Handler.Services;
using Microsoft.AspNetCore.Mvc;

public class LeaveRequestController : Controller
{
    private readonly ILeaveRequestService _leaveRequestService;

    public LeaveRequestController(ILeaveRequestService leaveRequestService)
    {
        _leaveRequestService = leaveRequestService;
    }

    public async Task<IActionResult> Index()
    {
        var pendingRequests = await _leaveRequestService.GetAllPendingRequests();
        return View(pendingRequests);
    }

    public async Task<IActionResult> ApprovedList()
    {
        var approvedRequests = await _leaveRequestService.GetAllApprovedRequests();
        return View(approvedRequests);
    }

    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
        await _leaveRequestService.ApproveLeaveRequest(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _leaveRequestService.DeleteLeaveRequest(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var leaveRequestDto = await _leaveRequestService.GetById(id);
        if (leaveRequestDto == null)
        {
            return NotFound();
        }
        return View(leaveRequestDto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(LeaveRequestDto leaveRequestDto)
    {
        if (ModelState.IsValid)
        {
            await _leaveRequestService.UpdateApprovedLeaveRequest(leaveRequestDto);
            return RedirectToAction(nameof(ApprovedList));
        }
        return View(leaveRequestDto);
    }
}
