using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

namespace HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _repository;
    public CreateLeaveAllocationCommandValidator(ILeaveTypeRepository repository)
    {
        _repository = repository;
        RuleFor(x => x.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("{PropertyName} does not exist");
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var leaveType = await _repository.GetByIdAsync(id);

        return leaveType != null;
    }
}
