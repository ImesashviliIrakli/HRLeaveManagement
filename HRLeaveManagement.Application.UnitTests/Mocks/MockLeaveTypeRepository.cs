using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Domain;
using Moq;

namespace HRLeaveManagement.Application.UnitTests.Mocks;

public class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetLeaveTypes()
    {
        var leaveTypes = new List<LeaveType>
        {
            new LeaveType() { Id = 1, Name = "Vacation" },
            new LeaveType() { Id = 2, Name = "Sick" }
        };

        var mockRepo = new Mock<ILeaveTypeRepository>();

        mockRepo.Setup(x => x.GetAsync()).ReturnsAsync(leaveTypes);
        mockRepo.Setup(x => x.CreateAsync(It.IsAny<LeaveType>())).Returns((LeaveType leaveType) =>
        {
            leaveTypes.Add(leaveType);
            return Task.CompletedTask;
        });

        return mockRepo;
    }
}
