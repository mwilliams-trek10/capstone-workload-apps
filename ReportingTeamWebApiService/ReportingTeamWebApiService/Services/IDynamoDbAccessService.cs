namespace ReportingTeamWebApiService.Services;

/// <summary>
/// Dynamo DB Access Service
/// </summary>
public interface IDynamoDbAccessService
{
    /// <summary>
    /// Gets an organization by Id
    /// </summary>
    /// <param name="organizationId">The organization Id</param>
    /// <returns><see cref="Task"/></returns>
    Task GetOrganizationByIdAsync(int organizationId);
}