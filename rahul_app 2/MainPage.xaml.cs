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
using Microsoft.Phone.Marketplace;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using Newtonsoft.Json;
using Microsoft.Phone.Notification;
using System.Text;
using System.IO.IsolatedStorage;
using Microsoft.Phone.BackgroundAudio;
using System.Windows.Media.Imaging;
using Windows.Storage;
using System.Text.RegularExpressions;


namespace rahul_app_2
{
    public partial class MainPage : PhoneApplicationPage
    {
        static int c=0;
        MarketplaceDetailTask _marketPlaceDetailTask = new MarketplaceDetailTask();
        //PeriodicTask periodicTask;
        //string periodicTaskName = "PeriodicAgent";
        public bool agentsAreEnabled = true;
        static int internet_check = 0;
        // Constructor
        public MainPage()
        {
            //HttpNotificationChannel pushChannel;
            InitializeComponent();
            InstallVoiceCommands();
            BackgroundAudioPlayer.Instance.PlayStateChanged += new EventHandler(Instance_PlayStateChanged);
            if (!IsolatedStorageSettings.ApplicationSettings.Contains("IsFirstLaunchDone"))
            {
                MessageBox.Show("Thank you for downloading hit refresh once to update livetile");
                c = 0;
                player_load();
                IsolatedStorageSettings.ApplicationSettings["IsFirstLaunchDone"] = true;
            } 
            else
            {
                new_fun();
                new_fuction();
                c = 1;
                //MessageBox.Show("NOT First Launch Message");
                
            }
           
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
           
            BackgroundAudioPlayer.Instance.PlayStateChanged += new EventHandler(Instance_PlayStateChanged);
            // The name of our push channel.
            

           

           
        }

        //private void Instance_PlayStateChanged(object sender, EventArgs e)
        //{
        //    if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
        //    {
        //        song.Opacity = 100;
        //        song1.Opacity = 100;
        //        song.Text = BackgroundAudioPlayer.Instance.Track.Title +
        //                                  " by " +
        //                                  BackgroundAudioPlayer.Instance.Track.Artist;
        //        BitmapImage bi3 = new BitmapImage();
        //        if (BackgroundAudioPlayer.Instance.Track.Title == "Ringtone 1")
        //        {
                    

        //            bi3.UriSource = new Uri("/images/track1.jpg", UriKind.Relative);

        //            song1.Stretch = Stretch.Fill;
        //            song1.Source = bi3;
        //        }
        //        else if(BackgroundAudioPlayer.Instance.Track.Title == "Ringtone 2")
        //        {
                    
                    

        //            bi3.UriSource = new Uri("/images/track2.jpg", UriKind.Relative);

        //            song1.Stretch = Stretch.Fill;
        //            song1.Source = bi3;
        //        }
        //    }
        //    else
        //    {
        //        song.Opacity = 0;
        //        song1.Opacity = 0;
        //    }
        //}
     
        private void buttonNavigate_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/push.xaml?NavigatedFrom=Main Page", UriKind.Relative));
        }
        private async void InstallVoiceCommands()
        {
            await VoiceCommandService.InstallCommandSetsFromFileAsync(new Uri("ms-appx:///VCDChelsea.xml"));
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
            if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
            {
                var imageBrush = new ImageBrush();

                imageBrush = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("/settin/transport.pause.png", UriKind.Relative))
                };
                playButton.Background = imageBrush;

