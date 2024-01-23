using AutoMapper;
using HRLeaveManagement.Application.Contracts.Persistance;
using MediatR;

namespace HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _repository;

    public GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        var leaveTypes = await _repository.GetAsync();

        var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        return data;
    }
}
