namespace ReportingTeamWebApiService.Services;

/// <inheritdoc cref="IOrganizationReportService"/>
internal sealed class OrganizationReportService : IOrganizationReportService
{
    private readonly IDynamoDbAccessService _dynamoDbAccessService;
    
    private readonly ILogger<OrganizationReportService> _logger;
    
    /// <summary>
    /// Initializes a new instance of <see cref="OrganizationReportService"/>
    /// </summary>
    /// <param name="dynamoDbAccessService">The dynamo db access service</param>
    /// <param name="logger">The logger</param>
    public OrganizationReportService(IDynamoDbAccessService dynamoDbAccessService,
        ILogger<OrganizationReportService> logger)
    {
        _dynamoDbAccessService = dynamoDbAccessService;
        _logger = logger;
    }
    
    /// <inheritdoc cref="IOrganizationReportService"/>
    public async Task<bool> PingAsync()
    {
        return await Task.FromResult(true);
    }

    /// <inheritdoc cref="IOrganizationReportService"/>
    public async Task GetOrganizationById(int organizationId)
    {
        
    }
    
}