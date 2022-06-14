

namespace BackendTeamWebApiService.Services;

using Models;
using Repositories;

/// <summary>
/// Organization Service
/// </summary>
internal sealed class ScrumOrganizationService : IScrumOrganizationService
{
    private readonly ILogger<ScrumOrganizationService> _logger;

    private readonly ICreateOrganizationService _createOrganizationService;

    private readonly IScrumOrganizationRepository _scrumOrganizationRepository;

    private readonly IPersonRepository _personRepository;

    private readonly ITeamMemberRepository _teamMemberRepository;
    
    private readonly ITeamRepository _teamRepository;

    /// <summary>
    /// Initializes an instance of <see cref="ScrumOrganizationService"/>
    /// </summary>
    /// <param name="logger">Organization Service logger</param>
    /// <param name="createOrganizationService">Create organization process</param>
    /// <param name="scrumOrganizationRepository">Scrum organization repository</param>
    /// <param name="personRepository">Person Repository</param>
    /// <param name="teamMemberRepository">Team Member repository</param>
    /// <param name="teamRepository">Team repository</param>
    public ScrumOrganizationService(ILogger<ScrumOrganizationService> logger,
        ICreateOrganizationService createOrganizationService,
        IScrumOrganizationRepository scrumOrganizationRepository,
        IPersonRepository personRepository,
        ITeamMemberRepository teamMemberRepository,
        ITeamRepository teamRepository)
    {
        _logger = logger;
        _createOrganizationService = createOrganizationService;
        _scrumOrganizationRepository = scrumOrganizationRepository;
        _personRepository = personRepository;
        _teamMemberRepository = teamMemberRepository;
        _teamRepository = teamRepository;
    }

    /// <inheritdoc cref="IScrumOrganizationService"/>
    public async Task CreateScrumOrganizationAsync(AddScrumOrganizationArgs addScrumOrganizationArgs)
    {
        IOrganization newOrganization = await this._createOrganizationService.CreateScrumTeamOrganizationAsync(addScrumOrganizationArgs);
        await this._scrumOrganizationRepository.WriteOrganizationToDynamoDbAsync(newOrganization);
        await this._personRepository.WriteListOfPersonsToDynamoDbAsync(newOrganization.Supervisors, "capstone-supervisors");
        await this._personRepository.WriteListOfPersonsToDynamoDbAsync(newOrganization.Supervisors, "capstone-scrummasters");
        await this._teamMemberRepository.WriteTeamMembersToDynamoDbAsync(newOrganization.TeamMembers);
        await this._teamRepository.WriteTeamsToDynamoDbAsync(newOrganization.Teams);
    }

    /// <inheritdoc cref="IScrumOrganizationService"/>
    public bool Ping()
    {
        return true;
    }

    /// <inheritdoc cref="IScrumOrganizationService"/>
    public async Task<List<Organization>> GetAllOrganizationsAsync()
    {
        return await _scrumOrganizationRepository.GetAllOrganizations();
    }

    /// <inheritdoc cref="IScrumOrganizationService"/>
    public async Task<Organization> GetScrumOrganizationByIdAsync(Guid id)
    {
        return await _scrumOrganizationRepository.GetScrumOrganizationByIdAsync(id);
    }

    /// <inheritdoc cref="IScrumOrganizationService"/>
    public async Task UpdateScrumOrganizationAsync(UpdateScrumOrganizationArgs updateScrumOrganizationArgs)
    {
        await _scrumOrganizationRepository.UpdateExistingOrganization(updateScrumOrganizationArgs);
    }
}