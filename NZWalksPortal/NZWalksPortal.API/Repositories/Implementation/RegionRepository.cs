using Microsoft.EntityFrameworkCore;
using NZWalksPortal.API.Data;
using NZWalksPortal.API.Models.Domain;
using NZWalksPortal.API.Repositories.Interface;

namespace NZWalksPortal.API.Repositories.Implementation
{
    public class RegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext dbContext;

        public RegionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            await dbContext.Region.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteRegionAsync(Guid regionId)
        {
            var existingRegion=await dbContext.Region.FirstOrDefaultAsync(x => x.Id == regionId);
            if (existingRegion is null)
                return null;
            dbContext.Region.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
            return await dbContext.Region.ToListAsync();
        }

        public Task<Region?> GetRegionByIdAsync(Guid regionId)
        {
            var existingRegion = dbContext.Region.FirstOrDefaultAsync(x => x.Id == regionId);
            if (existingRegion is null)
                return null;
            return existingRegion;
        }

        public async Task<Region?> UpdateRegionAsync(Region region)
        {
            var existingRegion =await dbContext.Region.FirstOrDefaultAsync(x => x.Id == region.Id);
            if (existingRegion is null)
                return null;
            dbContext.Entry(existingRegion).CurrentValues.SetValues(region);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
