using Microsoft.EntityFrameworkCore;
using NZWalksPortal.API.Data;
using NZWalksPortal.API.Models.Domain;
using NZWalksPortal.API.Repositories.Interface;

namespace NZWalksPortal.API.Repositories.Implementation
{
    public class DifficultyRepository : IDifficultyRepository
    {
        private readonly ApplicationDbContext dbContext;

        public DifficultyRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Difficulty?> GetDifficultyByIdAsync(Guid id)
        {
            var existingDifficulty =await dbContext.Difficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDifficulty is null)
                return null;
            return existingDifficulty;
        }
    }
}
