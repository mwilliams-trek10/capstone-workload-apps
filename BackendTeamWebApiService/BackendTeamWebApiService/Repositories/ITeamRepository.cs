using BackendTeamWebApiService.Models;

namespace BackendTeamWebApiService.Repositories;

/// <summary>
/// Team Repository
/// </summary>
public interface ITeamRepository
{
    /// <summary>
    /// Writes the teams to dynamo db.
    /// </summary>
    /// <param name="teams">The teams</param>
    /// <returns><see cref="Task"/></returns>
    Task WriteTeamsToDynamoDbAsync(List<Team> teams);
}