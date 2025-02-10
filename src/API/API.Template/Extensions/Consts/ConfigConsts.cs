namespace API.Template.Extensions.Consts;

/// <summary>
/// Provides constants for configuration keys used in JWT authentication settings.
/// </summary>
public class ConfigConsts
{
    /// <summary>
    /// The configuration key for the JWT secret key.
    /// </summary>
    public const string Key = "Jwt:Key";

    /// <summary>
    /// The configuration key for the JWT token issuer.
    /// </summary>
    public const string Issuer = "Jwt:Issuer";

    /// <summary>
    /// The configuration key for the JWT token audience.
    /// </summary>
    public const string Audience = "Jwt:Audience";
}
