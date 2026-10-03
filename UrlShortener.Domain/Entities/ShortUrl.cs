namespace UrlShortener.Domain.Entities
{
    public class ShortUrl
    {
        public Guid Id { get; private set; }
        public string OriginalUrl { get; private set; }
        public string ShortCode { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ExpiresAt { get; private set; }
        public bool IsActive { get; private set; }

        private ShortUrl()
        {
        }

        public ShortUrl(
            string originalUrl,
            string shortCode,
            DateTime? expiresAt = null)
        {
            Id = Guid.NewGuid();
            OriginalUrl = originalUrl;
            ShortCode = shortCode;
            CreatedAt = DateTime.UtcNow;
            ExpiresAt = expiresAt;
            IsActive = true;
        }
    }
}
