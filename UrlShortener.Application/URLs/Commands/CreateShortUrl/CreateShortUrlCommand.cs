using MediatR;

namespace UrlShortener.Application.URLs.Commands.CreateShortUrl
{
    public record CreateShortUrlCommand(string OriginalUrl) : IRequest<CreateShortUrlResponse>;

    public record CreateShortUrlResponse(
        Guid Id,
        string OriginalUrl,
        string ShortCode,
        string ShortUrl
    );
}
