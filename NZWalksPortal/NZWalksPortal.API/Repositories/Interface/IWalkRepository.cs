using NZWalksPortal.API.Models.Domain;

namespace NZWalksPortal.API.Repositories.Interface
{
    public interface IWalkRepository
    {
        Task<Walk> AddWalkAsync(Walk walk);
        Task<IEnumerable<Walk>> GetAllWalksAsync();
        Task<Walk?> GetWalkByIdAsync(Guid id);
        Task<Walk?> DeleteWalkByIdAsync(Guid id);
    }
}
