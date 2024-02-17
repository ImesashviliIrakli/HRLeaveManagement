using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequests;

public class GetLeaveRequestsQueryHandler : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequestDto>>
{
    private readonly ILeaveRequestRepository _repository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveRequestsQueryHandler> _logger;

    public GetLeaveRequestsQueryHandler(ILeaveRequestRepository repository, IMapper mapper, IAppLogger<GetLeaveRequestsQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
    {
        var leaveRequests = await _repository.GetAsync();

        var data = _mapper.Map<List<LeaveRequestDto>>(leaveRequests);

        _logger.LogInformation("Leave requests were retrieved successfully");

        return data;
    }
}
