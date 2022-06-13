namespace BackendTeamWebApiService.Services;

using Amazon.DynamoDBv2.Model;
using Models;
using Utilities;

/// <summary>
/// Organization Service
/// </summary>
internal sealed class ScrumOrganizationService : IScrumOrganizationService
{
    private readonly ILogger<ScrumOrganizationService> _logger;

    private readonly ICreateOrganizationService _createOrganizationService;

    private readonly IDynamoDbAccessService _dynamoDbAccessService;

    /// <summary>
    /// Initializes an instance of <see cref="ScrumOrganizationService"/>
    /// </summary>
    /// <param name="logger">Organization Service logger</param>
    /// <param name="createOrganizationService">Create organization process</param>
    /// <param name="dynamoDbAccessService">Dynamo DB Access Service.</param>
    public ScrumOrganizationService(ILogger<ScrumOrganizationService> logger,
        ICreateOrganizationService createOrganizationService,
        IDynamoDbAccessService dynamoDbAccessService)
    {
        _logger = logger;
        _createOrganizationService = createOrganizationService;
        _dynamoDbAccessService = dynamoDbAccessService;
    }

    public async Task CreateScrumOrganizationAsync(AddScrumOrganizationArgs addScrumOrganizationArgs)
    {
        IOrganization newOrganization = await this._createOrganizationService.CreateScrumTeamOrganizationAsync(addScrumOrganizationArgs);
        await this._dynamoDbAccessService.WriteOrganizationToDynamoDbAsync(newOrganization);
    }

    private async Task WriteOrganizationToDynamoDb(IOrganization organization)
    {
        
    }

    private async Task GetTeamMembersToWrite()
    {
        
    }

    private Dictionary<string, AttributeValue> GetTeamMember(TeamMember teamMember)
    {
        Dictionary<string, AttributeValue> teamMemberToWrite = new Dictionary<string, AttributeValue>();
        teamMemberToWrite.SetPerson(teamMember);
        return teamMemberToWrite;
    }

    /// <inheritdoc cref="IScrumOrganizationService"/>
    public bool Ping()
    {
        return true;
    }

    /// <inheritdoc cref="IScrumOrganizationService"/>
    public async Task<List<Organization>> GetAllOrganizationsAsync()
    {
        // Organization organization = new Organization
        // {
        //     Id = Guid.NewGuid(),
        //     Name = "Organization-1"
        // };
        //
        // Organization organizationSecond = new Organization
        // {
        //     Id = Guid.NewGuid(),
        //     Name = "Organization-2"
        // };
        //
        // return await Task.FromResult(new List<Organization>
        // {
        //     organization,
        //     organizationSecond
        // });

        return await _dynamoDbAccessService.GetAllOrganizations();
    }

    /// <inheritdoc cref="IScrumOrganizationService"/>
    public async Task<Organization> GetScrumOrganizationByIdAsync(Guid id)
    {
        return await _dynamoDbAccessService.GetScrumOrganizationByIdAsync(id);
    }

    /// <inheritdoc cref="IScrumOrganizationService"/>
    public async Task UpdateScrumOrganizationAsync(UpdateScrumOrganizationArgs updateScrumOrganizationArgs)
    {
        await _dynamoDbAccessService.UpdateExistingOrganization(updateScrumOrganizationArgs);
    }
}