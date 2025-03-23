using NZWalksPortal.API.Models.Domain;

namespace NZWalksPortal.API.Repositories.Interface
{
    public interface IRegionRepository
    {
        Task<Region> AddRegionAsync(Region region);
        Task<IEnumerable<Region>> GetAllRegionsAsync();
        Task<Region?> GetRegionByIdAsync(Guid regionId);
        Task<Region?> UpdateRegionAsync(Region region);
        Task<Region?> DeleteRegionAsync(Guid regionId);
    }
}
