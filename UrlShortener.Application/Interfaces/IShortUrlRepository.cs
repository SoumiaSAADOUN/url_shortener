using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Interfaces
{
    public interface IShortUrlRepository
    {
        Task AddAsync(
        ShortUrl shortUrl,
        CancellationToken cancellationToken = default);

        Task<ShortUrl?> GetByShortCodeAsync(
            string shortCode,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsByShortCodeAsync(
            string shortCode,
            CancellationToken cancellationToken = default);
    }
}
