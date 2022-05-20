using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Constants;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Models;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Command.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(
            IOrderRepository repository,
            IMapper mapper,
            IEmailService emailService,
            ILogger<CheckoutOrderCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var result = await _repository.AddAsync(orderEntity);

            _logger.LogInformation($"Order {result.Id} is created successfully!");

            await SendMail(result);

            return result.Id;
        }

        private async Task SendMail(Order result)
        {
            var email = new Email
            {
                To = result.EmailAddress,
                Body = EmailInformation.Body,
                Subject = EmailInformation.Subject
            };

            try
            {
                await _emailService.SendEmail(email);
                _logger.LogInformation("Send Email for user successfully!.");
            }
            catch (Exception e)
            {
                _logger.LogError($"Has error occur when send mail. Infor: {e.Message}");
            }
        }
    }
}
