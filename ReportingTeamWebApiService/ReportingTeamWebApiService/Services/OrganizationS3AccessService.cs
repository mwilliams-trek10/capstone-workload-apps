namespace ReportingTeamWebApiService.Services;

/// <inheritdoc cref="IOrganizationS3AccessService"/>
internal sealed class OrganizationS3AccessService : IOrganizationS3AccessService
{
    private readonly IOrganizationReportService _organizationReportService;

    private readonly IS3AccessService _is3ReaderWriterService;
    
    private readonly ILogger<OrganizationS3AccessService> _logger;
    
    /// <summary>
    /// Initializes a new instance of <see cref="OrganizationS3AccessService"/>
    /// </summary>
    /// <param name="organizationReportService">Organization Report Service</param>
    /// <param name="is3ReaderWriterService">S3 Writer Service</param>
    /// <param name="logger">The logger</param>
    public OrganizationS3AccessService(IOrganizationReportService organizationReportService,
        IS3AccessService is3ReaderWriterService,
        ILogger<OrganizationS3AccessService> logger)
    {
        _organizationReportService = organizationReportService;
        _is3ReaderWriterService = is3ReaderWriterService;
        _logger = logger;
    }
    
    /// <inheritdoc cref="IOrganizationS3AccessService"/>
    public async Task WriteOrganizationByIdAsync(int organizationId)
    {
        
    }
}