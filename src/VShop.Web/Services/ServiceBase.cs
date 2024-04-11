using AutoMapper;
using VShopWeb.Interfaces;

namespace VShopWeb.Services;

public abstract class ServiceBase<TEntity, TDto>(IHttpClientFactory httpClientFactory, IMapper mapper) : IServiceBase<TEntity, TDto>
       where TEntity : class
       where TDto : class
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(IHttpClientFactory));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<TDto?> GetByIdAsync(string requestUri, int id)
    {
        try
        {
            var response = await _httpClientFactory.CreateClient().GetAsync($"{requestUri}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TDto>();
            }
            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<TDto>?> GetAllAsync(string requestUri)
    {
        try
        {
            var response = await _httpClientFactory.CreateClient().GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<TDto>>();
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TDto?> PostAsync(string requestUri, TDto dto)
    {
        try
        {
            var response = await _httpClientFactory.CreateClient().PostAsJsonAsync(requestUri, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TDto>();
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TDto?> PutAsync(string requestUri, TDto dto)
    {
        try
        {
            var response = await _httpClientFactory.CreateClient().PutAsJsonAsync(requestUri, dto);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TDto>();
            }
            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task DeleteAsync(string requestUri)
    {
        try
        {
            await _httpClientFactory.CreateClient().DeleteAsync(requestUri);
        }
        catch (Exception)
        {
            throw;
        }
    }

#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
    public TDto MapToDto<TDto>(TEntity dto)
#pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type
        => _mapper.Map<TDto>(dto);

#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
    public TEntity MapToEntity<TDto, TEntity>(TDto dto)
#pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type
        => _mapper.Map<TEntity>(dto);
}
