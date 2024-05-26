using ElasticSearch.Nest.Api.DTO;
using ElasticSearch.Nest.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.Nest.Api.Controller;

public class ProductController(ProductService _productService):BaseController
{
    [HttpPost]
    public async Task<IActionResult> Save(ProductCreateDto request)
    {
        return CreateActionResult(await _productService.SaveAsync(request));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return CreateActionResult(await _productService.GetAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return CreateActionResult(await _productService.GetByIdAsync(id));
    }
    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdateDto request)
    {
        return CreateActionResult(await _productService.UpdateAsync(request));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return CreateActionResult(await _productService.DeleteAsync(id));
    }
}