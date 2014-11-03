using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.ComponentModel;


namespace rahul_app_2
{
    public partial class About : PhoneApplicationPage
    {
        MarketplaceDetailTask _marketPlaceDetailTask = new MarketplaceDetailTask();
        public About()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowInBrowser("http://www.chelseafc.com");
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            ShowInBrowser("http://www.advaithastudios.com");
        }
        private void ShowInBrowser(string url)
        {
            Microsoft.Phone.Tasks.WebBrowserTask wbt = new Microsoft.Phone.Tasks.WebBrowserTask();
            wbt.Uri = new Uri(url);
            wbt.Show();
        }
        private void buy_now(object sender, EventArgs e)
        {
            _marketPlaceDetailTask.Show();
        }
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}