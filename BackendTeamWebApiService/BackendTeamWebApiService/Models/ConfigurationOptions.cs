namespace BackendTeamWebApiService.Models;

/// <summary>
/// Configuration Options
/// </summary>
public sealed class ConfigurationOptions
{
    /// <summary>
    /// Gets or sets the Region
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// Gets or sets the IsDevelopment flag
    /// </summary>
    public bool IsDevelopment { get; set; }

    /// <summary>
    /// Gets or sets the use web token property.
    /// </summary>
    public bool UseWebToken { get; set; }
}