namespace ElasticSearch.Nest.Api.DTO;

public record ProductDto(string Id, string Name, decimal Price, int Stock, ProductFeatureDto? Feature);