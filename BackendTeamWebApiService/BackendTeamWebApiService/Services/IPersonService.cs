using BackendTeamWebApiService.Models;

namespace BackendTeamWebApiService.Services;

/// <summary>
/// Person Service
/// </summary>
public interface IPersonService
{
    /// <summary>
    /// Gets all scrum masters for an organization
    /// </summary>
    /// <param name="organizationId">The organization id</param>
    /// <returns><see cref="Task"/><see cref="List{T}"/><see cref="ScrumMaster"/></returns>
    Task<List<ScrumMaster>> GetScrumMastersByOrganizationId(Guid organizationId);
    
    /// <summary>
    /// Gets a scrum master based on the table ids.
    /// </summary>
    /// <param name="organizationId">The organization Id</param>
    /// <param name="scrumMasterId">The scrum master id</param>
    /// <returns><see cref="Task"/><see cref="ScrumMaster"/></returns>
    Task<ScrumMaster> GetScrumMastersByIds(Guid organizationId, Guid scrumMasterId);
    
    /// <summary>
    /// Gets all supervisors for an organization
    /// </summary>
    /// <param name="organizationId">The organization id</param>
    /// <returns><see cref="Task"/><see cref="List{T}"/><see cref="ScrumMaster"/></returns>
    Task<List<Supervisor>> GetSupervisorsByOrganizationId(Guid organizationId);
    
    /// <summary>
    /// Gets a supervisors based on the table ids.
    /// </summary>
    /// <param name="organizationId">The organization Id</param>
    /// <param name="scrumMasterId">The scrum master id</param>
    /// <returns><see cref="Task"/><see cref="ScrumMaster"/></returns>
    Task<Supervisor> GetSupervisorsByIds(Guid organizationId, Guid scrumMasterId);
}