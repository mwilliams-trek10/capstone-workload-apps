using BackendTeamWebApiService.Models;

namespace BackendTeamWebApiService.Repositories;

/// <summary>
/// Dynamo DB Access Service
/// </summary>
public interface IScrumOrganizationRepository
{
    /// <summary>
    /// Write an organization to Dynamo Db.
    /// </summary>
    /// <param name="organization">The organization to write.</param>
    /// <returns><see cref="Task"/></returns>
    Task WriteOrganizationToDynamoDbAsync(IOrganization organization);

    /// <summary>
    /// Get all organizations from dynamo db.
    /// </summary>
    /// <returns><see cref="Task"/>A list of Organizations</returns>
    Task<List<Organization>> GetAllOrganizations();

    /// <summary>
    /// Gets a scrum organization by its id.
    /// </summary>
    /// <param name="id">The id of the scrum organization to get.</param>
    /// <returns><see cref="Task"/></returns>
    Task<Organization> GetScrumOrganizationByIdAsync(Guid id);

    /// <summary>
    /// Updates an existing organization in dynamo db.
    /// </summary>
    /// <param name="updateScrumOrganizationArgs">The organization update args</param>
    /// <returns><see cref="Task"/></returns>
    Task UpdateExistingOrganization(UpdateScrumOrganizationArgs updateScrumOrganizationArgs);
}