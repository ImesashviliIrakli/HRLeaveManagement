using AutoMapper;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _repository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateLeaveAllocationCommandHandler> _logger;

    public CreateLeaveAllocationCommandHandler(
        ILeaveAllocationRepository repository,
        ILeaveTypeRepository leaveTypeRepository,
        IMapper mapper,
        IAppLogger<CreateLeaveAllocationCommandHandler> logger)
    {
        _repository = repository;
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid LeaveAllocation", validationResult);

        var newLeaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);

        await _repository.CreateAsync(newLeaveAllocation);

        _logger.LogInformation($"Leave Allocation {newLeaveAllocation.Id} was created.");

        return Unit.Value;
    }
}