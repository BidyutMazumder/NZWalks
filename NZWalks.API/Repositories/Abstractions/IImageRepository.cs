using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Abstractions
{
    public interface IImageRepository
    {
        Task<Image>Upload(Image image);
    }
}
