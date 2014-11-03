using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace rahul_app_2
{
    public partial class Fixture : PhoneApplicationPage
    {
        public Fixture()
        {
            InitializeComponent();
        }

        private void Fixture_click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Fixture.xaml", UriKind.Relative));
        }
        private void Fixture2(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Table.xaml", UriKind.Relative));
        }
        private void News(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/News.xaml", UriKind.Relative));
        }

        private void refresh(object sender, EventArgs e)
        {
            WebBr.Navigate(new Uri("http://www.chelseafc.com/matches/fixtures---results.html", UriKind.Absolute));
        }

        private void Main_menu(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

    }
}