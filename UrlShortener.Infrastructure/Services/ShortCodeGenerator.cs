using System.Security.Cryptography;
using UrlShortener.Application.Interfaces;

namespace UrlShortener.Infrastructure.Services;

public class ShortCodeGenerator : IShortCodeGenerator
{
    private const string Characters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public string Generate()
    {
        Span<char> result = stackalloc char[6];

        for (var i = 0; i < result.Length; i++)
        {
            result[i] = Characters[
                RandomNumberGenerator.GetInt32(Characters.Length)];
        }

        return new string(result);
    }
}