namespace Interfaces
{
    public interface IServiceBase<TEntity, TDto>
        where TEntity : class
        where TDto : class
    {
        Task<int> SaveChangesAsync();
        Task<int> UpdateAsync(TDto dto);
        Task<int> DeleteAsync(TDto dto);
        void Add(TDto dto);
        void AddRange(IEnumerable<TDto> dtos);
        Task<TDto?> GetByIdAsync(int id);
        Task<IEnumerable<TDto>> GetAllAsync();
        void Dispose();
    }
}
