using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistance.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(HRDbContext context) : base(context) { }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
    {
        return await _context.LeaveRequests
            .Include(x => x.LeaveType)
            .Where(x => x.RequestingEmployeeId == userId)
            .ToListAsync();
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
    {
        return await _context.LeaveRequests
            .Include(x => x.LeaveType)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails()
    {
        return await _context.LeaveRequests
            .Include(x => x.LeaveType)
            .ToListAsync();
    }
}
