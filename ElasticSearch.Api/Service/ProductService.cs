using System.Net;
using Elastic.Clients.Elasticsearch;
using ElasticSearch.Nest.Api.DTO;
using ElasticSearch.Nest.Api.Repository;

namespace ElasticSearch.Nest.Api.Service;

public sealed class ProductService(ProductRepository productRepository, ILogger<ProductService> _logger)
{
    public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
    {
        var responseProduct = await productRepository.SaveAsync(request.CreateProduct());
        if (responseProduct == null)
        {
            return ResponseDto<ProductDto>.Fail(new List<string> { "kayıt esnasında bir hata meydana geldi." }, System.Net.HttpStatusCode.InternalServerError);
        }
        
        return ResponseDto<ProductDto>.Success(responseProduct.CreateDto(), HttpStatusCode.Created);
    }
    
    public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
    {
        
        var products = await productRepository.GetAllAsync();
        var productListDto = new List<ProductDto>();
        
        foreach (var x in products)
        {
            if (x.Feature is null)
            {
                productListDto.Add(new ProductDto(x.Id, x.Name, x.Price, x.Stock, null));
                continue;
            }
            productListDto.Add(new ProductDto(x.Id, x.Name, x.Price, x.Stock, new ProductFeatureDto(x.Feature.Width, x.Feature!.Height, x.Feature!.Color)));
        }

        return ResponseDto<List<ProductDto>>.Success(productListDto, HttpStatusCode.OK);
    }
    
    public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
    {

        var hasProduct = await productRepository.GetByIdAsync(id);


        if (hasProduct == null)
        {
            return ResponseDto<ProductDto>.Fail("ürün bulunamadı", HttpStatusCode.NotFound);
        }

        var productDto = hasProduct.CreateDto();

        return ResponseDto<ProductDto>.Success(productDto, HttpStatusCode.OK);
    }
    
    public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto updateProduct)
    {
        var  isSuccess= await productRepository.UpdateAsync(updateProduct);

        if(!isSuccess)
        {
            return ResponseDto<bool>.Fail(new List<string> { "update esnasında bir hata meydana geldi." }, System.Net.HttpStatusCode.InternalServerError);
        }
        
        return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
    }
    
    public async Task<ResponseDto<bool>> DeleteAsync(string id)
    {
        var deleteResponse = await productRepository.DeleteAsync(id);


        if(!deleteResponse.IsSuccess() && deleteResponse.Result==Result.NotFound)
        {
            _logger.LogError("silinemediiii");

            return ResponseDto<bool>.Fail(new List<string> { "Silmeye çalıştığınız ürün bulunamamıştır." }, System.Net.HttpStatusCode.NotFound);

        }


        if(!deleteResponse.IsSuccess())
        {
            deleteResponse.TryGetOriginalException(out Exception exception);
            _logger.LogError(exception, deleteResponse.ElasticsearchServerError.Error.ToString());
            return ResponseDto<bool>.Fail(new List<string> { "silme esnasında bir hata meydana geldi." }, System.Net.HttpStatusCode.InternalServerError);
        }
        
        return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
    }

}