using BackendTeamWebApiService.Models;

namespace BackendTeamWebApiService.Services;

/// <summary>
/// Team Member Service
/// </summary>
public interface ITeamMemberService
{
    /// <summary>
    /// Gets all team members for an organization
    /// </summary>
    /// <param name="organizationId">The organization id</param>
    /// <returns><see cref="Task"/><see cref="List{T}"/><see cref="Team"/></returns>
    Task<List<TeamMember>> GetTeamMembersByOrganizationId(Guid organizationId);

    /// <summary>
    /// Gets a team member based on the table ids.
    /// </summary>
    /// <param name="organizationId">The organization Id</param>
    /// <param name="teamMemberId">The team member id</param>
    /// <returns><see cref="Task"/><see cref="Team"/></returns>
    Task<TeamMember> GetTeamMemberByIds(Guid organizationId, Guid teamMemberId);
}