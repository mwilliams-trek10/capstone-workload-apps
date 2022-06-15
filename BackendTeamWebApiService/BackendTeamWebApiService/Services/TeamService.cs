using BackendTeamWebApiService.Models;
using BackendTeamWebApiService.Repositories;

namespace BackendTeamWebApiService.Services;

/// <inheritdoc cref="ITeamService"/>
internal sealed class TeamService : ITeamService
{private readonly ITeamRepository _teamRepository;
    
    public TeamService(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    /// <summary>
    /// Gets all teams for an organization
    /// </summary>
    /// <param name="organizationId">The organization id</param>
    /// <returns><see cref="Task"/><see cref="List{T}"/><see cref="Team"/></returns>
    public async Task<List<Team>> GetTeamsByOrganizationId(Guid organizationId)
    {
        return await _teamRepository.GetTeamsByOrganizationId(organizationId);
    }

    /// <summary>
    /// Gets a Team based on the table ids.
    /// </summary>
    /// <param name="organizationId">The organization Id</param>
    /// <param name="teamId">The team id</param>
    /// <returns><see cref="Task"/><see cref="Team"/></returns>
    public async Task<Team> GetTeamByIds(Guid organizationId, Guid teamId)
    {
        return await _teamRepository.GetTeamByIds(organizationId, teamId);
    }
}