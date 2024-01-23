using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public record GetLeaveTypesDetailsQuery(int id) : IRequest<LeaveTypeDetailsDto>;

