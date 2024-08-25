using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using System.Globalization;

namespace NZWalks.API.Repositories.Abstractions
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? sortType = null);
        Task<Walk?> GatByIdAsynk(Guid id);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
