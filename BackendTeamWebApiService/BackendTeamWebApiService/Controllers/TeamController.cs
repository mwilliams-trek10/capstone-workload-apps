namespace BackendTeamWebApiService.Controllers;

using Models;
using Services;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Team Controller.
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public sealed class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;

    private readonly ILogger<TeamController> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="TeamController"/>
    /// </summary>
    /// <param name="teamService">The team service.</param>
    /// <param name="logger">The logger.</param>
    public TeamController(ITeamService teamService, ILogger<TeamController> logger)
    {
        _teamService = teamService;
        _logger = logger;
    }
    
    /// <summary>
    /// Gets all teams for an organization.
    /// </summary>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<Team>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTeamsByOrganizationIdAsync([FromHeader] string organizationId)
    {
        try
        {
            Guid id = Guid.Parse(organizationId);
            List<Team> teams = await _teamService.GetTeamsByOrganizationId(id);
            return this.Ok(teams);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    
    /// <summary>
    /// Gets a team by its ids.
    /// </summary>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Team), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTeamByIdsAsync([FromHeader] string organizationId, [FromHeader] string teamId)
    {
        try
        {
            Guid oId = Guid.Parse(organizationId);
            Guid tId = Guid.Parse(teamId);
            Team team = await _teamService.GetTeamByIds(oId, tId);
            return this.Ok(team);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}