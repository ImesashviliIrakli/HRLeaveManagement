using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.DeleLeaveAllocation;

public record DeleteLeaveAllocationCommand(int Id) : IRequest<Unit>;
