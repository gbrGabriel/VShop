using AutoMapper;
using VShopProduct.Interfaces;

namespace VShopProduct.Services;

public abstract class ServiceBase<TEntity, TDto>(IRepositoryBase<TEntity> repository, IMapper mapper) : IServiceBase<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    private readonly IRepositoryBase<TEntity> _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public virtual TEntity MapDtoToEntity(TDto dto) => _mapper.Map<TEntity>(dto);

    public virtual IEnumerable<TEntity> MapListDtoToEntity(IEnumerable<TDto> dtos) =>
        _mapper.Map<IEnumerable<TEntity>>(dtos);

    public virtual IEnumerable<TDto> MapListEntityToDto(IEnumerable<TEntity> entities) =>
        _mapper.Map<IEnumerable<TDto>>(entities);

    public virtual TDto MapEntityToDto(TEntity entity) => _mapper.Map<TDto>(entity);

    public virtual void Add(TDto dto)
    {
        try
        {
            _repository.Add(MapDtoToEntity(dto));
        }
        catch (Exception)
        {
            throw;
        }
    }

    public virtual void AddRange(IEnumerable<TDto> dtos)
    {
        try
        {
            _repository.AddRange(MapListDtoToEntity(dtos));
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await _repository.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TDto> AddSaveAsync(TDto dto)
    {
        try
        {
            var entity = MapDtoToEntity(dto);

            _repository.Add(entity);

            await _repository.SaveChangesAsync();

            return MapEntityToDto(entity);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TDto> AddUpdateAsync(TDto dto)
    {
        try
        {
            var entity = MapDtoToEntity(dto);

            await _repository.UpdateAsync(entity);

            return MapEntityToDto(entity);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public virtual async Task<int> UpdateAsync(TDto dto)
    {
        try
        {
            return await _repository.UpdateAsync(MapDtoToEntity(dto));
        }
        catch (Exception)
        {
            throw;
        }
    }

    public virtual async Task<int> DeleteAsync(TDto dto)
    {
        try
        {
            return await _repository.DeleteAsync(MapDtoToEntity(dto));
        }
        catch (Exception)
        {
            throw;
        }
    }

    public virtual async Task<TDto?> GetByIdAsync(int id)
    {
        try
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return MapEntityToDto(await _repository.GetByIdAsync(id));
#pragma warning restore CS8604 // Possible null reference argument.
        }
        catch (Exception)
        {
            throw;
        }
    }

    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
    {
        try
        {
            return MapListEntityToDto(await _repository.GetAllAsync());
        }
        catch (Exception)
        {
            throw;
        }
    }

    public virtual void Dispose()
        => _repository.Dispose();
}
