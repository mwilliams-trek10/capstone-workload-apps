namespace ReportingTeamWebApiService.Controllers;

using Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>
/// Organization Controller.
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public sealed class OrganizationController : ControllerBase
{
    private readonly IOrganizationS3AccessService _organizationS3AccessService;
    
    private readonly IOrganizationEfsAccessService _organizationEfsAccessService;
    
    private readonly ILogger<OrganizationController> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="OrganizationController"/>
    /// </summary>
    /// <param name="organizationS3AccessService">The organization S3 writer service.</param>
    /// <param name="organizationEfsAccessService">The organization EFS writer service.</param>
    /// <param name="logger">The logger.</param>
    public OrganizationController(IOrganizationS3AccessService organizationS3AccessService,
        IOrganizationEfsAccessService organizationEfsAccessService,
        ILogger<OrganizationController> logger)
    {
        _organizationS3AccessService = organizationS3AccessService;
        _organizationEfsAccessService = organizationEfsAccessService;
        _logger = logger;
    }

    /// <summary>
    /// Writes a report based on the passed organization to S3.
    /// </summary>
    /// <param name="organizationId">The organization Id</param>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpPost]
    [SwaggerOperation("WriteReportForOrganizationById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> WriteReportForOrganizationByIdToS3Async([FromQuery] int organizationId)
    {
        try
        {
            await _organizationS3AccessService.WriteOrganizationByIdAsync(organizationId);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    
    /// <summary>
    /// Writes a report based on the passed organization to EFS.
    /// </summary>
    /// <param name="organizationId">The organization Id</param>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpPost]
    [SwaggerOperation("WriteReportForOrganizationById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> WriteReportForOrganizationByIdToEfsAsync([FromQuery] int organizationId)
    {
        try
        {
            await _organizationEfsAccessService.WriteOrganizationByIdAsync(organizationId);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}