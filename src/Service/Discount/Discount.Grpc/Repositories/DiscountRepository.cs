using Dapper;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DiscountRepository> _logger;
        private string ConnectionString = "";

        public DiscountRepository(IConfiguration configuration, ILogger<DiscountRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            ConnectionString = _configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        }
        public async Task<bool> Create(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(ConnectionString);

            var param = new
            {
                ProductName = coupon.ProductName,
                Amount = coupon.Amount,
                Description = coupon.Description
            };

            var result = await connection.ExecuteAsync(
                "Insert into Coupon(ProductName,Description,Amount) " +
                "Values(@ProductName,@Description,@Amount)", param);
            return result > 0;
        }

        public async Task<bool> Delete(string productName)
        {
            using var connection = new NpgsqlConnection(ConnectionString);

            var param = new
            {
                ProductName = productName
            };

            var result = await connection.ExecuteAsync(
                "Delete from Coupon " +
                "Where ProductName=@ProductName", param);
            return result > 0;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                        ("SELECT * FROM Coupon WHERE ProductName=@ProductName", new { ProductName = productName });
                    if (coupon == null)
                    {
                        return new Coupon
                        {
                            ProductName = "No Discount",
                            Amount = 0,
                            Description = "No discount DESC"
                        };
                    }
                    return coupon;
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
           
        }

        public async Task<bool> Update(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(ConnectionString);

            var param = new
            {
                ProductName = coupon.ProductName,
                Amount = coupon.Amount,
                Description = coupon.Description,
                Id = coupon.Id
            };

            var affected = await connection.ExecuteAsync
                    ("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                            param);

            if (affected == 0)
                return false;

            return true;

        }
    }
}
