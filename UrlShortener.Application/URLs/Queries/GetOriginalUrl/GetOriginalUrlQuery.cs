using FluentValidation;
using MediatR;
using UrlShortener.Application.Common.Helpers;
using UrlShortener.Application.Interfaces;

namespace UrlShortener.Application.URLs.Queries.GetOriginalUrl;

public record GetOriginalUrlQuery(string ShortCode) : IRequest<GetOriginalUrlResponse?>;
public record GetOriginalUrlResponse(string OriginalUrl);
public class GetOriginalUrlValidator : AbstractValidator<GetOriginalUrlQuery>
{
    public GetOriginalUrlValidator()
    {
        RuleFor(x => x.ShortCode)
            .NotNull().NotEmpty()
            .WithMessage("Short code is required.");
    }
}
public class GetiOriginalUrlQueryHandler(IShortUrlRepository repository) :
    IRequestHandler<GetOriginalUrlQuery, GetOriginalUrlResponse?>
{
    private readonly IShortUrlRepository _repository = repository;
    public async Task<GetOriginalUrlResponse?> Handle(GetOriginalUrlQuery query, CancellationToken cancellationToken)
    {
        var ShortCode = await _repository.GetByShortCodeAsync(
                   query.ShortCode,
                   cancellationToken);

        if (ShortCode is null)
            return null;

        return new GetOriginalUrlResponse(ShortCode.OriginalUrl);
    }


}
