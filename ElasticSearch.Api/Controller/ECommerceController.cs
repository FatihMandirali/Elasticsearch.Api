using ElasticSearch.Nest.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.Nest.Api.Controller;

public class ECommerceController(ECommerceRepository _eCommerceRepository) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> TermQuery(string customerFirstName)
    {
        return Ok(await _eCommerceRepository.TermQuery(customerFirstName));
    }
    [HttpGet("TermsQuery")]
    public async Task<IActionResult> TermsQuery([FromQuery]List<string> customerFirstNameList)
    {
        return Ok(await _eCommerceRepository.TermsQuery(customerFirstNameList));
    }
    [HttpGet("TermsPrefixQuery")]
    public async Task<IActionResult> TermsPrefixQuery([FromQuery]string customStartName)
    {
        return Ok(await _eCommerceRepository.TermPrefixQuery(customStartName));
    }
    [HttpGet("TermsRangeQuery")]
    public async Task<IActionResult> TermsRangeQuery([FromQuery]double? fromPrice, double? topPrice)
    {
        return Ok(await _eCommerceRepository.TermRangeQuery(fromPrice,topPrice));
    }
    [HttpGet("TermMatchAllQuery")]
    public async Task<IActionResult> TermMatchAllQuery()
    {
        return Ok(await _eCommerceRepository.TermMatchAllQuery());
    }
    [HttpGet("TermPaginationQuery")]
    public async Task<IActionResult> TermPaginationQuery(int page,int pageSize)
    {
        return Ok(await _eCommerceRepository.TermPaginationQuery(page,pageSize));
    }
    [HttpGet("TermWildCardQuery")]
    public async Task<IActionResult> TermWildCardQuery(string value)
    {
        return Ok(await _eCommerceRepository.TermWildCardQuery(value));
    }
    [HttpGet("TermFuzzyQuery")]
    public async Task<IActionResult> TermFuzzyQuery(string value)
    {
        return Ok(await _eCommerceRepository.TermFuzzyQuery(value));
    }
    [HttpGet("MatchQueryFullTextAsync")]
    public async Task<IActionResult> MatchQueryFullTextAsync(string value)
    {
        return Ok(await _eCommerceRepository.MatchQueryFullTextAsync(value));
    }
}