using Discount.Api.Entities;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discount.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<IDiscountRepository> _logger;

        public DiscountController(IDiscountRepository repository,ILogger<IDiscountRepository> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{productName}", Name ="GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var result = await _repository.GetDiscount(productName);
            return Ok(result);
        }

        [HttpPost(Name = "CreateDiscount")]
        [ProducesResponseType(typeof(bool),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody]Coupon coupon)
        {
            try
            {
                await _repository.Create(coupon);
                return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Has error when Create Discount - MsgError: {ex.Message}");
                throw;
            }
          
        }

        [HttpPut(Name = "UpdateDiscount")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> UpdateDiscount([FromBody]Coupon coupon)
        {
            try
            {
                return Ok(await _repository.Update(coupon));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Has error when Update Discount - MsgError: {ex.Message}");
                throw;
            }
          
        }

        [HttpDelete("{productName}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> DeleteDiscount(string productName)
        {
            try
            {
                return Ok(await _repository.Delete(productName));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Has error when Update Discount - MsgError: {ex.Message}");
                throw;
            }
          
        }

    }
}
