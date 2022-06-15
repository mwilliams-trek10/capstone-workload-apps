namespace BackendTeamWebApiService.Controllers;

using Models;
using Services;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Team Member Controller.
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class TeamMemberController : ControllerBase
{
    private readonly ITeamMemberService _teamMemberService;

    private readonly ILogger<TeamMemberController> _logger;

    /// <summary>
    /// Initializes a new instance of <see cref="TeamMemberController"/>
    /// </summary>
    /// <param name="teamMemberService">The team member service.</param>
    /// <param name="logger">The logger.</param>
    public TeamMemberController(ITeamMemberService teamMemberService, ILogger<TeamMemberController> logger)
    {
        _teamMemberService = teamMemberService;
        _logger = logger;
    }
    
    /// <summary>
    /// Gets all team members for an organization.
    /// </summary>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<TeamMember>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTeamMembersByOrganizationIdAsync([FromHeader] string organizationId)
    {
        try
        {
            Guid id = Guid.Parse(organizationId);
            List<TeamMember> teamMembers = await _teamMemberService.GetTeamMembersByOrganizationId(id);
            return this.Ok(teamMembers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    
    /// <summary>
    /// Gets a team member by its ids.
    /// </summary>
    /// <returns>Ok Http Status code if successful.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(TeamMember), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTeamMembersByIdsAsync([FromHeader] string organizationId, [FromHeader] string teamMemberId)
    {
        try
        {
            Guid oId = Guid.Parse(organizationId);
            Guid tmId = Guid.Parse(teamMemberId);
            TeamMember teamMember = await _teamMemberService.GetTeamMemberByIds(oId, tmId);
            return this.Ok(teamMember);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}