using MediatR;
using UrlShortener.Application.URLs.Commands.CreateShortUrl;

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

        return app;
    }
}
