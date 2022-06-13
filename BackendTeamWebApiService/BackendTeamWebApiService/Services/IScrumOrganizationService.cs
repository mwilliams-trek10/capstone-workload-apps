

namespace BackendTeamWebApiService.Services;

using Models;

/// <summary>
/// Organization Service.
/// </summary>
public interface IScrumOrganizationService
{
    /// <summary>
    /// Ping
    /// </summary>
    /// <returns><see cref="bool"/>True</returns>
    bool Ping();

    /// <summary>
    /// Creates a new Scrum Organization.
    /// </summary>
    /// <param name="addScrumOrganizationArgs">The new organizations args.</param>
    /// <returns><see cref="Task"/></returns>
    Task CreateScrumOrganizationAsync(AddScrumOrganizationArgs addScrumOrganizationArgs);

    /// <summary>
    /// Gets all of the organizations.
    /// </summary>
    /// <returns>List of organizations.</returns>
    Task<List<Organization>> GetAllOrganizationsAsync();

    /// <summary>
    /// Gets a scrum organization by id.
    /// </summary>
    /// <returns>A <see cref="Organization"/></returns>
    Task<Organization> GetScrumOrganizationByIdAsync(Guid id);

    /// <summary>
    /// Updates an existing Scrum Organization.
    /// </summary>
    /// <param name="updateScrumOrganizationArgs">The update organization args</param>
    /// <returns><see cref="Task"/></returns>
    Task UpdateScrumOrganizationAsync(UpdateScrumOrganizationArgs updateScrumOrganizationArgs);
}