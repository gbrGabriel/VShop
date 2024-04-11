namespace VShopWeb.Interfaces;
public interface IServiceBase<TEntity, TDto>
       where TEntity : class
       where TDto : class
{
    Task<TDto?> GetByIdAsync(string requestUri, int id);
    Task<IEnumerable<TDto>?> GetAllAsync(string requestUri);
    Task<TDto?> PostAsync(string requestUri, TDto dto);
    Task<TDto?> PutAsync(string requestUri, TDto dto);
    Task DeleteAsync(string requestUri);
    TDto MapToDto<TDto>(TEntity entity);
    TEntity MapToEntity<TDto, TEntity>(TDto dto);
}