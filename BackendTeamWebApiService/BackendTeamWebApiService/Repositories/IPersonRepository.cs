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

    /// <summary>
    /// Gets a group of persons by their organization id.
    /// </summary>
    /// <param name="organizationId">The organization id</param>
    /// <param name="tableName">The table name.</param>
    /// <typeparam name="T">The type of person.</typeparam>
    /// <returns><see cref="Task"/><see cref="List{T}"/></returns>
    Task<List<T>> GetPersonsByOrganizationId<T>(Guid organizationId, string tableName) where T : IPerson, new();

    /// <summary>
    /// Gets a person by their ids.
    /// </summary>
    /// <param name="organizationId">The organization id</param>
    /// <param name="id">The id of the person</param>
    /// <param name="tableName">The table name.</param>
    /// <typeparam name="T">The type of person.</typeparam>
    /// <returns><see cref="Task"/><see cref="List{T}"/></returns>
    Task<T> GetPersonByIds<T>(Guid organizationId, Guid id, string tableName) where T : IPerson, new();
    
}