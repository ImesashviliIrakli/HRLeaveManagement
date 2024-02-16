using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.DeleLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _repository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<DeleteLeaveAllocationCommandHandler> _logger;

    public DeleteLeaveAllocationCommandHandler(
        ILeaveAllocationRepository repository,
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper,
        IAppLogger<DeleteLeaveAllocationCommandHandler> logger)
    {
        _repository = repository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _repository.GetByIdAsync(request.Id);

        if (leaveAllocation == null)
               throw new NotFoundException(nameof(LeaveAllocation), request.Id);

        await _repository.DeleteAsync(leaveAllocation);

        _logger.LogInformation($"Leave Allocation {leaveAllocation.Id} was deleted.");

        return Unit.Value;
    }
}
