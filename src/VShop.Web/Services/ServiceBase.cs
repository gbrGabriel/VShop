using AutoMapper;
using System.Text.Json;
using VShopWeb.Interfaces;

namespace VShopWeb.Services;

public abstract class ServiceBase<TModel>(IHttpClientFactory httpClientFactory, IMapper mapper) : IServiceBase<TModel>
       where TModel : class
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(IHttpClientFactory));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public async Task<TModel?> GetByIdAsync(string requestUri, int id)
    {
        try
        {
            using var response = await _httpClientFactory.CreateClient("MicroserviceProduct").GetAsync($"{requestUri}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<TModel>(await response.Content.ReadAsStreamAsync(), _options);
            }
            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<TModel>?> GetAllAsync(string requestUri)
    {
        try
        {
            using var response = await _httpClientFactory.CreateClient("MicroserviceProduct").GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<TModel>>(await response.Content.ReadAsStreamAsync(), _options);
            }

            return Enumerable.Empty<TModel>(); ;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TModel?> PostAsync(string requestUri, TModel model)
    {
        try
        {
            using var response = await _httpClientFactory.CreateClient("MicroserviceProduct").PostAsJsonAsync(requestUri, model);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<TModel>(await response.Content.ReadAsStreamAsync(), _options);
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TModel?> PutAsync(string requestUri, TModel model)
    {
        try
        {
            using var response = await _httpClientFactory.CreateClient().PutAsJsonAsync(requestUri, model);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<TModel>(await response.Content.ReadAsStreamAsync(), _options);
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
            await _httpClientFactory.CreateClient("MicroserviceProduct").DeleteAsync(requestUri);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
