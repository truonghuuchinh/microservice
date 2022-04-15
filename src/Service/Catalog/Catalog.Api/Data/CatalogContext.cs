using Catalog.Api.Common.Constants;
using Catalog.Api.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext()
        {
            var client = new MongoClient(ConfigDatabase.ConnectionString);
            var database = client.GetDatabase(ConfigDatabase.DatabaseName);

            Products = database.GetCollection<Product>(ConfigDatabase.Collection);

            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
