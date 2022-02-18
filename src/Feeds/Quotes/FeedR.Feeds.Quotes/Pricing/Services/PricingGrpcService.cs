using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace FeedR.Feeds.Quotes.Pricing.Services
{
    internal sealed class PricingGrpcService : PricingFeed.PricingFeedBase
    {
        private readonly IPricingGenerator _pricingGenerator;
        public PricingGrpcService(IPricingGenerator pricingGenerator)
        {
            _pricingGenerator = pricingGenerator;
        }

        public override Task<GetSymbolResponse> GetSymbols(GetSymbolRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GetSymbolResponse()
            {
                Symbols =  { _pricingGenerator.GetSymbols()}
            });
        }
    }
}