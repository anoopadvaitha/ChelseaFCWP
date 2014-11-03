using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Diagnostics;

namespace rahul_app_2
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Page1_Loaded);  
        }
        void Page1_Loaded(object sender, RoutedEventArgs e)
        {
            WebClient feber = new WebClient();
            feber.DownloadStringCompleted += new DownloadStringCompletedEventHandler(feber_DownloadStringCompleted);
            feber.DownloadStringAsync(new Uri("http://blogs.chelseafc.com/?feed=rss2", UriKind.Absolute));
        }  
        void feber_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XElement rssFeed = XElement.Parse(e.Result);
            listbox1.ItemsSource = from item in rssFeed.Descendants("item")
                                   select new FeberItem
                                   {
                                       Title = item.Element("title").Value,
                                       ImageURL = imgsrc(item.Element("description").Value),
                                       //Description = item.Element("description").Value,  
                                   };
        }

        string imgsrc(string content)
        {
            // search for <img> tags 
            Regex searchimg = new Regex("<img.*?>");

            Debug.WriteLine("item");
            string firstresult = "";
            foreach (Match m in searchimg.Matches(content))
            {
                // extract the src attribute 
                Regex searchsrc = new Regex("(?<=src=\").*?(?=\")");
                string result = searchsrc.Match(m.ToString()).ToString();
                if (firstresult == "")
                    firstresult = result;
                Debug.WriteLine(result);
            }

            // Only the first match is returned. See other matches in the debug output 
            return firstresult;
        }


        public class FeberItem
        {
            public string Title { get; set; }
            public string ImageURL { get; set; }
            //public string Description { get; set; }  
        } 
    }
}