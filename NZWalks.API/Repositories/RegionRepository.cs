using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Response;
using NZWalks.API.Repositories.Abstractions;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _context;
        public RegionRepository(NZWalksDbContext context)
        {
            this._context = context;
        }
        public async Task<List<Region>> GetAllAsync()
        {
            var regions = await _context.Regions.ToListAsync();

            return regions; 
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(region => region.Id == id);
            return region;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();


            return region;
        }

        public async Task<Region?> DeleteAsync(Guid Id)
        {
            var regionDomainModel = await _context.Regions.FirstOrDefaultAsync(region => region.Id == Id);
            if (regionDomainModel == null)
            {
                return null;
            }
            _context.Regions.Remove(regionDomainModel);
            await _context.SaveChangesAsync();

            return regionDomainModel;
        }     

        public async Task<Region?> UpdateAsync(Guid Id, Region region)
        {
            var ExistingRegion = _context.Regions.FirstOrDefault(region => region.Id == Id);
            if (ExistingRegion == null)
            {
                return null;
            }
            
            ExistingRegion.Name = region.Name;
            ExistingRegion.Code = region.Code;
            ExistingRegion.RegionImageUrl = region.RegionImageUrl;
            await _context.SaveChangesAsync();

            return ExistingRegion;
        }
    }
}
