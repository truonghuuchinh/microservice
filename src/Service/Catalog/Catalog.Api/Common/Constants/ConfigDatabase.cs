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

        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static string Collection { get; set; }

        public ConfigDatabase(IConfiguration configuration)
        {
            _configuration = configuration;

            ConnectionString = _configuration.GetValue<string>("DatabaseSettings:ConnectionStrings");
            DatabaseName = _configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            Collection = _configuration.GetValue<string>("DatabaseSettings:CollectionName");
        }


    }
}
