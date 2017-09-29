using Newtonsoft.Json;
using System;

namespace Jojatekok.PoloniexAPI.TradingTools
{
    public class Trade : Order, ITrade
    {
        [JsonProperty("date")]
        private string TimeInternal {
            set { Time = Helper.ParseDateTime(value); }
        }
        public DateTime Time { get; private set; }

        public Trade(ulong aIdOrder, OrderType aType, double aPricePerCoin, double aAmountQuote, double aAmountBase, DateTime aTime) : base (aIdOrder, aType, aPricePerCoin, aAmountQuote, aAmountBase) {
          Time = aTime;
        }
    }
}
