using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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
using System.Net.Http.Json;
using Newtonsoft.Json;
using CryptocurrencyInfo.model;
using System.ComponentModel;
using System.Threading;
using System.Globalization;
using FancyCandles;

namespace CryptocurrencyInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       static readonly HttpClient client = new HttpClient();
        private BindingList<Symbol_info_convert> data_assets = new BindingList<Symbol_info_convert>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e) //
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://api.coincap.io/v2/assets");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                M_Symbol_info_from_api k = JsonConvert.DeserializeObject<M_Symbol_info_from_api>(responseBody);
                //System.Diagnostics.Debug.WriteLine(responseBody);

                CultureInfo newCulture = CultureInfo.CreateSpecificCulture("en-GB");
                Thread.CurrentThread.CurrentCulture = newCulture;
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";
                for (int i = 0; i < 100;i++)
                {
                    Symbol_info_convert v = new Symbol_info_convert(k.data[i].name, k.data[i].id, Decimal.Round(Convert.ToDecimal(k.data[i].priceUsd, nfi), 2),
                    Decimal.Round(Convert.ToDecimal(k.data[i].marketCapUsd, nfi), 0), Decimal.Round(Convert.ToDecimal(k.data[i].volumeUsd24Hr, nfi), 0),
                    Decimal.Round(Convert.ToDecimal(k.data[i].changePercent24Hr, nfi), 2));
                    data_assets.Add(v);
                }
            }
            catch (HttpRequestException ex)
            {

            }
            myFrame.Content = new MainPage(data_assets);
        }
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            myFrame.Content = new CandlesPage(client);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

        }
        private void calc_page_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    
}
