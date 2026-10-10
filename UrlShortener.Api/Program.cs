using FluentValidation;
using UrlShortener.Api.Common.Behaviors;
using UrlShortener.Api.Common.Middleware;
using UrlShortener.Api.Endpoints;
using UrlShortener.Application.Common.Settings;
using UrlShortener.Application.URLs.Commands.CreateShortUrl;
using UrlShortener.Application.URLs.Queries.GetOriginalUrl;
using UrlShortener.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        typeof(CreateShortUrlCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetOriginalUrlQuery).Assembly);

    cfg.AddOpenBehavior(
        typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(
    typeof(CreateShortUrlCommand).Assembly);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.Configure<ShortUrlSettings>(
    builder.Configuration.GetSection("ShortUrlSettings"));

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.MapUrlEndpoints();

app.Run();