using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Persistance.Data;
using HRLeaveManagement.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Persistance;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuartion)
    {
        services.AddDbContext<HRDbContext>
        (
            options =>
            {
                options.UseSqlServer
                (
                    configuartion.GetConnectionString("HRDBConnectionString")
                );
            }
        );

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(ILeaveTypeRepository), typeof(LeaveTypeRepository));
        services.AddScoped(typeof(ILeaveRequestRepository), typeof(LeaveRequestRepository));
        services.AddScoped(typeof(ILeaveAllocationRepository), typeof(LeaveAllocationRepository));

        return services;
    }

}
