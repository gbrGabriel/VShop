using Microsoft.EntityFrameworkCore;
using VShopProduct.Context;
using VShopProduct.Interfaces;

namespace VShopProduct.Repositories;

public abstract class RepositoryBase<TEntity>(ApplicationDbContext context) : IRepositoryBase<TEntity>
    where TEntity : class
{
    protected readonly ApplicationDbContext _context = context;

    public async Task<int> DeleteAsync(TEntity entity)
    {
        int result = 0;

        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                _context.ChangeTracker.Clear();
                _context.Set<TEntity>().Remove(entity);

                result = await _context.SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        return result;
    }

    public async Task<int> SaveChangesAsync()
    {
        int result = 0;
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                result = await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        return result;
    }

    public async Task<int> UpdateAsync(TEntity entity)
    {
        int result = 0;
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                _context.Set<TEntity>().Update(entity);

                result = await _context.SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction?.Rollback();
                throw;
            }
        }
        return result;
    }

    public void Add(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Add(entity);
            }
            else
            {
                _context.Set<TEntity>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync() =>
        await _context.Set<TEntity>().ToListAsync();

    public virtual async Task<TEntity?> GetByIdAsync(int id) =>
        await _context.Set<TEntity>().FindAsync(id);

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
