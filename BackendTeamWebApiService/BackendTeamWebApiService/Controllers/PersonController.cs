using BackendTeamWebApiService.Models;
using BackendTeamWebApiService.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendTeamWebApiService.Controllers;

/// <summary>
/// Person Controller.
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public sealed class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    private readonly ILogger<PersonController> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="PersonController"/>
    /// </summary>
    /// <param name="personService">The person service.</param>
    /// <param name="logger">The logger.</param>
    public PersonController(IPersonService personService, ILogger<PersonController> logger)
    {
        _personService = personService;
        _logger = logger;
    }
    
    /// <summary>
    /// Gets all scrum masters for an organization.
    /// </summary>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<ScrumMaster>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetScrumMastersByOrganizationIdAsync([FromHeader] string organizationId)
    {
        try
        {
            Guid id = Guid.Parse(organizationId);
            List<ScrumMaster> scrumMasters = await _personService.GetScrumMastersByOrganizationId(id);
            return this.Ok(scrumMasters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    
    /// <summary>
    /// Gets a scrum master by its ids.
    /// </summary>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ScrumMaster), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetScrumMasterByIdsAsync([FromHeader] string organizationId, [FromHeader] string scrumMasterId)
    {
        try
        {
            Guid oId = Guid.Parse(organizationId);
            Guid smId = Guid.Parse(scrumMasterId);
            ScrumMaster team = await _personService.GetScrumMastersByIds(oId, smId);
            return this.Ok(team);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    
    
    /// <summary>
    /// Gets all supervisors for an organization.
    /// </summary>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<Supervisor>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSupervisorsByOrganizationIdAsync([FromHeader] string organizationId)
    {
        try
        {
            Guid id = Guid.Parse(organizationId);
            List<Supervisor> supervisors = await _personService.GetSupervisorsByOrganizationId(id);
            return this.Ok(supervisors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    
    /// <summary>
    /// Gets a supervisor by its ids.
    /// </summary>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Supervisor), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSupervisorByIdsAsync([FromHeader] string organizationId, [FromHeader] string scrumMasterId)
    {
        try
        {
            Guid oId = Guid.Parse(organizationId);
            Guid smId = Guid.Parse(scrumMasterId);
            Supervisor team = await _personService.GetSupervisorsByIds(oId, smId);
            return this.Ok(team);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}