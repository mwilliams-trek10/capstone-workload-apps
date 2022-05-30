namespace BackendTeamWebApiService.Services;

/// <summary>
/// Organization Service
/// </summary>
internal sealed class OrganizationService : IOrganizationService
{
    private readonly ILogger<OrganizationService> _logger;

    /// <summary>
    /// Initializes an instance of <see cref="OrganizationService"/>
    /// </summary>
    /// <param name="logger">Organization Service logger.</param>
    public OrganizationService(ILogger<OrganizationService> logger)
    {
        _logger = logger;
    }
    
    /// <inheritdoc cref="IOrganizationService"/>
    public bool Ping()
    {
        return true;
    }
}