using MediatR;
using UrlShortener.Application.URLs.Commands.CreateShortUrl;
using UrlShortener.Application.URLs.Queries.GetOriginalUrl;

namespace UrlShortener.Api.Endpoints;

public static class UrlEndpoints
{
    public static IEndpointRouteBuilder MapUrlEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/urls", async (
            CreateShortUrlCommand command,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(command, cancellationToken);

            return Results.Ok(result);
        })
        .Produces<CreateShortUrlResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

        app.MapGet("/{shortCode}", async (
            string shortCode,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetOriginalUrlQuery(shortCode);

            var result = await sender.Send(query, cancellationToken);

            if (result is null)
                return Results.NotFound();
            return Results.Ok(result);
        })
        .Produces<GetOriginalUrlResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status302Found)
        .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}
