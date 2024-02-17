using HRLeaveManagement.Domain;
using HRLeaveManagement.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HRLeaveManagement.Persistance.IntegrationTests;

public class HRDatabaseContextTests
{
    private HRDbContext _hrDatabaseContext;

    public HRDatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<HRDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _hrDatabaseContext = new HRDbContext(dbOptions);
    }

    [Fact]
    public async void Save_SetDateCreatedValue()
    {
        // Arrange
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        // Act
        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        // Assert
        leaveType.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async void Save_SetDateModifiedValue()
    {
        // Arrange
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation"
        };

        // Act
        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        // Assert
        leaveType.DateModified.ShouldNotBeNull();
    }
}
