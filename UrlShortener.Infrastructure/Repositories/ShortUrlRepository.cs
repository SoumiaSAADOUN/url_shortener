using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;
using UrlShortener.Infrastructure.Persistence;
using UrlShortener.Application.Interfaces;

namespace UrlShortener.Infrastructure.Repositories;

public class ShortUrlRepository : IShortUrlRepository
{
    private readonly AppDbContext _context;

    public ShortUrlRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        ShortUrl shortUrl,
        CancellationToken cancellationToken = default)
    {
        await _context.ShortUrls.AddAsync(
            shortUrl,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ShortUrl?> GetByShortCodeAsync(
        string shortCode,
        CancellationToken cancellationToken = default)
    {
        return await _context.ShortUrls
            .FirstOrDefaultAsync(
                x => x.ShortCode == shortCode,
                cancellationToken);
    }

    public async Task<bool> ExistsByShortCodeAsync(
        string shortCode,
        CancellationToken cancellationToken = default)
    {
        return await _context.ShortUrls
            .AnyAsync(
                x => x.ShortCode == shortCode,
                cancellationToken);
    }
}