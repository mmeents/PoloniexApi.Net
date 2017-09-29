using Jojatekok.PoloniexAPI.LiveTools;
using Jojatekok.PoloniexAPI.MarketTools;
using Jojatekok.PoloniexAPI.TradingTools;
using Jojatekok.PoloniexAPI.WalletTools;
using System;
using System.Collections.Generic;

namespace Jojatekok.PoloniexAPI
{
    public sealed class PoloniexClient
    {
        /// <summary>Represents the authenticator object of the client.</summary>
        public IAuthenticator Authenticator { get; private set; }

        /// <summary>A class which contains market tools for the client.</summary>
        public IMarkets Markets { get; private set; }
        /// <summary>A class which contains trading tools for the client.</summary>
        public ITrading Trading { get; private set; }
        /// <summary>A class which contains wallet tools for the client.</summary>
        public IWallet Wallet { get; private set; }
        /// <summary>A class which represents live data fetched automatically from the server.</summary>
        public ILive Live { get; private set; }

        /// <summary>Creates a new instance of Poloniex API .NET's client service.</summary>
        /// <param name="publicApiKey">Your public API key.</param>
        /// <param name="privateApiKey">Your private API key.</param>
        public PoloniexClient(string publicApiKey, string privateApiKey)
        {
            var apiWebClient = new ApiWebClient(Helper.ApiUrlHttpsBase);

            Authenticator = new Authenticator(apiWebClient, publicApiKey, privateApiKey);

            Markets = new Markets(apiWebClient);
            Trading = new Trading(apiWebClient);
            Wallet = new Wallet(apiWebClient);
            Live = new Live();
        }

        /// <summary>Creates a new, unauthorized instance of Poloniex API .NET's client service.</summary>
        public PoloniexClient() : this("", "")
        {

        }
    }

    public class MrAverager {
      private Int32 iHowMany = 0;
      private List<MarketData> lmd;
      public MrAverager (int iHowManyToAverage) {
        if (iHowManyToAverage > 2) {
          iHowMany = iHowManyToAverage - 1;
        }
        lmd = new List<MarketData>();
        lmd.Clear();
      }
      public void Add(MarketData md) {
        lmd.Insert(0, md);
        if ((iHowMany != 0) && (lmd.Count > iHowMany)) {
          lmd.RemoveAt(iHowMany);
        }
      }
      public double AvgChange() {
        double dAvg = 0.0;
        double dDif = 0.0;
        if(lmd.Count>2){
        MarketData a =lmd[0];
        Boolean iftt = true;
        foreach (MarketData md in lmd) {
          if (iftt) {
            iftt = false;
            a = md;
          } else {
            dDif = a.PriceLast - md.PriceLast;
            a = md;
          }
          dAvg = dAvg + dDif;
        }
        }
        return dAvg;
      }
    }
}
