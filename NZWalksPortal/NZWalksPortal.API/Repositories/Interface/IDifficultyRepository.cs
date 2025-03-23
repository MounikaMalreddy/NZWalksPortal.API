using NZWalksPortal.API.Models.Domain;

namespace NZWalksPortal.API.Repositories.Interface
{
    public interface IDifficultyRepository
    {
        Task<Difficulty?> GetDifficultyByIdAsync(Guid id);
    }
}
