using BackendTeamWebApiService.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendTeamWebApiService.Controllers;

/// <summary>
/// Team Controller.
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public sealed class ReportingController : ControllerBase
{
    private readonly IReportingService _reportingService;

    private readonly ILogger<ReportingController> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="ReportingController"/>
    /// </summary>
    /// <param name="reportingService">The reporting service.</param>
    /// <param name="logger">The logger.</param>
    public ReportingController(IReportingService reportingService, ILogger<ReportingController> logger)
    {
        _reportingService = reportingService;
        _logger = logger;
    }
    
    /// <summary>
    /// Writes an organization report.
    /// </summary>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> WriteOrganizationReport([FromHeader] string organizationId)
    {
        try
        {
            Guid id = Guid.Parse(organizationId);
            await _reportingService.WriteOrganizationReport(id);
            return this.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}