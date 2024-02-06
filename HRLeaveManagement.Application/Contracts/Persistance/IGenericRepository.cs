using HRLeaveManagement.Domain.Common;

namespace HRLeaveManagement.Application.Contracts.Persistance;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IReadOnlyList<T>> GetAsync();
    Task<T> GetByIdAsync(int id);
}
