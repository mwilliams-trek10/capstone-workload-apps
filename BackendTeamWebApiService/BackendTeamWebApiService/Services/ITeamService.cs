using BackendTeamWebApiService.Models;

namespace BackendTeamWebApiService.Services;

/// <summary>
/// Team Service
/// </summary>
public interface ITeamService
{
    /// <summary>
    /// Gets all teams for an organization
    /// </summary>
    /// <param name="organizationId">The organization id</param>
    /// <returns><see cref="Task"/><see cref="List{T}"/><see cref="Team"/></returns>
    Task<List<Team>> GetTeamsByOrganizationId(Guid organizationId);
    
    /// <summary>
    /// Gets a Team based on the table ids.
    /// </summary>
    /// <param name="organizationId">The organization Id</param>
    /// <param name="teamId">The team id</param>
    /// <returns><see cref="Task"/><see cref="Team"/></returns>
    Task<Team> GetTeamByIds(Guid organizationId, Guid teamId);
}