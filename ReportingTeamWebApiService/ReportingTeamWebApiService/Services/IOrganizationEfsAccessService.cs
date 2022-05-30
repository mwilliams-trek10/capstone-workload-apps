namespace ReportingTeamWebApiService.Services;

/// <summary>
/// Organization Efs Access Service
/// </summary>
public interface IOrganizationEfsAccessService
{
    /// <summary>
    /// Writes a certain organization to EFS.
    /// </summary>
    /// <param name="organizationId">The id of the organization to write.</param>
    /// <returns><see cref="Task"/></returns>
    public async Task WriteOrganizationByIdAsync(int organizationId)
    {
        
    }
}