using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Windows.Storage;
using System.IO;
using System.ComponentModel;

namespace rahul_app_2
{
    public partial class Squad : PhoneApplicationPage
    {
        public Squad()
        {
            InitializeComponent();
            player_load();
            new_func();
        }
        public string anoop;
        public string anooppl;
        async void player_load()
        {
            try
            {

                await ReadFile();

                var rootObject = JsonConvert.DeserializeObject<players>(anoop);


                suber.ItemsSource = rootObject.posts;



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
                // MessageBox.Show(abc.ToString());
                MessageBox.Show("Unable to connect to our server. Check internet connectivity!");

            }
        }
       
       async void   new_func()
        {
            await ReadFile_pl();

                var rootObject = JsonConvert.DeserializeObject<players>(anooppl);
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
                    var file = await dataFolder.OpenStreamForReadAsync("jsonchelsea2.txt");

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
                    MessageBox.Show("No Internet!!");
            }
        }
        private async Task ReadFile_pl()
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
                        anooppl = streamReader.ReadToEnd();
                    }
                    //MessageBox.Show(anoop);
                }
            }
            catch
            {
                MessageBox.Show("No Internet!!");
            }
        }
        private void social_click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Squad.xaml", UriKind.Relative));
        }
        private void Fixture(object sender, EventArgs e)
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

        
        private void Main_menu(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
        }
}