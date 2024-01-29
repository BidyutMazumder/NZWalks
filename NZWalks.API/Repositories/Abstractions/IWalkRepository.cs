using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Abstractions
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllWalksAsync();
        Task<Walk?> GatByIdAsynk(Guid id);
    }
}
