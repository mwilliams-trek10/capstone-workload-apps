namespace BackendTeamWebApiService.Services;

using Models;

/// <summary>
/// Create Organization Service
/// </summary>
public interface ICreateOrganizationService
{
    /// <summary>
    /// Create Scrum Team Organization
    /// </summary>
    /// <returns>
    /// <see cref="Task{TResult}"/>
    /// <see cref="IOrganization"/>
    /// </returns>
    Task<IOrganization> CreateScrumTeamOrganizationAsync(AddOrganizationArgs addOrganizationArgs);
}