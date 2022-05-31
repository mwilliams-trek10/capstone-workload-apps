

namespace BackendTeamWebApiService.Services;

using Models;

/// <summary>
/// Organization Service.
/// </summary>
public interface IOrganizationService
{
    /// <summary>
    /// Ping
    /// </summary>
    /// <returns><see cref="bool"/>True</returns>
    bool Ping();

    /// <summary>
    /// Creates a new Scrum Organization.
    /// </summary>
    /// <param name="addOrganizationArgs">The new organizations args.</param>
    /// <returns><see cref="Task"/></returns>
    Task CreateScrumOrganizationAsync(AddOrganizationArgs addOrganizationArgs);

    /// <summary>
    /// Gets all of the organizations.
    /// </summary>
    /// <returns>List of organizations.</returns>
    Task<List<Organization>> GetAllOrganizationsAsync();
}