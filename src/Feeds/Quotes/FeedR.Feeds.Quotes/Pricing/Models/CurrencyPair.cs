using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedR.Feeds.Quotes.Pricing.Models
{
    internal sealed record CurrencyPair(string Symbol, decimal value, long Timestamp);
}