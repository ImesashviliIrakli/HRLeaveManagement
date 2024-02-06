using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistance.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HRDbContext context) : base(context) { }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await _context.AddRangeAsync(allocations);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
        return await _context.LeaveAllocations.AnyAsync(x => x.EmployeeId == userId
            && x.LeaveTypeId == leaveTypeId
            && x.Period == period
        );
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        return await _context.LeaveAllocations
            .Include(x => x.LeaveType)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails()
    {
        return await _context.LeaveAllocations
            .Include(x => x.LeaveType)
            .ToListAsync();
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId)
    {
        return await _context.LeaveAllocations
            .Include(x => x.LeaveType)
            .Where(x => x.EmployeeId == userId)
            .ToListAsync();
    }

    public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
    {
        return await _context.LeaveAllocations
            .Include(x => x.LeaveType)
            .FirstOrDefaultAsync(
            x => x.EmployeeId == userId
            && x.LeaveTypeId == leaveTypeId
            );
    }
}
