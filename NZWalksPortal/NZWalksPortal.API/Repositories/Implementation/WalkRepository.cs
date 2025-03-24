using Microsoft.EntityFrameworkCore;
using NZWalksPortal.API.Data;
using NZWalksPortal.API.Models.Domain;
using NZWalksPortal.API.Repositories.Interface;

namespace NZWalksPortal.API.Repositories.Implementation
{
    public class WalkRepository: IWalkRepository
    {
        private readonly ApplicationDbContext dbContext;

        public WalkRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            await dbContext.Walk.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteWalkByIdAsync(Guid id)
        {
            var existingWalk =await dbContext.Walk.FirstOrDefaultAsync(w => w.Id == id);
            if (existingWalk is null)
                return null;
            dbContext.Walk.Remove(existingWalk);
            dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<IEnumerable<Walk>> GetAllWalksAsync()
        {
            return await dbContext.Walk.ToListAsync();
        }

        public Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            var existingWalk = dbContext.Walk.FirstOrDefaultAsync(w => w.Id == id);
            if (existingWalk is null)
                return null;
            return existingWalk;
        }
    }
}
