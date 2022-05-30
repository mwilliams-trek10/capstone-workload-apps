namespace ReportingTeamWebApiService.Services;

/// <summary>
/// EFS Writer
/// </summary>
public interface IEfsAccessService
{
    /// <summary>
    /// Writes the organization report to EFS.
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    Task WriteOrganizationReportToEfs();
}