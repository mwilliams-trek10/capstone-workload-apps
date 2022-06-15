namespace BackendTeamWebApiService.Controllers;

using Models;
using Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

/// <summary>
/// Scrum Organization Controller.
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public sealed class ScrumOrganizationController : ControllerBase
{
    private readonly IScrumOrganizationService _scrumOrganizationService;

    private readonly ILogger<ScrumOrganizationController> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="ScrumOrganizationController"/>
    /// </summary>
    /// <param name="scrumOrganizationService">The scrum organization service.</param>
    /// <param name="logger">The logger.</param>
    public ScrumOrganizationController(IScrumOrganizationService scrumOrganizationService, ILogger<ScrumOrganizationController> logger)
    {
        _scrumOrganizationService = scrumOrganizationService;
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
        return _scrumOrganizationService.Ping();
    }

    /// <summary>
    /// Adds a new scrum organization.
    /// </summary>
    /// <param name="addScrumOrganizationArgs"></param>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpPost]
    [SwaggerOperation("AddNewOrganization")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddNewScrumOrganizationAsync([FromBody] AddScrumOrganizationArgs addScrumOrganizationArgs)
    {
        try
        {
            await _scrumOrganizationService.CreateScrumOrganizationAsync(addScrumOrganizationArgs);
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
    /// <param name="updateScrumOrganizationArgs"></param>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateScrumOrganizationAsync([FromBody] UpdateScrumOrganizationArgs updateScrumOrganizationArgs)
    {
        try
        {
            await _scrumOrganizationService.UpdateScrumOrganizationAsync(updateScrumOrganizationArgs);
            return this.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    
    /// <summary>
    /// Gets all scrum organizations.
    /// </summary>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<Organization>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllScrumOrganizationsAsync()
    {
        try
        {
            List<Organization> organizations = await _scrumOrganizationService.GetAllOrganizationsAsync();
            return this.Ok(organizations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    
    /// <summary>
    /// Gets scrum organization by id
    /// </summary>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<Organization>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetScrumOrganizationByIdAsync([FromHeader] string guid)
    {
        try
        {
            Guid id = Guid.Parse(guid);
            Organization organization = await _scrumOrganizationService.GetScrumOrganizationByIdAsync(id);
            return this.Ok(organization);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}