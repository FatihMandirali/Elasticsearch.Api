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
    [HttpGet]
    public async Task<IActionResult> MatchQueryFullText(string category)
    {

        return Ok(await _eCommerceRepository.MatchQueryFullTextAsync(category));

    }


    [HttpGet]
    public async Task<IActionResult> MatchBoolPrefixQueryFullText(string customerFullName)
    {

        return Ok(await _eCommerceRepository.MatchBoolPrefixFullTextAsync(customerFullName));

    }

    [HttpGet]
    public async Task<IActionResult> MatchPhraseQueryFullText(string customerFullName)
    {

        return Ok(await _eCommerceRepository.MatchPhraseFullTextAsync(customerFullName));

    }

    [HttpGet]
    public async Task<IActionResult> MatchPhrasePrefixQueryFullText(string customerFullName)
    {

        return Ok(await _eCommerceRepository.MatchPhrasePrefixFullTextAsync(customerFullName));

    }

    [HttpGet]
    public async Task<IActionResult> CompoundQueryExampleOne(string cityName, double taxfulTotalPrice, string categoryName, string menufacturer)
    {

        return Ok(await _eCommerceRepository.CompoundQueryExampleOneAsync(cityName,taxfulTotalPrice,categoryName,menufacturer));

    }


    [HttpGet]
    public async Task<IActionResult> CompoundQueryExampleTwo(string customerFullName)
    {

        return Ok(await _eCommerceRepository.CompoundQueryExampleTwoAsync(customerFullName));

    }
    [HttpGet]
    public async Task<IActionResult> MultiMatchQueryFullText(string name)
    {

        return Ok(await _eCommerceRepository.MultiMatchQueryFullTextAsync(name));

    }
}