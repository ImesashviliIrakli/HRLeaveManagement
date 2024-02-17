using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequests;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
    private readonly ILeaveRequestRepository _repository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveRequestDetailsQueryHandler> _logger;

    public GetLeaveRequestDetailsQueryHandler(
        ILeaveRequestRepository repository,
        IMapper mapper,
        IAppLogger<GetLeaveRequestDetailsQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _repository.GetLeaveRequestWithDetails(request.Id);

        var data = _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);

        _logger.LogInformation("Leave request was retrieved successfully");

        return data;
    }
}
