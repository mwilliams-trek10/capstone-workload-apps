using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using BackendTeamWebApiService.Models;
using Microsoft.Extensions.Options;

namespace BackendTeamWebApiService.Repositories;

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

        await ProcessChunkedListOfPersons(chunkedPeople, "capstone-teammembers");
    }
    
    private static Dictionary<string, AttributeValue> GetTeamMemberAttributes(TeamMember teamMember)
    {
        Dictionary<string, AttributeValue> teamMemberAttributes = GetPersonAttributes(teamMember);
        teamMemberAttributes["SupervisorId"] = new AttributeValue(teamMember.SupervisorId.ToString());
        teamMemberAttributes["TeamId"] = new AttributeValue(teamMember.TeamId.ToString());
        return teamMemberAttributes;
    }
}