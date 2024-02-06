using HRLeaveManagement.Application.Contracts.Persistance;
using HRLeaveManagement.Domain.Common;
using HRLeaveManagement.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace HRLeaveManagement.Persistance.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity 
{
    protected readonly HRDbContext _context;

    public GenericRepository(HRDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }
}
