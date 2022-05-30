namespace ReportingTeamWebApiService.Services;

/// <inheritdoc cref="IDynamoDbAccessService"/>
internal sealed class DynamoDbAccessService : IDynamoDbAccessService
{
    private readonly ILogger<DynamoDbAccessService> _logger;
    
    /// <summary>
    /// Initializes a new instance of <see cref="DynamoDbAccessService"/>
    /// </summary>
    /// <param name="logger">The logger.</param>
    public DynamoDbAccessService(ILogger<DynamoDbAccessService> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc cref="IDynamoDbAccessService"/>
    public Task GetOrganizationByIdAsync(int organizationId)
    {
        throw new NotImplementedException();
    }
}