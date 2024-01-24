using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _repository;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteLeaveTypeCommandValidator(_repository);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new BadRequestException("Invalid LeaveType", validationResult);

        var leaveTypeToDelete = await _repository.GetByIdAsync(request.Id);

        if (leaveTypeToDelete == null)
            throw new NotFoundException(nameof(LeaveType), request.Id);

        await _repository.DeleteAsync(leaveTypeToDelete);

        return Unit.Value;
    }
}
