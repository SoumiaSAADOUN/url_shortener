using FluentValidation;
using MediatR;
using UrlShortener.Application.Common.Helpers;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.URLs.Commands.CreateShortUrl;

public record CreateShortUrlCommand(string OriginalUrl) : IRequest<CreateShortUrlResponse>;

public record CreateShortUrlResponse(
    Guid Id,
    string OriginalUrl,
    string ShortCode,
    string ShortUrl
);
public class CreateShortUrlCommandValidator
    : AbstractValidator<CreateShortUrlCommand>
{
    public CreateShortUrlCommandValidator()
    {
        RuleFor(x => x.OriginalUrl)
            .NotEmpty()
            .WithMessage("URL is required.")
            .Must(UrlHelper.IsValidUrl)
            .WithMessage("A valid HTTP or HTTPS URL is required.");
    }

 
}
public class CreateShortUrlHandler(
    IShortUrlRepository repository,
    IShortCodeGenerator shortCodeGenerator)
        : IRequestHandler<CreateShortUrlCommand, CreateShortUrlResponse>
{
    private readonly IShortUrlRepository _repository = repository;
    private readonly IShortCodeGenerator _shortCodeGenerator = shortCodeGenerator;

    public async Task<CreateShortUrlResponse> Handle(
        CreateShortUrlCommand request,
        CancellationToken cancellationToken)
    {
        string shortCode;

        do
        {
            shortCode = _shortCodeGenerator.Generate();
        }
        while (await _repository.ExistsByShortCodeAsync(
            shortCode,
            cancellationToken));

        var shortUrl = new ShortUrl(
            request.OriginalUrl,
            shortCode);

        await _repository.AddAsync(
            shortUrl,
            cancellationToken);

        return new CreateShortUrlResponse(
            shortUrl.Id,
            shortUrl.OriginalUrl,
            shortUrl.ShortCode,
            $"https://localhost:5001/{shortUrl.ShortCode}");
    }
}