using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationsQueryHandler : IRequestHandler<GeLeaveAllocationsQuery, List<LeaveAllocationDto>>
{
    private readonly ILeaveAllocationRepository _repository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveAllocationsQueryHandler> _logger;

    public GetLeaveAllocationsQueryHandler(
        ILeaveAllocationRepository repository,
        IMapper mapper,
        IAppLogger<GetLeaveAllocationsQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<LeaveAllocationDto>> Handle(GeLeaveAllocationsQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocations = await _repository.GetLeaveAllocationWithDetails();

        var data = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

        _logger.LogInformation("Leave allocations were retrieved successfully");

        return data;
    }
}
