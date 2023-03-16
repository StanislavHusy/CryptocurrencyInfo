using FancyCandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyInfo.model
{
    public class Symbol_info_from_api
    {
        public string id { get; set; }
        public string rank { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string supply { get; set; }
        public string maxSupply { get; set; }

        public string marketCapUsd { get; set; }
        public string volumeUsd24Hr { get; set; }
        public string priceUsd { get; set; }
        public string changePercent24Hr { get; set; }
        public string vwap24Hr { get; set; }
        //       "id": "bitcoin",
        //"rank": "1",
        //"symbol": "BTC",
        //"name": "Bitcoin",
        //"supply": "17193925.0000000000000000",
        //"maxSupply": "21000000.0000000000000000",
        //"marketCapUsd": "119150835874.4699281625807300",
        //"volumeUsd24Hr": "2927959461.1750323310959460",
        //"priceUsd": "6929.8217756835584756",
        //"changePercent24Hr": "-0.8101417214350335",
        //"vwap24Hr": "7175.0663247679233209"
    }
    public class Symbol_info_convert
    {

        public string name { get; set; }
        public string id { get; set; }
        public decimal priceUsd { get; set; }
        public decimal marketCapUsd { get; set; }
        public decimal volumeUsd24Hr { get; set; }
        public decimal changePercent24Hr { get; set; }
        public Symbol_info_convert(string name, string id, decimal priceUsd, decimal marketCapUsd, decimal volumeUsd24Hr, decimal changePercent24Hr)
        {
            this.name = name;
            this.id = id;
            this.priceUsd = priceUsd;
            this.marketCapUsd = marketCapUsd;
            this.volumeUsd24Hr = volumeUsd24Hr;
            this.changePercent24Hr = changePercent24Hr;
        }
    }

    public class history_from_api
    {
        public string priceUsd { get; set; }
        public string time { get; set; }
    //    {
    //  "priceUsd": "6379.3997635993342453",
    //  "time": 1530403200000
    //}
    }
    public class M_history_from_api
    {
        public List<history_from_api> data { get; set; }
        public string timestamp { get; set; }
    }


    public class M_Symbol_info_from_api
    {
        public List<Symbol_info_from_api> data { get; set; }
        public string timestamp { get; set; }
    }

    public class M_Symbol_info_from_api_2
    {
        public Symbol_info_from_api_2 data { get; set; }
        public string timestamp { get; set; }
    }
    public class Symbol_info_from_api_2
    {
        public string id { get; set; }
        public string rank { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string supply { get; set; }
        public string maxSupply { get; set; }

        public string marketCapUsd { get; set; }
        public string volumeUsd24Hr { get; set; }
        public string priceUsd { get; set; }
        public string changePercent24Hr { get; set; }
        public string vwap24Hr { get; set; }
        public string explorer { get; set; }

        //{"id":"bitcoin",
        //"rank":"1",
        //"symbol":"BTC",
        //"name":"Bitcoin",
        //"supply":"19319087.0000000000000000",
        //"maxSupply":"21000000.0000000000000000",
        //"marketCapUsd":"483236904717.4652883111511012",
        //"volumeUsd24Hr":"21020147785.4686869235731810",
        //"priceUsd":"25013.4442024856189276",
        //"changePercent24Hr":"1.2583818445074686",
        //"vwap24Hr":"24518.5980241833042206",
        //"explorer":"https://blockchain.info/"},
        //"timestamp":1678978528623}

        //    "id": "bitcoin",
        //"rank": "1",
        //"symbol": "BTC",
        //"name": "Bitcoin",
        //"supply": "17193925.0000000000000000",
        //"maxSupply": "21000000.0000000000000000",
        //"marketCapUsd": "119179791817.6740161068269075",
        //"volumeUsd24Hr": "2928356777.6066665425687196",
        //"priceUsd": "6931.5058555666618359",
        //"changePercent24Hr": "-0.8101417214350335",
        //"vwap24Hr": "7175.0663247679233209"
        // "explorer":"https://blockchain.info/"}
    }



    public class Candle : FancyCandles.ICandle
    {
        public DateTime t { get; set; }
        public double O { get; set; }
        public double H { get; set; }
        public double L { get; set; }
        public double C { get; set; }
        public double V { get; set; }

        public Candle(DateTime t, double O, double H, double L, double C, double V)
        {
            this.t = t;
            this.O = O;
            this.H = H;
            this.L = L;
            this.C = C;
            this.V = V;
        }
    }
    public class CandlesSource :
           System.Collections.ObjectModel.ObservableCollection<ICandle>,
           FancyCandles.ICandlesSource
    {
        public CandlesSource(FancyCandles.TimeFrame timeFrame)
        {
            this.timeFrame = timeFrame;
        }

        private readonly FancyCandles.TimeFrame timeFrame;
        public FancyCandles.TimeFrame TimeFrame { get { return timeFrame; } }
    }

}
