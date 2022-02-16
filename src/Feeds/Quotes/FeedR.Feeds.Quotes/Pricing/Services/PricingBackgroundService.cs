using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedR.Feeds.Quotes.Pricing.Requests;
using FeedR.Feeds.Quotes.Pricing.Utility;
using FeedR.Shared.Streaming;

namespace FeedR.Feeds.Quotes.Pricing.Services
{
    internal class PricingBackgroundService : BackgroundService
    {
        private int _runningStatus;
        private readonly IPricingGenerator _pricingGenerator;
        private readonly PricingRequestsChannel _pricingRequestsChannel;
        private readonly ILogger<PricingBackgroundService> _logger;
        private readonly IStreamPublisher _streamPublisher;
        public PricingBackgroundService(IPricingGenerator pricingGenerator, PricingRequestsChannel pricingRequestsChannel,
                                IStreamPublisher streamPublisher, ILogger<PricingBackgroundService> logger)
        {
            _streamPublisher = streamPublisher;
            _logger = logger;
            _pricingRequestsChannel = pricingRequestsChannel;
            _pricingGenerator = pricingGenerator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var request in _pricingRequestsChannel.Requests.Reader.ReadAllAsync(stoppingToken))
            {
                var _= request switch
                {
                    StartPricing => StartGeneratorAsync(),
                    StopPricing => StopGeneratorAsync(),
                    _ => Task.CompletedTask
                };
            }
        }

        private async Task StartGeneratorAsync()
        {
            if(Interlocked.Exchange(ref _runningStatus, 1) == 1)
            {
                return;
            }

            await foreach (var item in _pricingGenerator.StartAsync())
            {
                _logger.LogInformation("Publishing the currency pair.... {item}", item);
                await _streamPublisher.PublishAsync("pricing", item);
            }
        }

        private async Task StopGeneratorAsync()
        {
            if(Interlocked.Exchange(ref _runningStatus, 0) == 0)
            {
                return;
            }
            await _pricingGenerator.StopAsync();
        }
    }
}