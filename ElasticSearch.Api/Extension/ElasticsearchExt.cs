using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

namespace ElasticSearch.Nest.Api.Extension;

public static class ElasticsearchExt
{
    public static void AddElastic(this IServiceCollection services, IConfiguration configuration)
    {
        string username = configuration.GetSection("Elastic")["Username"]??"";
        string password = configuration.GetSection("Elastic")["Password"]??"";
        var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("Elastic")["Url"]!))
            .Authentication(new BasicAuthentication(username,password));
        
        var client = new ElasticsearchClient(settings);
        
        services.AddSingleton(client);
    }
}