namespace BackendTeamWebApiService.Controllers;

using Models;
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
    private readonly IOrganizationService _organizationService;

    private readonly ILogger<OrganizationController> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="OrganizationController"/>
    /// </summary>
    /// <param name="organizationService">The organization service.</param>
    /// <param name="logger">The logger.</param>
    public OrganizationController(IOrganizationService organizationService, ILogger<OrganizationController> logger)
    {
        _organizationService = organizationService;
        _logger = logger;
    }
    
    /// <summary>
    /// Pings the dependency injected organization service.
    /// </summary>
    /// <returns><see cref="bool"/>True</returns>
    [HttpGet]
    [Produces(typeof(bool))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public bool Ping()
    {
        return _organizationService.Ping();
    }

    /// <summary>
    /// Adds a new organization.
    /// </summary>
    /// <param name="addOrganizationArgs"></param>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpPost]
    [SwaggerOperation("AddNewOrganization")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddNewOrganizationAsync([FromBody] AddOrganizationArgs addOrganizationArgs)
    {
        try
        {
            return this.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    
    /// <summary>
    /// Updates an existing organization.
    /// </summary>
    /// <param name="updateOrganizationArgs"></param>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateOrganizationAsync([FromBody] UpdateOrganizationArgs updateOrganizationArgs)
    {
        try
        {
            return this.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}