using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using System.Xml;
using System.ServiceModel.Syndication;

namespace rahul_app_2
{
    public partial class news : PhoneApplicationPage
    {
        public news()
        {
            InitializeComponent();
            player_load();
        }
        public void player_load()
        {
            WebClient fb365 = new WebClient();
            fb365.DownloadStringCompleted += new DownloadStringCompletedEventHandler(fb365_DownloadStringCompleted);

            // Download the RSS feed. DownloadStringAsync was used instead of OpenStreamAsync because we do not need 
            // to leave a stream open, and we will not need to worry about closing the channel. 
            fb365.DownloadStringAsync(new System.Uri("http://talksport.com/rss/football/chelsea/feed"));
        }
        private void fb365_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                
                if (e.Error != null)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        // Showing the exact error message is useful for debugging. In a finalized application, 
                        // output a friendly and applicable string to the user instead. 
                      
                            
                       
                       
                    });
                }
                else
                {
                    // Save the feed into the State property in case the application is tombstoned. 

                    this.State["feed"] = e.Result.ToString();

                    UpdateFeedList(e.Result.ToString());
                }
            }
            catch
            {

            }
        }
        private void UpdateFeedList(string feedXML)
        {
            // Load the feed into a SyndicationFeed instance.
            StringReader stringReader = new StringReader(feedXML);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);

            // In Windows Phone OS 7.1 or later versions, WebClient events are raised on the same type of thread they were called upon. 
            // For example, if WebClient was run on a background thread, the event would be raised on the background thread. 
            // While WebClient can raise an event on the UI thread if called from the UI thread, a best practice is to always 
            // use the Dispatcher to update the UI. This keeps the UI thread free from heavy processing.
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                // Bind the list of SyndicationItems to our ListBox.
                fb365.ItemsSource = feed.Items;
            });
        }

        private void player_load1(object sender, EventArgs e)
        {
            player_load();
        }

        private void fb365_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
       
    }
}