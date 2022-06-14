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
}