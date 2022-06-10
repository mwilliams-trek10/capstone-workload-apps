namespace BackendTeamWebApiService.Services;

using Models;

/// <summary>
/// Dynamo DB Access Service
/// </summary>
public interface IDynamoDbAccessService
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
}