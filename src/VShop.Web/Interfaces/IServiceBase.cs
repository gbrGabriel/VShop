namespace VShopWeb.Interfaces;
public interface IServiceBase<TModel>
       where TModel : class
{
    Task<TModel?> GetByIdAsync(string requestUri, int id);
    Task<IEnumerable<TModel>?> GetAllAsync(string requestUri);
    Task<TModel?> PostAsync(string requestUri, TModel model);
    Task<TModel?> PutAsync(string requestUri, TModel model);
    Task DeleteAsync(string requestUri);
}