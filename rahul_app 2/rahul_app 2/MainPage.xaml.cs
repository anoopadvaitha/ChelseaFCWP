using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Windows.Phone.System.UserProfile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Windows.Foundation;
using Windows.Phone.Speech.Recognition;
using Windows.Phone.Speech.Synthesis;
using Windows.Phone.Speech.VoiceCommands;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.Phone.Tasks;
using System.ServiceModel.Syndication;
using System.Linq;

namespace rahul_app_2
{
    public partial class MainPage : PhoneApplicationPage
    {
      
        PeriodicTask periodicTask;
        string periodicTaskName = "PeriodicAgent";
        public bool agentsAreEnabled = true;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            InstallVoiceCommands();
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
        }
        
        private async void InstallVoiceCommands()
        {
            await VoiceCommandService.InstallCommandSetsFromFileAsync(
     new Uri("ms-appx:///VCDChelsea.xml"));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
                  {
                App.ViewModel.LoadData();
                }
            if (this.State.ContainsKey("feed"))
            {
                // Get the feed again only if the application was tombstoned, which means the ListBox will be empty.
                // This is because the OnNavigatedTo method is also called when navigating between pages in your application.
                // You would want to rebind only if your application was tombstoned and page state has been lost. 
                if (feedListBox.Items.Count == 0)
                {
                    UpdateFeedList(State["feed"] as string);
                }
            }
            
            base.OnNavigatedTo(e);

            // Is this a new activation or a resurrection from tombstone?
            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.New)
            {

                // Was the app launched using a voice command?
                if (NavigationContext.QueryString.ContainsKey("voiceCommandName"))
                {

                    // If so, get the name of the voice command.
                    string voiceCommandName
                      = NavigationContext.QueryString["voiceCommandName"];

                    // Define app actions for each voice command name.
                    switch (voiceCommandName)
                    {
                        case "showWidgets":
                            // Add code to display specified widgets.
                            break;
                        case "Table":
                            break;
                        case "News":
                            break;
                        // Add cases for other voice commands. 
                        default:

                            // There is no match for the voice command name.
                            break;
                    }
                }
            }
        }
        // Load data for the ViewModel Items
        //protected override void OnNavigatedTo(NavigationEventArgs e)
       // {
       //     if (!App.ViewModel.IsDataLoaded)
       //     {
         //      App.ViewModel.LoadData();
      //      }
     //   }
        private void MenuItem1_Click(object sender, EventArgs e)
        {
            
            LockScreenChange("wallpaper/Chelsea_0.jpg", true);
            StartPeriodicAgent();
            //Do work for you   r application here.
        }
        private void Fixture(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Fixture.xaml", UriKind.Relative));
        }
        private void Fixture2(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Table.xaml", UriKind.Relative));
        }
        private void Panorama_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void LongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
       // private void changelock(object sender, RoutedEventArgs e)
        //{
         //   LockScreenChange("wallpaper/Chelsea_0.jpg", true);
         //   StartPeriodicAgent();
        //}
        
        private void StartPeriodicAgent()
        {
            // is old task running, remove it
            periodicTask = ScheduledActionService.Find(periodicTaskName) as PeriodicTask;
            if (periodicTask != null)
            {
                try
                {
                    ScheduledActionService.Remove(periodicTaskName);
                }
                catch (Exception)
                {
                   
                }
            }
            // create a new task
            periodicTask = new PeriodicTask(periodicTaskName);
            // load description from localized strings
            periodicTask.Description = "This is Lockscreen image provider app.";
            // set expiration days
            periodicTask.ExpirationTime = DateTime.Now.AddDays(14);
            try
            {
                // add thas to scheduled action service
                ScheduledActionService.Add(periodicTask);
                // debug, so run in every 30 secs

                ScheduledActionService.LaunchForTest(periodicTaskName, TimeSpan.FromMinutes(30));
                System.Diagnostics.Debug.WriteLine("Periodic task is started: " + periodicTaskName);


            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    // load error text from localized strings
                    MessageBox.Show("Background agents for this application have been disabled by the user.");
                }
                if (exception.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {
                    // No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.
                }
            }
            catch (SchedulerServiceException)
            {
               // No user action required.
            }
        }

        private async void LockScreenChange(string filePathOfTheImage, bool isAppResource)
        {
            if (!LockScreenManager.IsProvidedByCurrentApplication)
            {
                // If you're not the provider, this call will prompt the user for permission.
                // Calling RequestAccessAsync from a background agent is not allowed.
                await LockScreenManager.RequestAccessAsync();
            }

            // Only do further work if the access is granted.
            if (LockScreenManager.IsProvidedByCurrentApplication)
            {
                // At this stage, the app is the active lock screen background provider.
                // The following code example shows the new URI schema.
                // ms-appdata points to the root of the local app data folder.
                // ms-appx points to the Local app install folder, to reference resources bundled in the XAP package
                var schema = isAppResource ? "ms-appx:///" : "ms-appdata:///Local/";
                var uri = new Uri(schema + filePathOfTheImage, UriKind.Absolute);

                // Set the lock screen background image.
                LockScreen.SetImageUri(uri);

                // Get the URI of the lock screen background image.
                var currentImage = LockScreen.GetImageUri();
                System.Diagnostics.Debug.WriteLine("The new lock screen background image is set to {0}", currentImage.ToString());
                MessageBox.Show("Lock screen changed.");
            }
            else
            {
                MessageBox.Show("Background cant be updated as you clicked no!!");
            }
        }
        //private void loadFeedButton_Click(object sender, System.Windows.RoutedEventArgs e)
       // {
           // WebClient is used instead of HttpWebRequest in this code sample because 
            // the implementation is simpler and easier to use, and we do not need to use 
            // advanced functionality that HttpWebRequest provides, such as the ability to send headers.
        //    WebClient webClient = new WebClient();

            // Subscribe to the DownloadStringCompleted event prior to downloading the RSS feed.
       //     webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);

            // Download the RSS feed. DownloadStringAsync was used instead of OpenStreamAsync because we do not need 
            // to leave a stream open, and we will not need to worry about closing the channel. 
        //      webClient.DownloadStringAsync(new System.Uri("http://blogs.chelseafc.com/?feed=rss2"));
      //  }

        // Event handler which runs after the feed is fully downloaded.
        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    // Showing the exact error message is useful for debugging. In a finalized application, 
                    // output a friendly and applicable string to the user instead. 
                    MessageBox.Show(e.Error.Message);
                });
            }
            else
            {
                // Save the feed into the State property in case the application is tombstoned. 
                this.State["feed"] = e.Result;

                UpdateFeedList(e.Result);
            }
        }

        // This method determines whether the user has navigated to the application after the application was tombstoned.
       // protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
      //  {
            // First, check whether the feed is already saved in the page state.
      //      if (this.State.ContainsKey("feed"))
       //     {
      //          // Get the feed again only if the application was tombstoned, which means the ListBox will be empty.
                // This is because the OnNavigatedTo method is also called when navigating between pages in your application.
                // You would want to rebind only if your application was tombstoned and page state has been lost. 
        //        if (feedListBox.Items.Count == 0)
       //         {
       //             UpdateFeedList(State["feed"] as string);
       //         }
      //      }
        //   }

        // This method sets up the feed and binds it to our ListBox. 
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
                feedListBox.ItemsSource = feed.Items;

                loadFeedButton.Content = "Refresh Feed";
            });
        }

        // The SelectionChanged handler for the feed items 
        private void feedListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (listBox != null && listBox.SelectedItem != null)
            {
                // Get the SyndicationItem that was tapped.
                SyndicationItem sItem = (SyndicationItem)listBox.SelectedItem;

                // Set up the page navigation only if a link actually exists in the feed item.
                if (sItem.Links.Count > 0)
                {
                    // Get the associated URI of the feed item.
                    Uri uri = sItem.Links.FirstOrDefault().Uri;

                    // Create a new WebBrowserTask Launcher to navigate to the feed item. 
                    // An alternative solution would be to use a WebBrowser control, but WebBrowserTask is simpler to use. 
                    WebBrowserTask webBrowserTask = new WebBrowserTask();
                    webBrowserTask.Uri = uri;
                    webBrowserTask.Show();
                }
            }
        }

        private void loadFeedButton_Click(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();

            // Subscribe to the DownloadStringCompleted event prior to downloading the RSS feed.
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);

            // Download the RSS feed. DownloadStringAsync was used instead of OpenStreamAsync because we do not need 
            // to leave a stream open, and we will not need to worry about closing the channel. 
            webClient.DownloadStringAsync(new System.Uri("http://feeds.feedburner.com/ChelseaFCRumourMill"));
        }

        private void about_nav(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Page1.xaml", UriKind.Relative));
        }
       

        }
   

    }