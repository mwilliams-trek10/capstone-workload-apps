using BackendTeamWebApiService.Models;
using BackendTeamWebApiService.Repositories;
using BackendTeamWebApiService.Utilities;

namespace BackendTeamWebApiService.Services;

/// <inheritdoc cref="IPersonService"/>
internal sealed class PersonService : IPersonService
{private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    
    /// <inheritdoc cref="IPersonService"/>
    public async Task<List<ScrumMaster>> GetScrumMastersByOrganizationId(Guid organizationId)
    {
        return await GetPersonsByOrganizationId<ScrumMaster>(organizationId, Constants.CapstoneScrumMastersTableName);
    }

    /// <inheritdoc cref="IPersonService"/>
    public async Task<ScrumMaster> GetScrumMastersByIds(Guid organizationId, Guid scrumMasterId)
    {
        return await GetPersonByIds<ScrumMaster>(organizationId, scrumMasterId, Constants.CapstoneScrumMastersTableName);
    }
    
    /// <inheritdoc cref="IPersonService"/>
    public async Task<List<Supervisor>> GetSupervisorsByOrganizationId(Guid organizationId)
    {
        return await GetPersonsByOrganizationId<Supervisor>(organizationId, Constants.CapstoneSupervisorsTableName);
    }

    /// <inheritdoc cref="IPersonService"/>
    public async Task<Supervisor> GetSupervisorsByIds(Guid organizationId, Guid scrumMasterId)
    {
        return await GetPersonByIds<Supervisor>(organizationId, scrumMasterId, Constants.CapstoneSupervisorsTableName);
    }

    /// <summary>
    /// Gets all teams for an organization
    /// </summary>
    /// <param name="organizationId">The organization id</param>
    /// <param name="tableName">The table name</param>
    /// <returns><see cref="Task"/><see cref="List{T}"/><see cref="Team"/></returns>
    private async Task<List<T>> GetPersonsByOrganizationId<T>(Guid organizationId, string tableName) where T: IPerson, new()
    {
        return await _personRepository.GetPersonsByOrganizationId<T>(organizationId, tableName);
    }

    /// <summary>
    /// Gets a Team based on the table ids.
    /// </summary>
    /// <param name="organizationId">The organization Id</param>
    /// <param name="personId">The person id</param>
    /// <param name="tableName">The table name</param>
    /// <returns><see cref="Task"/><see cref="Team"/></returns>
    private async Task<T> GetPersonByIds<T>(Guid organizationId, Guid personId, string tableName) where T: IPerson, new()
    {
        return await _personRepository.GetPersonByIds<T>(organizationId, personId, tableName);
    }
}