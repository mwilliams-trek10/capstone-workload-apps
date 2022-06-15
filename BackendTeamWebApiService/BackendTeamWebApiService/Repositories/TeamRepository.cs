using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using BackendTeamWebApiService.Models;
using BackendTeamWebApiService.Utilities;
using Microsoft.Extensions.Options;

namespace BackendTeamWebApiService.Repositories;

/// <inheritdoc cref="ITeamRepository"/>
internal sealed class TeamRepository : ITeamRepository
{
    private readonly ILogger<TeamRepository> _logger;

    private readonly AmazonDynamoDBClient _amazonDynamoDbClient;

    /// <summary>
    /// Initializes a new instance of <see cref="TeamRepository"/>
    /// </summary>
    /// <param name="logger">The logger</param>
    /// <param name="amazonDynamoDbClient">The amazon dynamo db client</param>
    public TeamRepository(ILogger<TeamRepository> logger,
        IOptions<AmazonDynamoDBClient> amazonDynamoDbClient)
    {
        _logger = logger;
        _amazonDynamoDbClient = amazonDynamoDbClient.Value;
    }
    
    /// <inheritdoc cref="ITeamRepository"/>
    public async Task WriteTeamsToDynamoDbAsync(List<Team> teams)
    {
        List<Dictionary<string, AttributeValue>> teamsAttributes = new List<Dictionary<string, AttributeValue>>(teams.Count);

        foreach (var team in teams)
        {
            teamsAttributes.Add(GetTeamAttributes(team));
        }

        var chunkedTeams = teamsAttributes.Chunk(25);

        await ProcessChunkedListOfTeams(chunkedTeams);
    }
    
    public async Task<List<Team>> GetTeamsByOrganizationId(Guid organizationId)
    {
        try
        {
            QueryRequest request = new QueryRequest
            {
                TableName = Constants.CapstoneTeamTableName,
                KeyConditionExpression = "OrganizationId = :organizationId",
                ExpressionAttributeValues =
                {
                    {":organizationId", new AttributeValue(organizationId.ToString())}
                }
            };
            var response = await _amazonDynamoDbClient.QueryAsync(request);

            return ConvertResponseToTeams(response.Items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);
            throw;
        }
    }

    public async Task<Team> GetTeamByIds(Guid organizationId, Guid teamId)
    {
        try
        {
            GetItemRequest request = new GetItemRequest
            {
                TableName = Constants.CapstoneTeamTableName,
                Key = 
                {
                    {"OrganizationId", new AttributeValue(organizationId.ToString())},
                    {"Id", new AttributeValue(teamId.ToString())}
                }
            };
            var response = await _amazonDynamoDbClient.GetItemAsync(request);

            List<Team> organizations = ConvertResponseToTeams(new List<Dictionary<string, AttributeValue>> {response.Item });
            return organizations.First();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);
            throw;
        }
    }
    
    
    private async Task ProcessChunkedListOfTeams(IEnumerable<Dictionary<string, AttributeValue>[]> chunks)
    {
        foreach (var chunk in chunks)
        {
            List<WriteRequest> writeRequests = GetWriteRequests(chunk);
            await WriteBatchToDynamoDbAsync(writeRequests);
        }
    }

    private static List<WriteRequest> GetWriteRequests(Dictionary<string, AttributeValue>[] teams)
    {
        List<WriteRequest> writeRequests = new List<WriteRequest>(teams.Length);
        foreach (var team in teams)
        {
            PutRequest putRequest = new PutRequest(team);
            WriteRequest writeRequest = new WriteRequest(putRequest);
            writeRequests.Add(writeRequest);
        }

        return writeRequests;
    }

    private async Task WriteBatchToDynamoDbAsync(List<WriteRequest> writeRequests)
    {
        try
        {
            Dictionary<string, List<WriteRequest>> batchWriteItem = new() {{Constants.CapstoneTeamTableName, writeRequests}};
            BatchWriteItemRequest batchWriteItemRequest = new BatchWriteItemRequest(batchWriteItem);
            await _amazonDynamoDbClient.BatchWriteItemAsync(batchWriteItemRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);
            throw;
        }
    }
    
    private static Dictionary<string, AttributeValue> GetTeamAttributes(Team team)
    {
        return new Dictionary<string, AttributeValue>
        {
            {"Id", new AttributeValue(team.Id.ToString())},
            {"TeamNumber", new AttributeValue(team.TeamNumber)},
            {"OrganizationId", new AttributeValue(team.OrganizationId.ToString())},
            {"ScrumMasterId", new AttributeValue(team.ScrumMasterId.ToString())},
            {"TeamName", new AttributeValue(team.TeamName)}
        };
    }
    
    private List<Team> ConvertResponseToTeams(List<Dictionary<string, AttributeValue>> responseItems)
    {
        List<Team> organizations = new List<Team>();

        foreach (Dictionary<string, AttributeValue> item in responseItems)
        {
            organizations.Add(ConvertDictionaryAttributesToTeam(item));
        }

        return organizations;
    }

    private Team ConvertDictionaryAttributesToTeam(Dictionary<string, AttributeValue> attributeValues)
    {
        return new Team
        {
            Id = new Guid(attributeValues["Id"].S),
            TeamNumber = attributeValues["TeamNumber"].S,
            OrganizationId = new Guid(attributeValues["OrganizationId"].S),
            ScrumMasterId = new Guid(attributeValues["ScrumMasterId"].S),
            TeamName = attributeValues["TeamName"].S
        };
    }
}