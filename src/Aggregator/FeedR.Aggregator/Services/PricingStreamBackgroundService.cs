using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedR.Shared.Streaming;

namespace FeedR.Aggregator.Services
{
    internal sealed class PricingStreamBackgroundService : BackgroundService
    {
        private readonly ILogger<PricingStreamBackgroundService> _logger;
        private readonly IStreamSubscriber _subscriber;
        public PricingStreamBackgroundService(IStreamSubscriber subscriber, ILogger<PricingStreamBackgroundService> logger)
        {
            _subscriber = subscriber;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _subscriber.SubscribeAsync<CurrencyPair>("pricing", currencyPair => _logger.LogInformation("Pricing {currencyPair.Symbol} = {currency.Value}, timestamp: {currencyPair.Timestamp}", currencyPair.Symbol, currencyPair.Value, currencyPair.Timestamp));
        }

        private record CurrencyPair(string Symbol, decimal Value, long Timestamp);
    }
}