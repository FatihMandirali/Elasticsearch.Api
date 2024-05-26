using System.Collections.Immutable;
using ElasticSearch.Nest.Api.DTO;
using ElasticSearch.Nest.Api.Model;
using Nest;

namespace ElasticSearch.Nest.Api.Repository;

public sealed class ProductRepository(ElasticClient elasticClient)
{
    private const string indexName = "products";
    public async Task<Product?> SaveAsync(Product newProduct)
    {
        newProduct.Created = DateTime.Now;
        var response = await elasticClient.IndexAsync(newProduct,x=>x.Index(indexName).Id(Guid.NewGuid().ToString()));
        if (!response.IsValid) return null;
        newProduct.Id = response.Id;
        return newProduct;
    }
    
    public async Task<ImmutableList<Product>> GetAllAsync()
    {

        var result = await elasticClient.SearchAsync<Product>(
            s => s.Index(indexName).Query(q => q.MatchAll()
            ));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();
    }
    
    public async Task<Product?> GetByIdAsync(string id)
    {
        var response = await elasticClient.GetAsync<Product>(id, x => x.Index(indexName));

        if(!response.IsValid)
        {
            return null;
        }

        response.Source.Id = response.Id;
        return response.Source;

    }
    
    public async Task<bool> UpdateAsync(ProductUpdateDto updateProduct)
    {
        var response = await elasticClient.UpdateAsync<Product, ProductUpdateDto>(updateProduct.Id, x =>
            x.Index(indexName).Doc(updateProduct));

        return response.IsValid;
    }
    
    /// <summary>
    /// Hata yönetimi için bu method ele alınmıştır.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DeleteResponse> DeleteAsync(string id)
    {

        var response= await elasticClient.DeleteAsync<Product>(id,x=>x.Index(indexName));
        return response;
    }

}