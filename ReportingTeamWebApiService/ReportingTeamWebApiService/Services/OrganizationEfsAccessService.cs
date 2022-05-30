namespace ReportingTeamWebApiService.Services;

/// <inheritdoc cref="IOrganizationEfsAccessService"/>
internal sealed class OrganizationEfsAccessService : IOrganizationEfsAccessService
{
    private readonly IOrganizationReportService _organizationReportService;

    private readonly IEfsAccessService _efsAccessService;
    
    private readonly ILogger<OrganizationEfsAccessService> _logger;
    
    /// <summary>
    /// Initializes a new instance of <see cref="OrganizationEfsAccessService"/>
    /// </summary>
    /// <param name="organizationReportService">Organization Report Service</param>
    /// <param name="efsAccessService">Efs Writer Service</param>
    /// <param name="logger">The logger</param>
    public OrganizationEfsAccessService(IOrganizationReportService organizationReportService,
        IEfsAccessService efsAccessService,
        ILogger<OrganizationEfsAccessService> logger)
    {
        _organizationReportService = organizationReportService;
        _efsAccessService = efsAccessService;
        _logger = logger;
    }
    
    /// <inheritdoc cref="IOrganizationEfsAccessService"/>
    public async Task WriteOrganizationByIdAsync(int organizationId)
    {
        
    }
}