using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var respone = await _client.GetAsync("/api/v1/catalog");

            return await respone.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var respone = await _client.GetAsync($"/api/v1/catalog/{id}");

            return await respone.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var respone = await _client.GetAsync($"/api/v1/catalog/getproductbycatagory/{category}");

            return await respone.ReadContentAs<List<CatalogModel>>();
        }
    }
}
