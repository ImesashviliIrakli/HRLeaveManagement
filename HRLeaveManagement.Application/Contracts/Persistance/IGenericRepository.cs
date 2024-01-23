namespace HRLeaveManagement.Application.Contracts.Persistance;

public interface IGenericRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    Task<List<T>> GetAsync();
    Task<T> GetByIdAsync(int id);
}
