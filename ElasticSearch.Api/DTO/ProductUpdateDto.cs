namespace ElasticSearch.Nest.Api.DTO;

public record ProductUpdateDto(string Id,string Name, decimal Price, int Stock, ProductFeatureDto Feature)
{
}