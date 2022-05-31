using BackendTeamWebApiService.Utilities;

namespace BackendTeamWebApiService.Services;

using Amazon.DynamoDBv2.Model;
using Models;

/// <summary>
/// Organization Service
/// </summary>
internal sealed class OrganizationService : IOrganizationService
{
    private readonly ILogger<OrganizationService> _logger;

    private readonly ICreateOrganizationService _createOrganizationService;

    private readonly IDynamoDbAccessService _dynamoDbAccessService;

    /// <summary>
    /// Initializes an instance of <see cref="OrganizationService"/>
    /// </summary>
    /// <param name="logger">Organization Service logger</param>
    /// <param name="createOrganizationService">Create organization process</param>
    /// <param name="dynamoDbAccessService">Dynamo DB Access Service.</param>
    public OrganizationService(ILogger<OrganizationService> logger,
        ICreateOrganizationService createOrganizationService,
        IDynamoDbAccessService dynamoDbAccessService)
    {
        _logger = logger;
        _createOrganizationService = createOrganizationService;
        _dynamoDbAccessService = dynamoDbAccessService;
    }

    public async Task CreateScrumOrganizationAsync(AddOrganizationArgs addOrganizationArgs)
    {
        IOrganization newOrganization = await this._createOrganizationService.CreateScrumTeamOrganizationAsync(addOrganizationArgs);
        
        
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

    /// <inheritdoc cref="IOrganizationService"/>
    public bool Ping()
    {
        return true;
    }

    /// <inheritdoc cref="IOrganizationService"/>
    public async Task<List<Organization>> GetAllOrganizationsAsync()
    {
        Organization organization = new Organization
        {
            Id = Guid.NewGuid(),
            Name = "Organization-1"
        };
        
        Organization organizationSecond = new Organization
        {
            Id = Guid.NewGuid(),
            Name = "Organization-2"
        };

        return await Task.FromResult(new List<Organization>
        {
            organization,
            organizationSecond
        });
    }
}