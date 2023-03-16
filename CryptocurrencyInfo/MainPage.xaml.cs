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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        BindingList<Symbol_info_convert> data_assets_copy = new BindingList<Symbol_info_convert>();
        public MainPage(BindingList<Symbol_info_convert> data_assets)
        {
            InitializeComponent();
            data_assets_copy = data_assets;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lv1.ItemsSource = data_assets_copy;
        }
    }
}
