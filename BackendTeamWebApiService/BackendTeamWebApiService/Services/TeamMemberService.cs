using BackendTeamWebApiService.Models;
using BackendTeamWebApiService.Repositories;

namespace BackendTeamWebApiService.Services;

/// <inheritdoc cref="ITeamMemberService"/>
internal sealed class TeamMemberService : ITeamMemberService
{
    private readonly ITeamMemberRepository _teamMemberRepository;
    
    /// <summary>
    /// Initializes a new instance of <see cref="TeamMemberService"/>
    /// </summary>
    /// <param name="teamMemberRepository">The team member repository.</param>
    public TeamMemberService(ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }

    /// <summary>
    /// Gets all teams for an organization
    /// </summary>
    /// <param name="organizationId">The organization id</param>
    /// <returns><see cref="Task"/><see cref="List{T}"/><see cref="Team"/></returns>
    public async Task<List<TeamMember>> GetTeamMembersByOrganizationId(Guid organizationId)
    {
        return await _teamMemberRepository.GetTeamMembersByOrganizationId(organizationId);
    }

    /// <summary>
    /// Gets a Team Member based on the table ids.
    /// </summary>
    /// <param name="organizationId">The organization Id</param>
    /// <param name="teamMemberId">The team id</param>
    /// <returns><see cref="Task"/><see cref="Team"/></returns>
    public async Task<TeamMember> GetTeamMemberByIds(Guid organizationId, Guid teamMemberId)
    {
        return await _teamMemberRepository.GetTeamMembersByIds(organizationId, teamMemberId);
    }
}