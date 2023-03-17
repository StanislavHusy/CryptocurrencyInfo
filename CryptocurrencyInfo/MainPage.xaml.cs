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
        public MainPage(BindingList<Symbol_info_convert> data_assets, bool eng_localization)
        {
            InitializeComponent();
            if (!eng_localization)
            {
                transl_1.Text = "Інформація про активи";
                transl_2.Header = "назва";
                transl_3.Header = "ціна";
                transl_4.Header = "капіталізація млрд дол";
                transl_5.Header = "добові обсяги в млрд дол";
                transl_6.Header = "відсоток зміни 24год";
            }
            data_assets_copy = data_assets;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lv1.ItemsSource = data_assets_copy;
        }
    }
}
