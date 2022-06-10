using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using Microsoft.Extensions.Options;

namespace BackendTeamWebApiService.Services;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Models;

/// <inheritdoc cref="IDynamoDbAccessService"/>
internal sealed class DynamoDbAccessService : IDynamoDbAccessService
{
    private readonly ILogger<DynamoDbAccessService> _logger;

    private readonly AmazonDynamoDBClient _amazonDynamoDbClient;

    public DynamoDbAccessService(ILogger<DynamoDbAccessService> logger,
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
            await _amazonDynamoDbClient.PutItemAsync("mcw-capstone-project", organizationAttributes);
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
            ScanRequest request = new ScanRequest()
            {
                TableName = "mcw-capstone-project",
                AttributesToGet = {"Id", "Name"}
            };
            var response = await _amazonDynamoDbClient.ScanAsync(request);

            return ConvertResponseToOrganizations(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.StackTrace);
            throw;
        }
    }

    private List<Organization> ConvertResponseToOrganizations(ScanResponse response)
    {
        List<Organization> organizations = new List<Organization>();

        foreach (Dictionary<string, AttributeValue> item in response.Items)
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