namespace BackendTeamWebApiService.Repositories;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Models;
using Microsoft.Extensions.Options;
using Utilities;

/// <inheritdoc cref="IScrumOrganizationRepository"/>
internal sealed class ScrumOrganizationRepository : IScrumOrganizationRepository
{   
    private readonly ILogger<ScrumOrganizationRepository> _logger;

    private readonly AmazonDynamoDBClient _amazonDynamoDbClient;

    public ScrumOrganizationRepository(ILogger<ScrumOrganizationRepository> logger,
        IOptions<AmazonDynamoDBClient> amazonDynamoDbClient)
    {
        _logger = logger;
        _amazonDynamoDbClient = amazonDynamoDbClient.Value;
    }
    
    public async Task WriteOrganizationToDynamoDbAsync(IOrganization organization)
    {
        Dictionary<string, AttributeValue> organizationAttributes = GetOrganizationAttributes(organization);
        
        try
        {
            await _amazonDynamoDbClient.PutItemAsync(Constants.OrganizationTableName, organizationAttributes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);
            throw;
        }
    }

    private static Dictionary<string, AttributeValue> GetOrganizationAttributes(IOrganization organization)
    {
        return new Dictionary<string, AttributeValue>
        {
            {"Id", new AttributeValue(organization.Id.ToString())},
            {"Name", new AttributeValue(organization.Name)}
        };
    }

    public async Task<List<Organization>> GetAllOrganizations()
    {
        try
        {
            ScanRequest request = new ScanRequest
            {
                TableName = Constants.OrganizationTableName,
                AttributesToGet = {"Id", "Name"}
            };
            var response = await _amazonDynamoDbClient.ScanAsync(request);

            return ConvertResponseToOrganizations(response.Items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);
            throw;
        }
    }

    public async Task<Organization> GetScrumOrganizationByIdAsync(Guid id)
    {
        try
        {
            GetItemRequest request = new GetItemRequest
            {
                TableName = Constants.OrganizationTableName,
                AttributesToGet = {"Id", "Name"},
                Key = 
                {
                    {"Id", new AttributeValue(id.ToString())}
                }
            };
            var response = await _amazonDynamoDbClient.GetItemAsync(request);

            List<Organization> organizations = ConvertResponseToOrganizations(new List<Dictionary<string, AttributeValue>> {response.Item });
            return organizations.First();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);
            throw;
        }
    }

    public async Task UpdateExistingOrganization(UpdateScrumOrganizationArgs updateScrumOrganizationArgs)
    {
        UpdateItemRequest updateItemRequest = new UpdateItemRequest
        {
            TableName = Constants.OrganizationTableName,
            Key = new Dictionary<string, AttributeValue>
            {
                {"Id", new AttributeValue
                {
                    S = updateScrumOrganizationArgs.Id.ToString()
                }}
            },
            UpdateExpression = "set #nm = :nval",
            ExpressionAttributeNames = new Dictionary<string, string>
            {
                {"#nm", "Name"}
            },
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                {":nval", new AttributeValue{ S =updateScrumOrganizationArgs.Name }}
            }
        };
        
        _logger.LogInformation(updateItemRequest.TableName);
        _logger.LogInformation(updateItemRequest.Key.First().ToString());
        _logger.LogInformation(updateItemRequest.UpdateExpression);

        await _amazonDynamoDbClient.UpdateItemAsync(updateItemRequest);
    }

    private List<Organization> ConvertResponseToOrganizations(List<Dictionary<string, AttributeValue>> responseItems)
    {
        List<Organization> organizations = new List<Organization>();

        foreach (Dictionary<string, AttributeValue> item in responseItems)
        {
            organizations.Add(ConvertDictionaryAttributesToOrganization(item));
        }

        return organizations;
    }

    private Organization ConvertDictionaryAttributesToOrganization(Dictionary<string, AttributeValue> attributeValues)
    {
        return new Organization
        {
            Id = new Guid(attributeValues["Id"].S),
            Name = attributeValues["Name"].S
        };
    }
}