using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public record GeLeaveAllocationsQuery : IRequest<List<LeaveAllocationDto>>;