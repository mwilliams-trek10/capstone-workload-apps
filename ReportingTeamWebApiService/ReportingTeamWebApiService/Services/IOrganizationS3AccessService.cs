namespace ReportingTeamWebApiService.Services;

/// <summary>
/// Organization S3 Writer
/// </summary>
public interface IOrganizationS3AccessService
{
    /// <summary>
    /// Writes a certain organization to S3.
    /// </summary>
    /// <param name="organizationId">The id of the organization to write.</param>
    /// <returns><see cref="Task"/></returns>
    Task WriteOrganizationByIdAsync(int organizationId);
}