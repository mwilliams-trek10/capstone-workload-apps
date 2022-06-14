using BackendTeamWebApiService.Models;

namespace BackendTeamWebApiService.Repositories;

/// <summary>
/// Person Repository
/// </summary>
public interface IPersonRepository
{
    /// <summary>
    /// Writes a list of persons to dynamo db.
    /// </summary>
    /// <param name="persons">The people to write to dynamo db.</param>
    /// <param name="tableName">The table name to write to in dynamo db.</param>
    /// <typeparam name="T">A <see cref="IPerson"/></typeparam>
    /// <returns><see cref="Task"/></returns>
    Task WriteListOfPersonsToDynamoDbAsync<T>(List<T> persons, string tableName) where T : IPerson;
}