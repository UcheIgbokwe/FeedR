using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedR.Feeds.Quotes.Pricing.Services
{
    public interface IPricingGenerator
    {
        Task StartAsync();
        Task StopAsync();
    }

    public class PricingGenerator : IPricingGenerator
    {
        private readonly Random _random = new();
        private readonly ILogger<PricingGenerator> _logger;
        public PricingGenerator(ILogger<PricingGenerator> logger)
        {
            _logger = logger;
        }
        private readonly Dictionary<string, decimal> _currencyPairs = new()
        {
            ["EURUSD"] = 1.13M,
            ["EURGBP"] = 0.85M,
            ["EURCHF"] = 1.04M,
            ["EURPLN"] = 4.62M,
        };
        private bool _isRunning;
        public async Task StartAsync()
        {
            _isRunning = true;
            while(_isRunning)
            {
                foreach (var (symbol, pricing) in _currencyPairs)
                {
                    if(!_isRunning)
                    {
                        return;
                    }

                    var tick = NextTick();
                    var newPricing = pricing + tick;
                    _currencyPairs[symbol] = newPricing;

                    _logger.LogInformation("Update Pricing for {symbol}, {pricing:F} -> {newPricing:F}", symbol, pricing, newPricing);

                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }
        }

        public Task StopAsync()
        {
            _isRunning = false;
            return Task.CompletedTask;
        }

        private decimal NextTick()
        {
            var sign = _random.Next(0, 2) == 0 ? -1 : 1;
            var tick = _random.NextDouble() / 20;
            return (decimal) (sign * tick);
        }
    }
}