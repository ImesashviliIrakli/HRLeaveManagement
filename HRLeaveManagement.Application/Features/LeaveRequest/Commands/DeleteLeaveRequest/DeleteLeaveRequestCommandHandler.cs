using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Application.Exceptions;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _repository;
    private readonly IAppLogger<DeleteLeaveRequestCommandHandler> _logger;

    public DeleteLeaveRequestCommandHandler(
        ILeaveRequestRepository repository,
        IAppLogger<DeleteLeaveRequestCommandHandler> logger
        )
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _repository.GetByIdAsync(request.Id);

        if (leaveRequest == null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);

        await _repository.DeleteAsync(leaveRequest);

        _logger.LogInformation($"Leave Request {request.Id} was deleted successfully");

        return Unit.Value;
    }
}