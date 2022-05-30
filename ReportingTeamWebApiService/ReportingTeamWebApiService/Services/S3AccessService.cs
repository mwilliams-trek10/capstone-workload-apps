namespace ReportingTeamWebApiService.Services;

using Amazon.S3;

/// <inheritdoc cref="IS3AccessService"/>
internal sealed class S3AccessService: IS3AccessService
{
    private readonly ILogger<S3AccessService> _logger;
    
    /// <summary>
    /// Initializes a new instance of <see cref="S3AccessService"/>
    /// </summary>
    /// <param name="logger">The logger.</param>
    public S3AccessService(ILogger<S3AccessService> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc cref="IS3AccessService"/>
    public Task WriteOrganizationReportToS3()
    {
        throw new NotImplementedException();
    }
}