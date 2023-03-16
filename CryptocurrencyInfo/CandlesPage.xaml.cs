using CryptocurrencyInfo.model;
using FancyCandles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptocurrencyInfo
{
    /// <summary>
    /// Логика взаимодействия для CandlesPage.xaml
    /// </summary>
    public partial class CandlesPage : Page
    {
        CandlesSource candles = new CandlesSource(FancyCandles.TimeFrame.M5);
        HttpClient client_copy; // грубая ошибка???
        private BindingList<Symbol_info_convert> data_assets = new BindingList<Symbol_info_convert>();
        public CandlesPage(HttpClient client)
        {
            InitializeComponent();
            client_copy = client;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = candles;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string st_id = textBox1.Text;
                string st_TimeFrame = textBox2.Text;
                HttpResponseMessage response = await client_copy.GetAsync("https://api.coincap.io/v2/assets/" + st_id + "/history?interval=" + st_TimeFrame); // close_price
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();


                System.Diagnostics.Debug.WriteLine(responseBody);
                M_history_from_api k = JsonConvert.DeserializeObject<M_history_from_api>(responseBody);

                response = await client_copy.GetAsync("http://api.coincap.io/v2/assets/" + st_id);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();

                M_Symbol_info_from_api_2 req_data2 = JsonConvert.DeserializeObject<M_Symbol_info_from_api_2>(responseBody);

                //  timeframe  
                FancyCandles.TimeFrame timeFrame = FancyCandles.TimeFrame.M1;
                if(st_TimeFrame == "m5")
                    timeFrame = FancyCandles.TimeFrame.M5;
                if (st_TimeFrame == "m15")
                    timeFrame = FancyCandles.TimeFrame.M15;
                if (st_TimeFrame == "m30")
                    timeFrame = FancyCandles.TimeFrame.M30;
                if (st_TimeFrame == "h1")
                    timeFrame = FancyCandles.TimeFrame.H1;
                if (st_TimeFrame == "h2")
                    timeFrame = FancyCandles.TimeFrame.H2;
                if (st_TimeFrame == "d1")
                    timeFrame = FancyCandles.TimeFrame.Daily;
               // 


               CandlesSource candles = new CandlesSource(timeFrame);


                // знак плавающей точки меняем с "," на "."
                CultureInfo newCulture = CultureInfo.CreateSpecificCulture("en-GB");
                Thread.CurrentThread.CurrentCulture = newCulture;
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";

                DateTime t0 = DateTime.Now;
                for (int i = 0; i < k.data.Count; i++)
                {
                    double p0 = Math.Round(Convert.ToDouble(Convert.ToDecimal(k.data[i].priceUsd, nfi)), 2);

                    // по какой-то причине (наверное нужен ключ) апи отказывается отправлять мне целыче свечи, поэтому я формирую их сам 
                    // на основе close_price history
                    // generate high price 
                    Random rnd = new Random();
                    double d_v1 = rnd.NextDouble();
                    double high_price = p0 * (1 + 0.005 * d_v1);
                    // generate low price 
                    rnd = new Random();
                    d_v1 = rnd.NextDouble();
                    double low_price = p0 * (0.99 + 0.005 * d_v1);
                    // generate open price 
                    rnd = new Random();
                    d_v1 = rnd.NextDouble();
                    double open_price = low_price + (high_price - low_price) * d_v1;
                    // generate close price 
                    rnd = new Random();
                    d_v1 = rnd.NextDouble();
                    double close_price = low_price + (high_price - low_price) * d_v1;

                    candles.Add(new Candle(t0.AddMinutes(-k.data.Count * timeFrame.ToMinutes() + i * timeFrame.ToMinutes()),
                                 open_price, high_price, low_price, close_price, 0));
                }
                Symbol_info_convert v = new Symbol_info_convert(req_data2.data.name, req_data2.data.id, Decimal.Round(Convert.ToDecimal(req_data2.data.priceUsd, nfi), 2),
                    Decimal.Round(Convert.ToDecimal(req_data2.data.marketCapUsd, nfi) / 1000000000, 2), Decimal.Round(Convert.ToDecimal(req_data2.data.volumeUsd24Hr, nfi) / 1000000000, 2),
                    Decimal.Round(Convert.ToDecimal(req_data2.data.changePercent24Hr, nfi), 2));
                if (data_assets.Count > 0)
                    data_assets[0] = v;
                else
                    data_assets.Add(v);

                info_line.DataContext = data_assets;
                DataContext = candles;
            }
            catch (HttpRequestException ex)
            {

            }
        }
    }
}
