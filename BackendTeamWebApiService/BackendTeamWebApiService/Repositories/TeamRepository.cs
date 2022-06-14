using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using BackendTeamWebApiService.Models;
using Microsoft.Extensions.Options;

namespace BackendTeamWebApiService.Repositories;

/// <inheritdoc cref="ITeamRepository"/>
public sealed class TeamRepository : ITeamRepository
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
            Dictionary<string, List<WriteRequest>> batchWriteItem = new() {{"capstone-teams", writeRequests}};
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
}