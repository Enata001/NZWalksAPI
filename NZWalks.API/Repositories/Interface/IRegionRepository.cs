using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interface;

public interface IRegionRepository
{
    Task<List<Region>> GetAllAsync();
    Task<Region?> GetById(Guid id);
    Task<Region?> Remove(Guid id);
    Task<Region?> Update(Guid id, Region region);
    Task<Region> CreateAsync(Region region);

}