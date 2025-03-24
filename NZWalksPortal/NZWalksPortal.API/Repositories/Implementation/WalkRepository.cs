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

        public async Task<IEnumerable<Walk>> GetAllWalksAsync(string? filterQuery = null, string? sortBy = null,
            string? sortDirection = null, int? pageNumber=1, int? pageSize=10)
        {
            var walks= dbContext.Walk.AsQueryable();
            if (!string.IsNullOrEmpty(filterQuery))
            {
                walks = walks.Where(w => w.Name.Contains(filterQuery)); 
            }
            if(!string.IsNullOrEmpty(sortBy))
            {
                if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc= string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase);
                    walks = isAsc ? walks.OrderBy(w => w.Name) : walks.OrderByDescending(w => w.Name);
                }
            }
            var skipResults = (pageNumber - 1) * pageSize;
            return await walks.Skip(skipResults??0).Take(pageSize??10).ToListAsync();
            //return await dbContext.Walk.ToListAsync();
        }

        public Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            var existingWalk = dbContext.Walk.FirstOrDefaultAsync(w => w.Id == id);
            if (existingWalk is null)
                return null;
            return existingWalk;
        }

        public async Task<Walk?> UpdateWalkAsync(Walk walk)
        {
            var existingWalk =await dbContext.Walk.FirstOrDefaultAsync(w => w.Id == walk.Id);
            if (existingWalk is null)
                return null;
            dbContext.Entry(existingWalk).CurrentValues.SetValues(walk);
            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
