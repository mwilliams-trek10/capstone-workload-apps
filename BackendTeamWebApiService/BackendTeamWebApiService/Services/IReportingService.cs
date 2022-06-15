namespace BackendTeamWebApiService.Services;

/// <summary>
/// Reporting Service.
/// </summary>
public interface IReportingService
{
    /// <summary>
    /// Writes an organization report.
    /// </summary>
    /// <param name="organizationId">The organization id.</param>
    /// <returns><see cref="Task"/></returns>
    Task WriteOrganizationReport(Guid organizationId);
}