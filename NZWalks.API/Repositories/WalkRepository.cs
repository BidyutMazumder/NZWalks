﻿using Microsoft.EntityFrameworkCore;
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
    }
}
