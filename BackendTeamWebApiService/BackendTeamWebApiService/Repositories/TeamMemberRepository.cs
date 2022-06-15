namespace BackendTeamWebApiService.Repositories;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Models;
using Utilities;
using Microsoft.Extensions.Options;

internal sealed class TeamMemberRepository : PersonRepository, ITeamMemberRepository
{
    public TeamMemberRepository(ILogger<ScrumOrganizationRepository> logger,
        IOptions<AmazonDynamoDBClient> amazonDynamoDbClient) : base(logger, amazonDynamoDbClient)
    {
    }

    public async Task WriteTeamMembersToDynamoDbAsync(IReadOnlyCollection<TeamMember> teamMembers)
    {
        List<Dictionary<string, AttributeValue>> peopleAttributes = new List<Dictionary<string, AttributeValue>>(teamMembers.Count);

        foreach (var teamMember in teamMembers)
        {
            peopleAttributes.Add(GetTeamMemberAttributes(teamMember));
        }

        var chunkedPeople = peopleAttributes.Chunk(25);

        await ProcessChunkedListOfPersons(chunkedPeople, Constants.CapstoneTeamMembersTableName);
    }

    public async Task<List<TeamMember>> GetTeamMembersByOrganizationId(Guid organizationId)
    {
        try
        {
            QueryRequest request = new QueryRequest
            {
                TableName = Constants.CapstoneTeamMembersTableName,
                KeyConditionExpression = "OrganizationId = :organizationId",
                ExpressionAttributeValues =
                {
                    {":organizationId", new AttributeValue(organizationId.ToString())}
                }
            };
            var response = await AmazonDynamoDbClient.QueryAsync(request);

            return ConvertResponseToTeamMembers(response.Items);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            Logger.LogError(ex.StackTrace);
            throw;
        }
    }

    public async Task<TeamMember> GetTeamMembersByIds(Guid organizationId, Guid teamMemberId)
    {
        try
        {
            GetItemRequest request = new GetItemRequest
            {
                TableName = Constants.CapstoneTeamMembersTableName,
                Key = 
                {
                    {"OrganizationId", new AttributeValue(organizationId.ToString())},
                    {"Id", new AttributeValue(teamMemberId.ToString())}
                }
            };
            var response = await AmazonDynamoDbClient.GetItemAsync(request);

            List<TeamMember> teamMembers = ConvertResponseToTeamMembers(new List<Dictionary<string, AttributeValue>> {response.Item });
            return teamMembers.First();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            Logger.LogError(ex.StackTrace);
            throw;
        }
    }
    
    private static Dictionary<string, AttributeValue> GetTeamMemberAttributes(TeamMember teamMember)
    {
        Dictionary<string, AttributeValue> teamMemberAttributes = GetPersonAttributes(teamMember);
        teamMemberAttributes["SupervisorId"] = new AttributeValue(teamMember.SupervisorId.ToString());
        teamMemberAttributes["TeamId"] = new AttributeValue(teamMember.TeamId.ToString());
        return teamMemberAttributes;
    }
    
    private List<TeamMember> ConvertResponseToTeamMembers(List<Dictionary<string, AttributeValue>> responseItems)
    {
        List<TeamMember> teamMembers = new List<TeamMember>();

        foreach (Dictionary<string, AttributeValue> item in responseItems)
        {
            teamMembers.Add(ConvertDictionaryAttributesToTeamMembers(item));
        }

        return teamMembers;
    }

    private TeamMember ConvertDictionaryAttributesToTeamMembers(Dictionary<string, AttributeValue> attributeValues)
    {
        return new TeamMember
        {
            Id = new Guid(attributeValues["Id"].S),
            OrganizationEmployeeNumber = attributeValues["OrganizationEmployeeNumber"].S,
            OrganizationId = new Guid(attributeValues["OrganizationId"].S),
            FirstName = attributeValues["FirstName"].S,
            LastName = attributeValues["LastName"].S,
            SupervisorId = new Guid(attributeValues["SupervisorId"].S),
            TeamId = new Guid(attributeValues["TeamId"].S)
        };
    }
}