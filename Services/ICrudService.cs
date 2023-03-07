namespace NETCoreDemo.Services;

using NETCoreDemo.Models;

public interface ICrudService<TModel, TDto>
{
    Task<TModel?> CreateAsync(TDto request);
    Task<TModel?> GetAsync(int id);
    Task<TModel?> UpdateAsync(int id, TDto request);
    Task<bool> DeleteAsync(int id);
    // TODO: Do pagination
    Task<ICollection<TModel>> GetAllAsync(ICrudFilter? filter);
}