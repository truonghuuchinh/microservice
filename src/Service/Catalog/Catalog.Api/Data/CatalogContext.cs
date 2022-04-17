﻿using Catalog.Api.Common.Constants;
using Catalog.Api.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {
        private IConfiguration _configuration;

        public CatalogContext(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(_configuration.GetValue<string>("DatabaseSettings:ConnectionStrings"));
            var database = client.GetDatabase(_configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(_configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
