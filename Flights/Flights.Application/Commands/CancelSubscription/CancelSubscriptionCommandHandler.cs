using Flights.Application.Exceptions;
using Flights.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Application.Commands.CancelSubscription
{
    public class CancelSubscriptionCommandHandler : IRequestHandler<CancelSubscriptionCommand>
    {
		private readonly IWebhookSubscriptionsRepository _webhookSubscriptionsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelSubscriptionCommandHandler(IWebhookSubscriptionsRepository webhookSubscriptionsRepository, IUnitOfWork unitOfWork)
        {
            _webhookSubscriptionsRepository = webhookSubscriptionsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CancelSubscriptionCommand request, CancellationToken cancellationToken)
        {
			try
			{
                var sub = await _webhookSubscriptionsRepository.GetSubscriptionBySecret(request.secret);
                if (sub == null)
                {
                    throw new NotFoundException("Couldn`t find subscription with specified secret");
                }

                _webhookSubscriptionsRepository.DeleteSubscription(sub);

                await _unitOfWork.SaveChangesAsync();

			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
