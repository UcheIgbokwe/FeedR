using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedR.Feeds.Quotes.Pricing.Requests;
using FeedR.Feeds.Quotes.Pricing.Utility;

namespace FeedR.Feeds.Quotes.Pricing.Services
{
    public class PricingBackgroundService : BackgroundService
    {
        private int _runningStatus;
        private readonly IPricingGenerator _pricingGenerator;
        private readonly PricingRequestsChannel _pricingRequestsChannel;
        public PricingBackgroundService(IPricingGenerator pricingGenerator, PricingRequestsChannel pricingRequestsChannel)
        {
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
            await _pricingGenerator.StartAsync();
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