using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using FeedR.Feeds.Quotes.Pricing.Requests;

namespace FeedR.Feeds.Quotes.Pricing.Utility
{
    public class PricingRequestsChannel
    {
        public readonly Channel<IPricingRequest> Requests = Channel.CreateUnbounded<IPricingRequest>();
    }
}