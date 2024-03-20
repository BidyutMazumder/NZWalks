using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Abstractions
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllWalksAsync();
        Task<Walk?> GatByIdAsynk(Guid id);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
