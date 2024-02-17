using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HRLeaveManagement.Application.Models.Email;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequest;
    private readonly ILeaveTypeRepository _leaveType;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _logger;

    public ChangeLeaveRequestApprovalCommandHandler(
        ILeaveRequestRepository leaveRequest,
        ILeaveTypeRepository leaveType,
        IMapper mapper, 
        IEmailSender emailSender,
        IAppLogger<ChangeLeaveRequestApprovalCommandHandler> logger)
    {
        _leaveRequest = leaveRequest;
        _leaveType = leaveType;
        _mapper = mapper;
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var validator = new ChangeLeaveRequestApprovalCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        var leaveRequest = await _leaveRequest.GetByIdAsync(request.Id);

        if (leaveRequest is null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);

        leaveRequest.Approved = request.Approved;

        await _leaveRequest.UpdateAsync(leaveRequest);

        _logger.LogInformation($"Leave Request {leaveRequest.Id} was {(request.Approved ? "approved" : "rejected")}.");

        // if request is approved, get and update the employee's allocations

        // send confirmation email
        var email = new EmailMessage
        {
            To = string.Empty, /* Get email from employee record */
            Body = $"The approval status for your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                    $"has been updated.",
            Subject = "Leave Request Approval Status Updated"
        };

        await _emailSender.SendEmail(email);

        _logger.LogInformation($"Email sent to employee {leaveRequest.RequestingEmployeeId}.");

        return Unit.Value;
    }
}
