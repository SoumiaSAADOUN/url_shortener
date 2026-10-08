using FluentValidation;
using MediatR;
using UrlShortener.Api.Common.Behaviors;
using UrlShortener.Api.Common.Middleware;
using UrlShortener.Api.Endpoints;
using UrlShortener.Application.URLs.Commands.CreateShortUrl;
using UrlShortener.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        typeof(CreateShortUrlCommand).Assembly);

    cfg.AddOpenBehavior(
        typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(
    typeof(CreateShortUrlCommand).Assembly);

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.MapUrlEndpoints();

app.Run();