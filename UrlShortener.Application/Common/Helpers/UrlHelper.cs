namespace UrlShortener.Application.Common.Helpers;

public static class UrlHelper
{
    public static bool IsValidUrl(string url)
    {
        return Uri.TryCreate(
                   url,
                   UriKind.Absolute,
                   out var uri)
               && (uri.Scheme == Uri.UriSchemeHttp
                   || uri.Scheme == Uri.UriSchemeHttps);
    }
}