using AutoMapper;
using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Contracts.Logging;
using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Application.Exceptions;
using HRLeaveManagement.Application.Models.Email;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
{
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequest;
    private readonly ILeaveTypeRepository _leaveType;
    private readonly IAppLogger<CreateLeaveRequestCommandHandler> _logger;

    public CreateLeaveRequestCommandHandler(
            IEmailSender emailSender,
            IMapper mapper,
            ILeaveRequestRepository leaveRequest,
            ILeaveTypeRepository leaveType,
            IAppLogger<CreateLeaveRequestCommandHandler> logger
        )
    {
        _emailSender = emailSender;
        _mapper = mapper;
        _leaveRequest = leaveRequest;
        _leaveType = leaveType;
        _logger = logger;
    }
    public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestCommandValidator(_leaveType);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave Request", validationResult);

        // Get requesting employee's id

        // Check on employee's allocation

        // if allocations aren't enough, return validation error with message

        // Create leave request
        var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
        await _leaveRequest.CreateAsync(leaveRequest);

        _logger.LogInformation($"Leave Request {leaveRequest.Id} was created.");

        // send confirmation email
        var email = new EmailMessage
        {
            To = string.Empty, /* Get email from employee record */
            Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " +
                    $"has been submitted successfully.",
            Subject = "Leave Request Submitted"
        };

        await _emailSender.SendEmail(email);

        _logger.LogInformation($"Confirmation email for leave request {leaveRequest.Id} was sent.");

        return Unit.Value;
    }
}
