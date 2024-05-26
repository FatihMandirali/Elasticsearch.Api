using System.Collections.Immutable;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearch.Nest.Api.Model.ECommerceModel;

namespace ElasticSearch.Nest.Api.Repository;

public class ECommerceRepository(ElasticsearchClient _elasticsearchClient)
{
    private const string indexName = "kibana_sample_data_ecommerce";

    #region TermQuery
    public async Task<ImmutableList<ECommerce>> TermQuery(string customerFirstName)
    {
        //1.Yol
        // var result = await _elasticsearchClient.SearchAsync<ECommerce>(
        //     s=>s.Index(indexName).Query(q=>q.Term(t=>t.Field("customer_first_name.keyword").Value(customerFirstName)))
        // );

        //2.Yol
        var termQuery = new TermQuery("customer_first_name.keyword")
        {
            Value = customerFirstName, CaseInsensitive = true
        };
        
        var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => 
            s.Index(indexName)
            .Query(termQuery)
        );

        foreach (var item in result.Hits)
        {
            item.Source.Id = item.Id;
        }

        return result.Documents.ToImmutableList();
    }
    public async Task<ImmutableList<ECommerce>> TermsQuery(List<string> customerFirstNameList)
    {

        List<FieldValue> terms = new List<FieldValue>();
        customerFirstNameList.ForEach(x =>
        {
            terms.Add(x);
        });
        var termsQuery = new TermsQuery()
        {
            Field = "customer_first_name.keyword",
            Terms = new TermsQueryField(terms),
            
        };
        //1.Yol
        //
        // var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName).Query(termsQuery));
        //
        
        //2.yol
        var result = await _elasticsearchClient.SearchAsync<ECommerce>(s=>s.Index(indexName)
            .Size(100)
            .Query(q=>q.
                Terms(t=>t.
                    Field(f=>f.CustomerFirstName).Suffix("keyword")).Terms(termsQuery)));
        
        foreach (var item in result.Hits)
        {
            item.Source.Id = item.Id;
        }
        return result.Documents.ToImmutableList();
    }
    public async Task<ImmutableList<ECommerce>> TermPrefixQuery(string customerName)
    {
        //Yol 1
        var prefixQuery = new PrefixQuery("customer_first_name.keyword")
        {
            Value = customerName, CaseInsensitive = true
        };
        var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q=>q.Prefix(prefixQuery))
        );

        //Yol 2
        // var result1 = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName)
        //     .Query(q=>q.Prefix(p=>p.Field("customer_first_name.keyword").CaseInsensitive().Value(customerName)))
        // );

        return result.Documents.ToImmutableList();
    }
    public async Task<ImmutableList<ECommerce>> TermRangeQuery(double? fromPrice, double? toPrice)
    {
        var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q=>
                q.Range(r=>r.NumberRange(nr=>nr.Field("products.base_price").Gte(fromPrice).Lt(toPrice))))
        );

        return result.Documents.ToImmutableList();

    }
    public async Task<ImmutableList<ECommerce>> TermMatchAllQuery()
    {
        var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q=>q.MatchAll(new MatchAllQuery())));

        return result.Documents.ToImmutableList();
    }
    public async Task<ImmutableList<ECommerce>> TermPaginationQuery(int page, int pageSize)
    {
        var pageFrom = (page - 1) * pageSize;
        var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Size(pageSize)
            .From(pageFrom)
            .Query(q=>q.MatchAll(new MatchAllQuery())));

        return result.Documents.ToImmutableList();
    }
    public async Task<ImmutableList<ECommerce>> TermWildCardQuery(string value)
    {
        var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q=>q.Wildcard(w=>w.Field("customer_first_name.keyword").CaseInsensitive().Value($"{value}*"))));

        return result.Documents.ToImmutableList();
    }
    public async Task<ImmutableList<ECommerce>> TermFuzzyQuery(string value)
    {
        var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Query(q=>q.Fuzzy(f=>f.Field("customer_first_name.keyword").Value(value)))
            .Sort(sort=>sort.Field("products.base_price",new FieldSort{Order = SortOrder.Desc})));

        return result.Documents.ToImmutableList();
    }
    #endregion

    #region FullTextQuery

    public async Task<ImmutableList<ECommerce>> MatchQueryFullTextAsync(string categoryName)
    {


        var result = await _elasticsearchClient.SearchAsync<ECommerce>(s => s.Index(indexName)
            .Size(1000).Query(q => q
                .Match(m => m
                    .Field(f => f.Category)
                    .Query(categoryName).Operator(Operator.And))));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;
        return result.Documents.ToImmutableList();

    }

    #endregion
   
}