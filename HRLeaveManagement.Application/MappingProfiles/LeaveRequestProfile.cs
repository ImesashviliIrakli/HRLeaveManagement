using AutoMapper;
using HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using HRLeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequests;
using HRLeaveManagement.Domain;

namespace HRLeaveManagement.Application.MappingProfiles;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequestDto, LeaveRequest>().ReverseMap();
        CreateMap<LeaveRequest, LeaveRequestDetailsDto>();
    }
}