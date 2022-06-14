using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using BackendTeamWebApiService.Models;
using Microsoft.Extensions.Options;

namespace BackendTeamWebApiService.Repositories;

internal class PersonRepository : IPersonRepository
{
    protected readonly ILogger<ScrumOrganizationRepository> _logger;

    protected readonly AmazonDynamoDBClient _amazonDynamoDbClient;

    public PersonRepository(ILogger<ScrumOrganizationRepository> logger,
        IOptions<AmazonDynamoDBClient> amazonDynamoDbClient)
    {
        _logger = logger;
        _amazonDynamoDbClient = amazonDynamoDbClient.Value;
    }
    
    public async Task WriteListOfPersonsToDynamoDbAsync<T>(List<T> persons, string tableName) where T: IPerson
    {
        List<Dictionary<string, AttributeValue>> peopleAttributes = new List<Dictionary<string, AttributeValue>>(persons.Count);

        foreach (var person in persons)
        {
            peopleAttributes.Add(GetPersonAttributes(person));
        }

        var chunkedPeople = peopleAttributes.Chunk(25);

        await ProcessChunkedListOfPersons(chunkedPeople, tableName);
    }

    protected async Task ProcessChunkedListOfPersons(IEnumerable<Dictionary<string, AttributeValue>[]> chunks, string tableName)
    {
        foreach (var chunk in chunks)
        {
            List<WriteRequest> writeRequests = GetWriteRequests(chunk);
            await WriteBatchToDynamoDbAsync(writeRequests, tableName);
        }
    }

    protected static List<WriteRequest> GetWriteRequests(Dictionary<string, AttributeValue>[] people)
    {
        List<WriteRequest> writeRequests = new List<WriteRequest>(people.Length);
        foreach (var person in people)
        {
            PutRequest putRequest = new PutRequest(person);
            WriteRequest writeRequest = new WriteRequest(putRequest);
            writeRequests.Add(writeRequest);
        }

        return writeRequests;
    }

    protected async Task WriteBatchToDynamoDbAsync(List<WriteRequest> writeRequests, string tableName)
    {
        try
        {
            Dictionary<string, List<WriteRequest>> batchWriteItem = new() {{tableName, writeRequests}};
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

    protected static Dictionary<string, AttributeValue> GetPersonAttributes<T>(T person) where T: IPerson
    {
        return new Dictionary<string, AttributeValue>
        {
            {"Id", new AttributeValue(person.Id.ToString())},
            {"OrganizationEmployeeNumber", new AttributeValue(person.OrganizationEmployeeNumber)},
            {"OrganizationId", new AttributeValue(person.OrganizationId.ToString())},
            {"FirstName", new AttributeValue(person.FirstName)},
            {"LastName", new AttributeValue(person.LastName)}
        };
    }
}