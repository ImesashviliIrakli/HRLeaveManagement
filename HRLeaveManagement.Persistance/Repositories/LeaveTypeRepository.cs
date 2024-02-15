using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistance.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(HRDbContext context) : base(context) { }

    public async Task<bool> IsLeaveTypeUnique(string name)
    {
        return await _context.LeaveTypes.AnyAsync(x => x.Name == name) == false;
    }
}