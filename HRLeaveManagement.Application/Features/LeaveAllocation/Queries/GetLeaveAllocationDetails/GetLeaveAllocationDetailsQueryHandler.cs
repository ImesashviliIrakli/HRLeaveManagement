using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQueryHandler : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
{
    private readonly ILeaveAllocationRepository _repository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveAllocationDetailsQueryHandler> _logger;

    public GetLeaveAllocationDetailsQueryHandler(
        ILeaveAllocationRepository repository,
        IMapper mapper,
        IAppLogger<GetLeaveAllocationDetailsQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveTypeDetails = await _repository.GetLeaveAllocationWithDetails(request.Id);

        var data = _mapper.Map<LeaveAllocationDetailsDto>(leaveTypeDetails);

        _logger.LogInformation($"Leave Allocation Details retrieved - {data}");

        return data;
    }
}