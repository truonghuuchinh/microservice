using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Common.Constants
{
    public class ConfigDatabase
    {
        private readonly IConfiguration _configuration;

        public  string ConnectionString { get; set; }
        public  string DatabaseName { get; set; }
        public  string Collection { get; set; }

        public ConfigDatabase(IConfiguration configuration)
        {
            _configuration = configuration;

            ConnectionString = _configuration.GetValue<string>("DatabaseSettings:ConnectionStrings");
            DatabaseName = _configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            Collection = _configuration.GetValue<string>("DatabaseSettings:CollectionName");
        }


    }
}
