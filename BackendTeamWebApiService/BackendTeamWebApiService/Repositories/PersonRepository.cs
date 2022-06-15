namespace BackendTeamWebApiService.Repositories;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Models;
using Microsoft.Extensions.Options;

/// <inheritdoc cref="IPersonRepository"/>
internal class PersonRepository : IPersonRepository
{
    protected readonly ILogger<ScrumOrganizationRepository> Logger;

    protected readonly AmazonDynamoDBClient AmazonDynamoDbClient;

    public PersonRepository(ILogger<ScrumOrganizationRepository> logger,
        IOptions<AmazonDynamoDBClient> amazonDynamoDbClient)
    {
        Logger = logger;
        AmazonDynamoDbClient = amazonDynamoDbClient.Value;
    }
    
    /// <inheritdoc cref="IPersonRepository"/>
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

    /// <inheritdoc cref="IPersonRepository"/>
    public async Task<List<T>> GetPersonsByOrganizationId<T>(Guid organizationId, string tableName) where T : IPerson, new()
    {
        try
        {
            QueryRequest request = new QueryRequest
            {
                TableName = tableName,
                //AttributesToGet = {"Id", "TeamNumber", "OrganizationId", "ScrumMasterId", "TeamName"},
                KeyConditionExpression = "OrganizationId = :organizationId",
                ExpressionAttributeValues =
                {
                    {":organizationId", new AttributeValue(organizationId.ToString())}
                }
            };
            var response = await AmazonDynamoDbClient.QueryAsync(request);

            return ConvertResponse<T>(response.Items);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            Logger.LogError(ex.StackTrace);
            throw;
        }
    }

    /// <inheritdoc cref="IPersonRepository"/>
    public async Task<T> GetPersonByIds<T>(Guid organizationId, Guid id, string tableName) where T : IPerson, new()
    {
        try
        {
            GetItemRequest request = new GetItemRequest
            {
                TableName = tableName,
                Key = 
                {
                    {"OrganizationId", new AttributeValue(organizationId.ToString())},
                    {"Id", new AttributeValue(id.ToString())}
                }
            };
            var response = await AmazonDynamoDbClient.GetItemAsync(request);

            List<T> responseList = ConvertResponse<T>(new List<Dictionary<string, AttributeValue>> {response.Item });
            return responseList.First();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            Logger.LogError(ex.StackTrace);
            throw;
        }
    }

    protected async Task ProcessChunkedListOfPersons(IEnumerable<Dictionary<string, AttributeValue>[]> chunks, string tableName)
    {
        foreach (var chunk in chunks)
        {
            List<WriteRequest> writeRequests = GetWriteRequests(chunk);
            await WriteBatchToDynamoDbAsync(writeRequests, tableName);
        }
    }

    private static List<WriteRequest> GetWriteRequests(Dictionary<string, AttributeValue>[] people)
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

    private async Task WriteBatchToDynamoDbAsync(List<WriteRequest> writeRequests, string tableName)
    {
        try
        {
            Dictionary<string, List<WriteRequest>> batchWriteItem = new() {{tableName, writeRequests}};
            BatchWriteItemRequest batchWriteItemRequest = new BatchWriteItemRequest(batchWriteItem);
            await AmazonDynamoDbClient.BatchWriteItemAsync(batchWriteItemRequest);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            Logger.LogError(ex.StackTrace);
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
    
    private List<T> ConvertResponse<T>(List<Dictionary<string, AttributeValue>> responseItems) where T: IPerson, new()
    {
        List<T> list = new List<T>();

        foreach (Dictionary<string, AttributeValue> item in responseItems)
        {
            list.Add(ConvertDictionaryAttributesToTeam<T>(item));
        }

        return list;
    }

    private T ConvertDictionaryAttributesToTeam<T>(Dictionary<string, AttributeValue> attributeValues) where T: IPerson, new()
    {
        return new T
        {
            Id = new Guid(attributeValues["Id"].S),
            OrganizationEmployeeNumber = attributeValues["OrganizationEmployeeNumber"].S,
            OrganizationId = new Guid(attributeValues["OrganizationId"].S),
            FirstName = attributeValues["FirstName"].S,
            LastName = attributeValues["LastName"].S
        };
    }
}