namespace ReportingTeamWebApiService.Services;

/// <summary>
/// Organization Report Service.
/// </summary>
public interface IOrganizationReportService
{
    /// <summary>
    /// Ping
    /// </summary>
    /// <returns><see cref="bool"/>True</returns>
    Task<bool> PingAsync();
}