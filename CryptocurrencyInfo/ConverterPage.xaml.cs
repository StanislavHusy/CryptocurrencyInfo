using CryptocurrencyInfo.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

namespace CryptocurrencyInfo
{
    /// <summary>
    /// Логика взаимодействия для ConverterPage.xaml
    /// </summary>
    public partial class ConverterPage : Page
    {
        BindingList<Symbol_info_convert> data_assets_copy;
        public ConverterPage(BindingList<Symbol_info_convert> data_assets, bool eng_localization)
        {
            InitializeComponent();
            if (!eng_localization)
            {
                textBox1.Text = "id активу";
                textBox4.Text = "id активу";
                textBox2.Text = "кількість";
            }
            data_assets_copy = data_assets;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string st_asset1 = textBox1.Text;
            string st_asset2 = textBox4.Text;
            int num_1 = 0, num_2 = 0;
            bool find_1 = false, find_2 = false;
            for(int i = 0; i < data_assets_copy.Count; i++)
            {
                if (!find_1 &&st_asset1 == data_assets_copy[i].id)
                {
                    find_1 = true;
                    num_1 = i;
                }
                if (!find_2 && st_asset2 == data_assets_copy[i].id)
                {
                    find_2 = true;
                    num_2 = i;
                }
                if (find_1 && find_2)
                    break;
            }
            decimal amount = Convert.ToDecimal(textBox2.Text);
            textBox3.Text = Convert.ToString(Math.Round(amount * data_assets_copy[num_1].priceUsd / data_assets_copy[num_2].priceUsd , 8) );

        }
    }
}
