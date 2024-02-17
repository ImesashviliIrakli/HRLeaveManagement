using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequests;

public record GetLeaveRequestsQuery : IRequest<List<LeaveRequestDto>>;
