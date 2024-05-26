using Elasticsearch.Net;
using Nest;

namespace ElasticSearch.Nest.Api.Extension;

public static class ElasticsearchExt
{
    public static void AddElastic(this IServiceCollection services, IConfiguration configuration)
    {
        var pool = new SingleNodeConnectionPool(new Uri(configuration.GetSection("Elastic")["Url"]!));
        var setting = new ConnectionSettings(pool);
        var client = new ElasticClient(setting);
        services.AddSingleton(client);
    }
}