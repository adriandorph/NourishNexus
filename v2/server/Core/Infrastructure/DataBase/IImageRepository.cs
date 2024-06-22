namespace server.Core.Infrastructure.DataBase;

public interface IImageRepository
{
    Task<Image?> CreateImageAsync(Image image);
    Task<Image?> UpdateImageAsync(Image image);
    Task<Image?> GetImageByIdAsync(string id);
    Task<List<Image>> GetImagesByIdsAsync(List<string> ids);
    Task<bool> DeleteImageAsync(string id);
}