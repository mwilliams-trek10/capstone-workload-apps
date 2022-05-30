namespace ReportingTeamWebApiService.Services;

/// <inheritdoc cref="IEfsAccessService"/>
internal sealed class EfsAccessService : IEfsAccessService
{
    private readonly ILogger<EfsAccessService> _logger;
    
    /// <summary>
    /// Initializes a new instance of <see cref="EfsAccessService"/>
    /// </summary>
    /// <param name="logger">The logger.</param>
    public EfsAccessService(ILogger<EfsAccessService> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc cref="IEfsAccessService"/>
    public Task WriteOrganizationReportToEfs()
    {
        throw new NotImplementedException();
    }
}