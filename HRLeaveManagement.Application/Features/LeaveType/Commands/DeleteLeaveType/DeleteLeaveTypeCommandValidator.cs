using FluentValidation;
using HRLeaveManagement.Application.Contracts.Persistance;

namespace HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandValidator : AbstractValidator<DeleteLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _repository;

    public DeleteLeaveTypeCommandValidator(ILeaveTypeRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Id)
            .NotNull()
            .NotEqual(0);

        RuleFor(x => x)
            .MustAsync(LeaveTypeNameUnique).WithMessage("Leave type already exists");
    }

    private Task<bool> LeaveTypeNameUnique(DeleteLeaveTypeCommand command, CancellationToken token)
    {
        return _repository.IsLeaveTypeUnique(command.Name);
    }
}