                //playButton.Content = "| |";     // Change to pause button
                txtCurrentTrack.Text = BackgroundAudioPlayer.Instance.Track.Title +
                                       " by " +
                                       BackgroundAudioPlayer.Instance.Track.Artist;

            }
            else
            {
                //playButton.Content = ">";     // Change to play button
                txtCurrentTrack.Text = "";
            }
            
            base.OnNavigatedTo(e);
            ApplicationBarIconButton btn_buy = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            if ((Application.Current as App).IsTrial)
            {
                btn_buy.IsEnabled = true;
            }
            else
            {
                btn_buy.IsEnabled = false;
               
            }
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
        void Instance_PlayStateChanged(object sender, EventArgs e)
        {
            switch (BackgroundAudioPlayer.Instance.PlayerState)
            {
                case PlayState.Playing:
                    var imageBrush = new ImageBrush();
                  
                    imageBrush = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("/settin/transport.pause.png", UriKind.Relative))
                    };
                    playButton.Background = imageBrush;
                    
                    //playButton.Content = "| |";     // Change to pause button
                    prevButton.IsEnabled = true;
                    nextButton.IsEnabled = true;
                    break;

                case PlayState.Paused:
                case PlayState.Stopped:
                    var imageBrush2 = new ImageBrush();

                    imageBrush2 = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("/settin/transport.play.png", UriKind.Relative))
                    };
                    playButton.Background = imageBrush2;
                    //playButton.Content = ">";     // Change to play button
                    break;
            }

            if (null != BackgroundAudioPlayer.Instance.Track)
            {
                txtCurrentTrack.Text = BackgroundAudioPlayer.Instance.Track.Title +
                                       " by " +
                                       BackgroundAudioPlayer.Instance.Track.Artist;
            }
        }
        #region Button Click Event Handlers

        /// <summary>
        /// Tells the background audio agent to skip to the previous track.
        /// </summary>
        /// <param name="sender">The button</param>
        /// <param name="e">Click event args</param>
        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            BackgroundAudioPlayer.Instance.SkipPrevious();

            // Prevent the user from repeatedly pressing the button and causing 
            // a backlong of button presses to be handled. This button is re-eneabled 
            // in the TrackReady Playstate handler.
            prevButton.IsEnabled = false;
        }


        /// <summary>
        /// Tells the background audio agent to play the current 
        /// track or to pause if we're already playing.
        /// </summary>
        /// <param name="sender">The button</param>
        /// <param name="e">Click event args</param>
        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
            {
                BackgroundAudioPlayer.Instance.Pause();
            }
            else
            {
                BackgroundAudioPlayer.Instance.Play();
            }

        }

        /// <summary>
        /// Tells the background audio agent to skip to the next track.
        /// </summary>
        /// <param name="sender">The button</param>
        /// <param name="e">Click event args</param>
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            BackgroundAudioPlayer.Instance.SkipNext();

            // Prevent the user from repeatedly pressing the button and causing 
            // a backlong of button presses to be handled. This button is re-eneabled 
            // in the TrackReady Playstate handler.
            nextButton.IsEnabled = false;
        }

        #endregion Button Click Event Handlers

  



        public void player_load()
        {
            WebClient webClient = new WebClient();
            WebClient webClient1 = new WebClient();
            try
            {
                if (c == 1)
                {
                    if (MessageBox.Show("Data will be loaded again?", "Are you sure?",
                                         MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        webClient1.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);

                        // Download the RSS feed. DownloadStringAsync was used instead of OpenStreamAsync because we do not need 
                        // to leave a stream open, and we will not need to worry about closing the channel. 
                        webClient1.DownloadStringAsync(new System.Uri("http://talksport.com/rss/football/chelsea/feed"));
                        webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(player_DownloadStringCompleted);
                        webClient.DownloadStringAsync(new Uri("http://che.advrdt.com/?json=get_category_posts&slug=playing11&count=11"));

                    }
                    else
                    {

                    }
                }
                else
                {
                    webClient1.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);

                    // Download the RSS feed. DownloadStringAsync was used instead of OpenStreamAsync because we do not need 
                    // to leave a stream open, and we will not need to worry about closing the channel. 
                    webClient1.DownloadStringAsync(new System.Uri("http://talksport.com/rss/football/chelsea/feed"));
                    webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(player_DownloadStringCompleted);
                    webClient.DownloadStringAsync(new Uri("http://che.advrdt.com/?json=get_category_posts&slug=playing11&count=11"));
                }
            }
            catch
            {
                MessageBox.Show("Check the Internet Connection!!");
            }
          }

        private async Task WriteToFile(string hello)
        {
            // Get the text data from the textbox. 
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(hello);

            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            // Create a new folder name DataFolder.
            var dataFolder = await local.CreateFolderAsync("DataFolder",
                CreationCollisionOption.OpenIfExists);

            // Create a new file named DataFile.txt.
            var file = await dataFolder.CreateFileAsync("jsonchelsea.txt",
            CreationCollisionOption.ReplaceExisting);

            // Write the data from the textbox.
            using (var s = await file.OpenStreamForWriteAsync())
            {
                s.Write(fileBytes, 0, fileBytes.Length);
            
           }
            //MessageBox.Show("Inside writter");
        }
        private async Task news_WriteToFile(string hello)
        {
            // Get the text data from the textbox. 
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(hello);

            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            // Create a new folder name DataFolder.
            var dataFolder = await local.CreateFolderAsync("DataFolder",
                CreationCollisionOption.OpenIfExists);

            // Create a new file named DataFile.txt.
            var file = await dataFolder.CreateFileAsync("newschelsea.txt",
            CreationCollisionOption.ReplaceExisting);

            // Write the data from the textbox.
            using (var s = await file.OpenStreamForWriteAsync())
            {
                s.Write(fileBytes, 0, fileBytes.Length);

            }
            //MessageBox.Show("Inside writter");
        }
       
        public static void UpdateMainTile(string name)
            {
                //Get application's main tile
               
                var mainTile = ShellTile.ActiveTiles.FirstOrDefault();

               
                if (null != mainTile)
                {
                    IconicTileData tileData = new IconicTileData()
                    {
                       
                        BackgroundColor = System.Windows.Media.Color.FromArgb(10, 69, 140, 100),
                        Title ="Chelsea FC latest News",
                        IconImage = new Uri("/Assets/Tiles/icon.png", UriKind.RelativeOrAbsolute),
                        SmallIconImage = new Uri("/Assets/Tiles/icon.png", UriKind.RelativeOrAbsolute),
                        WideContent1 = name,
                        WideContent2 = "",
                        WideContent3 = "" 
                    };

                    mainTile.Update(tileData);
                }
            }
        string anoop;
        private async Task ReadFile()
        {
            // Get the local folder.
            try
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

                if (local != null)
                {
                    // Get the DataFolder folder.
                    var dataFolder = await local.GetFolderAsync("DataFolder");

                    // Get the file.
                    var file = await dataFolder.OpenStreamForReadAsync("jsonchelsea.txt");

                    // Read the data.
                    using (StreamReader streamReader = new StreamReader(file))
                    {
                        anoop = streamReader.ReadToEnd();
                    }
                    //MessageBox.Show(anoop);
                }
            }
            catch
            {
                if (internet_check == 0)
                {
                    MessageBox.Show("No Internet!!");
                    internet_check = 1;
                }
                else
                {

                }
            }
        }
        string news;
        string tiletext;
        private async Task news_ReadFile()
        {
            int counter=0;
            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
            foreach (SyndicationItem news_n in feedListBox.Items)
            {

                //MessageBox.Show("Updated live tiles");
                if (counter == 0) {  
                    tiletext = news_n.Title.Text.ToString();
                UpdateMainTile(news_n.Title.Text.ToString());
                counter++;
                }
            }
            if (local != null)
            {
                // Get the DataFolder folder.
                var dataFolder = await local.GetFolderAsync("DataFolder");

                // Get the file.
                var file = await dataFolder.OpenStreamForReadAsync("newschelsea.txt");

                // Read the data.
                using (StreamReader streamReader = new StreamReader(file))
                {
                    news = streamReader.ReadToEnd();
                }
               
                //MessageBox.Show(news);
            }
        }
       async void player_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            
           try
           {
               await WriteToFile(e.Result.ToString());
               await ReadFile();
               
                var rootObject = JsonConvert.DeserializeObject<players>(anoop);


                player.ItemsSource = rootObject.posts;



                foreach (var blog in rootObject.posts)
                {
                    blog.content = html_tag.StripTagsCharArray(blog.content);
                    if (blog.custom_fields.imgurl != null)
                    {
                        List<string> logo_rl = blog.custom_fields.imgurl;
                        blog.logo = logo_rl[0];
                        List<string> position_r1 = blog.custom_fields.position;
                        blog.position = position_r1[0];
                        if(blog.custom_fields.starplayer!=null)
                        {
                            blog.imp = blog.custom_fields.starplayer[0];
                        }
                        //MessageBox.Show(logo_rl[0].ToString());
                    }
                }
            }
            catch 
            {
               // MessageBox.Show(abc.ToString());
                if (internet_check == 0)
                {
                    MessageBox.Show("Unable to connect to our server. Check internet connectivity!");
                    internet_check = 1;
                }
                else
                {

                }
                
            }
        }
        public async void new_fun()
        {
            
            try
            {
               
                await ReadFile();
                
                var rootObject = JsonConvert.DeserializeObject<players>(anoop);


                player.ItemsSource = rootObject.posts;



                foreach (var blog in rootObject.posts)
                {
                    blog.content = html_tag.StripTagsCharArray(blog.content);
                    if (blog.custom_fields.imgurl != null)
                    {
                        List<string> logo_rl = blog.custom_fields.imgurl;
                        blog.logo = logo_rl[0];
                        List<string> position_r1 = blog.custom_fields.position;
                        blog.position = position_r1[0];
                        if (blog.custom_fields.starplayer != null)
                        {
                            blog.imp = blog.custom_fields.starplayer[0];
                        }
                        //MessageBox.Show(logo_rl[0].ToString());
                    }
                }
            }
            catch 
            {
                //MessageBox.Show(abc.ToString());
                if (internet_check == 0)
                {
                    MessageBox.Show("Unable to connect to our server. Check internet connectivity!");
                    internet_check = 1;
                }
                else
                {

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
            //LockScreenChange("wallpaper/Chelsea_0.jpg", true);
            
            //Do work for you   r application here.
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

        private void Fixture(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/News.xaml", UriKind.Relative));
        }
        private void Fixture2(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Table.xaml", UriKind.Relative));
        }
        private void Panorama_Loaded(object sender, RoutedEventArgs e)
        {
           

        }
        DateTime exit_con = new DateTime();
        long LastExitAttemptTick;
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
          
               
                long thisTick = exit_con.Ticks;

                if (LastExitAttemptTick - thisTick < 10000)
                    if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit?",
                                       MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {



                    }
                    else
                    {
                        e.Cancel = true;
                    } //You can use XNA, but this is a quick and dirty way of exiting
                else
                    LastExitAttemptTick = exit_con.Ticks;
            
                
               
            
        }
        private void LongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
       // private void changelock(object sender, RoutedEventArgs e)
        //{
         //   LockScreenChange("wallpaper/Chelsea_0.jpg", true);
         //   StartPeriodicAgent();
        //}
        
        //private void StartPeriodicAgent()
        //{
        //    // is old task running, remove it
        //    periodicTask = ScheduledActionService.Find(periodicTaskName) as PeriodicTask;
        //    if (periodicTask != null)
        //    {
        //        try
        //        {
        //            ScheduledActionService.Remove(periodicTaskName);
        //        }
        //        catch (Exception)
        //        {
                   
        //        }
        //    }
        //    // create a new task
           
        //    periodicTask = new PeriodicTask(periodicTaskName);
        //    // load description from localized strings
        //    periodicTask.Description = "This is Lockscreen image provider app.";
        //    // set expiration days
        //    periodicTask.ExpirationTime = DateTime.Now.AddDays(14);
        //    try
        //    {
        //        // add thas to scheduled action service
        //        ScheduledActionService.Add(periodicTask);
        //        // debug, so run in every 30 secs

        //        ScheduledActionService.LaunchForTest(periodicTaskName, TimeSpan.FromSeconds(30));
        //        System.Diagnostics.Debug.WriteLine("Periodic task is started: " + periodicTaskName);


        //    }
        //    catch (InvalidOperationException exception)
        //    {
        //        if (exception.Message.Contains("BNS Error: The action is disabled"))
        //        {
        //            // load error text from localized strings
        //            MessageBox.Show("Background agents for this application have been disabled by the user.");
        //        }
        //        if (exception.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
        //        {
        //            // No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.
        //        }
        //    }
        //    catch (SchedulerServiceException)
        //    {
        //       // No user action required.
        //    }
        //}

        //private async void LockScreenChange(string filePathOfTheImage, bool isAppResource)
        //{
        //    if (!LockScreenManager.IsProvidedByCurrentApplication)
        //    {
        //        // If you're not the provider, this call will prompt the user for permission.
        //        // Calling RequestAccessAsync from a background agent is not allowed.
        //        await LockScreenManager.RequestAccessAsync();
        //    }

        //    // Only do further work if the access is granted.
        //    if (LockScreenManager.IsProvidedByCurrentApplication)
        //    {
        //        // At this stage, the app is the active lock screen background provider.
        //        // The following code example shows the new URI schema.
        //        // ms-appdata points to the root of the local app data folder.
        //        // ms-appx points to the Local app install folder, to reference resources bundled in the XAP package
        //        var schema = isAppResource ? "ms-appx:///" : "ms-appdata:///Local/";
        //        var uri = new Uri(schema + filePathOfTheImage, UriKind.Absolute);

        //        // Set the lock screen background image.
        //        LockScreen.SetImageUri(uri);

        //        // Get the URI of the lock screen background image.
        //        var currentImage = LockScreen.GetImageUri();
        //        System.Diagnostics.Debug.WriteLine("The new lock screen background image is set to {0}", currentImage.ToString());
        //        MessageBox.Show("Lock screen changed.");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Background cant be updated as you clicked no!!");
        //    }
        //}
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
        private async void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                
                await news_WriteToFile(e.Result.ToString());
                
                await news_ReadFile();
                
                if (e.Error != null)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        // Showing the exact error message is useful for debugging. In a finalized application, 
                        // output a friendly and applicable string to the user instead. 
                        if (internet_check == 0)
                        {
                            MessageBox.Show("Unable to connect to our server. Check internet connectivity!");
                            internet_check = 1;
                        }
                        else
                        {

                        }
                    });
                }
                else
                {
                    // Save the feed into the State property in case the application is tombstoned. 

                    this.State["feed"] = news;

                    UpdateFeedList(news);
                }
            }
            catch
            {
                
            }
        }
       
        private async void new_fuction()
        {
            try
            {
                await news_ReadFile();

               
                  // Save the feed into the State property in case the application is tombstoned. 

                    this.State["feed"] = news;

                    UpdateFeedList(news);
                    create_news();
                
            }
            catch
            {

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
        //private void loadfeed()
        //{
        //    WebClient webClient = new WebClient();

        //    // Subscribe to the DownloadStringCompleted event prior to downloading the RSS feed.
            
        //}
        //private void loadFeedButton_Click(object sender, EventArgs e)
        //{
        //    player_load();
        //    WebClient webClient = new WebClient();
           
        //    //// Subscribe to the DownloadStringCompleted event prior to downloading the RSS feed.
        //    //webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);

        //    //// Download the RSS feed. DownloadStringAsync was used instead of OpenStreamAsync because we do not need 
        //    //// to leave a stream open, and we will not need to worry about closing the channel. 
        //    //webClient.DownloadStringAsync(new System.Uri("http://feeds.feedburner.com/ChelseaFCRumourMill"));
            
        //}

        private void about_nav(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        }

        private void buy_now(object sender, EventArgs e)
        {
            _marketPlaceDetailTask.Show();
            
        }

        private void social_click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Squad.xaml", UriKind.Relative));
            
        }

        private void settings_Tap(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/settings.xaml", UriKind.Relative));
           
        }
        //private void state_music()
        //{
        //    if (PlayState.Playing == BackgroundAudioPlayer.Instance.PlayerState)
        //    {
        //        song.Opacity = 100;
        //    }
        //    else
        //    {
        //        song.Opacity = 0;
        //    }
        //}
        public void player_loadpl()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(playerpl_DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri("http://che.advrdt.com/?json=get_category_posts&slug=subs&count=30"));
        }
        async void playerpl_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {

            try
            {
                await WriteToFile_pl(e.Result.ToString());
                

               
            }
            catch
            {
                // MessageBox.Show(abc.ToString());
                  MessageBox.Show("Unable to connect to our server. Check internet connectivity!");
               
            }
        }
        PeriodicTask periodicTask;
        string periodicTaskName = "PeriodicAgent";
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

                //ScheduledActionService.LaunchForTest(periodicTaskName, TimeSpan.FromSeconds(30));
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
        private async Task WriteToFile_pl(string hello)
        {
            // Get the text data from the textbox. 
            byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(hello);

            // Get the local folder.
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            // Create a new folder name DataFolder.
            var dataFolder = await local.CreateFolderAsync("DataFolder",
                CreationCollisionOption.OpenIfExists);

            // Create a new file named DataFile.txt.
            var file = await dataFolder.CreateFileAsync("jsonchelsea2.txt",
            CreationCollisionOption.ReplaceExisting);

            // Write the data from the textbox.
            using (var s = await file.OpenStreamForWriteAsync())
            {
                s.Write(fileBytes, 0, fileBytes.Length);

            }
            //MessageBox.Show("Inside writter");
        }
        private void player_load1(object sender, EventArgs e)
        {
                   
         
            
            //foreach (SyndicationItem news_n in feedListBox.Items)
            //{
                
            //    MessageBox.Show(news_n.Title.Text.ToString());
            //}
            //MessageBox.Show(feedListBox.Items.Count.ToString());
            
            //string day = DateTime.Now.Day.ToString();
            //string month = DateTime.Now.Month.ToString();
            //string year = DateTime.Now.Year.ToString();

            //date.Text = day+"."+month+"."+year;
            player_load();
        }
        
        public void create_news()
        {
            foreach (SyndicationItem news_n in feedListBox.Items)
            {

                MessageBox.Show(news_n.Title.Text.ToString());
            }
        }
        }
   

    }