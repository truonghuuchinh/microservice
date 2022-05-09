using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(
            IDiscountRepository repository,
            ILogger<DiscountService> logger,
            IMapper mapper
        )
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with {request.ProductName} not found!"));
            }
            _logger.LogInformation("Discount is retrieved for ProductName: {productName},Amount: {amount}", coupon.ProductName, coupon.Amount);
            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            if (request == null)
            {
                throw new RpcException(new Status(StatusCode.DataLoss, $"Discount can't null!"));
            }

            var coupon = _mapper.Map<Coupon>(request.Coupon);

            var result = await _repository.Create(coupon);
            if (!result)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Cann't create discount"));
            }
            _logger.LogInformation("Discount is created successfully, ProductName: {ProductName}", coupon.ProductName);

            var couponModel = _mapper.Map<CouponModel>(coupon);


            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            if (request == null)
            {
                throw new RpcException(new Status(StatusCode.DataLoss, $"Discount can't null!"));
            }

            var coupon = _mapper.Map<Coupon>(request.Coupon);
            var result = await _repository.Update(coupon);

            if (!result)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Cann't update discount"));
            }

            _logger.LogInformation("Discount is updated successfully, ProductName: {ProductName}", coupon.ProductName);
            var couponModel = _mapper.Map<CouponModel>(coupon);

            return couponModel;
        }

        public override async Task<DeleteDiscountRespone> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var result = await _repository.Delete(request.ProductName);
            var respone = new DeleteDiscountRespone
            {
                Success = result
            };

            _logger.LogInformation($" Delete {request.ProductName} is successfully!");

            return respone;
        }
    }
}
