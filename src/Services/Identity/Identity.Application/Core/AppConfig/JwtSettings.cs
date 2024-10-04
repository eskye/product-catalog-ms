namespace Identity.Application.Core.AppConfig
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireTime { get; set; }
        public string RefreshSecret { get; set; }
        public int RefreshTokenExpirationMin { get; set; }
    }
}

