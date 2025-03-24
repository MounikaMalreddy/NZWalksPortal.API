using Microsoft.AspNetCore.Mvc;
using NZWalksPortal.API.Models.Domain;

namespace NZWalksPortal.API.Repositories.Interface
{
    public interface IWalkRepository
    {
        Task<Walk> AddWalkAsync(Walk walk);
        Task<IEnumerable<Walk>> GetAllWalksAsync(string? filterQuery=null, string? sortBy=null,
            string? sortDirection = null, int? pageNumber=1, int? pageSize=10);
        Task<Walk?> GetWalkByIdAsync(Guid id);
        Task<Walk?> DeleteWalkByIdAsync(Guid id);
        Task<Walk?> UpdateWalkAsync(Walk walk);
    }
}
