using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Layouts;
using Microsoft.Extensions.Configuration;

namespace Domain.Common.Utils;
public class Logs
{
    private Settings _settings = new ();
    public Logs(IConfiguration configuration)
    {
        _settings.Uri = configuration.GetSection("AzureCosmosDB").GetSection("Uri").Value ?? string.Empty;
        _settings.Key = configuration.GetSection("AzureCosmosDB").GetSection("Key").Value ?? string.Empty;
        _settings.DatabaseName = configuration.GetSection("AzureCosmosDB").GetSection("DatabaseName").Value ?? string.Empty;
        _settings.Container = configuration.GetSection("AzureCosmosDB").GetSection("Container").Value ?? string.Empty;
    }

    public async Task AddLog(string log, string userName)
    {
        var cosmosClient = new CosmosClient(_settings.Uri, _settings.Key);
        var database = cosmosClient.GetDatabase(_settings.DatabaseName);
        var container = database.GetContainer(_settings.Container);

        var document = new
        {
            id = Guid.NewGuid(),
            name = userName,
            description = log,
        };

        var response = await container.CreateItemAsync(document);

        if(response.StatusCode == System.Net.HttpStatusCode.Created)
        {
            Console.WriteLine("Created succesfull");
        }
        else
        {
            Console.WriteLine("Created unsuccesfull");
        }
    }

    public async Task SearchLog()
    {
        var cosmosClient = new CosmosClient(_settings.Uri, _settings.Key);
        var database = cosmosClient.GetDatabase(_settings.DatabaseName);
        var container = database.GetContainer(_settings.Container);

        var query = new QueryDefinition("SELECT * FROM c");

        var queryResult = container.GetItemQueryIterator<dynamic>(query);

        var results = new List<dynamic>();

        while(queryResult.HasMoreResults)
        {
            var currentResult = await queryResult.ReadNextAsync();

            foreach(var item in currentResult)
            {
                results.Add(item);
            }
        }

        foreach(var item in results)
        {
            Console.WriteLine($"Id: {item.id}, Name: {item.name}, description: {item.description}");
        }
    }
}
