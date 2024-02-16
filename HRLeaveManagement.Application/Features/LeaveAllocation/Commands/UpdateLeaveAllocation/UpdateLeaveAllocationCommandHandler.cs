using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _repository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateLeaveAllocationCommandHandler> _logger;

    public UpdateLeaveAllocationCommandHandler(
        ILeaveAllocationRepository repository,
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper,
        IAppLogger<UpdateLeaveAllocationCommandHandler> logger)
    {
        _repository = repository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationCommandValidator(_leaveTypeRepository, _repository);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid LeaveAllocation", validationResult);

        var leaveAllocation = await _repository.GetByIdAsync(request.Id);

        if (leaveAllocation == null)
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);

        var updatedLeaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);

        await _repository.UpdateAsync(updatedLeaveAllocation);

        _logger.LogInformation($"Leave Allocation {updatedLeaveAllocation.Id} was updated.");

        return Unit.Value;
    }
}
