namespace ReportingTeamWebApiService.Services;

/// <summary>
/// S3 Reader Writer Service
/// </summary>
public interface IS3AccessService
{
    /// <summary>
    /// Writes the organization report to an S3 bucket.
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    Task WriteOrganizationReportToS3();
}