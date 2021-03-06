using BackendTeamWebApiService.Models;

namespace BackendTeamWebApiService.Repositories;

/// <summary>
/// Team Member Repository
/// </summary>
public interface ITeamMemberRepository
{
    /// <summary>
    /// Writes the team members to dynamo db.
    /// </summary>
    /// <param name="teamMembers">The team members</param>
    /// <returns><see cref="Task"/></returns>
    Task WriteTeamMembersToDynamoDbAsync(IReadOnlyCollection<TeamMember> teamMembers);
    
    /// <summary>
    /// Gets all team members for an organization
    /// </summary>
    /// <param name="organizationId">The organization id</param>
    /// <returns><see cref="Task"/><see cref="List{T}"/><see cref="Team"/></returns>
    Task<List<TeamMember>> GetTeamMembersByOrganizationId(Guid organizationId);
    
    /// <summary>
    /// Gets a Team Member based on the table ids.
    /// </summary>
    /// <param name="organizationId">The organization Id</param>
    /// <param name="teamMemberId">The team member id</param>
    /// <returns><see cref="Task"/><see cref="Team"/></returns>
    Task<TeamMember> GetTeamMembersByIds(Guid organizationId, Guid teamMemberId);
}