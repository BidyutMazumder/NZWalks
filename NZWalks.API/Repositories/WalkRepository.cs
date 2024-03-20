using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Abstractions;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _Context;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this._Context = nZWalksDbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _Context.Walks.AddAsync(walk);
            await _Context.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> GatByIdAsynk(Guid id)
        {
            return await _Context.Walks
                .Include("Difficulty")
                .Include("region")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Walk>> GetAllWalksAsync()
        {
            return await _Context.Walks
                .Include("Difficulty")
                .Include("region")
                .ToListAsync();
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await _Context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }

            //public string Name { get; set; }
            //public string Description { get; set; }
            //public double LengthInKm { get; set; }
            //public string? WalkImageUrl { get; set; }
            //public Guid RegionId { get; set; }
            //public Guid DifficultyId { get; set; }

            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;

            await _Context.SaveChangesAsync();

            return existingWalk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await _Context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if(existingWalk == null)
            {
                return null;
            }
            _Context.Walks.Remove(existingWalk);
            await _Context.SaveChangesAsync();
            return existingWalk;

        }
    }
}
