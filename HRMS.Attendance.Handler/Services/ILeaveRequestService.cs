using HRMS.Attendance.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Attendance.Handler.Services
{
    public interface ILeaveRequestService
    {

        Task<IEnumerable<LeaveRequestDto>> GetAllPendingRequests();
        Task<IEnumerable<LeaveRequestDto>> GetAllApprovedRequests();
        Task ApproveLeaveRequest(int id);
        Task DeleteLeaveRequest(int id);
        Task UpdateApprovedLeaveRequest(LeaveRequestDto dto);
        Task<LeaveRequestDto> GetById(int id); 
    }
}

