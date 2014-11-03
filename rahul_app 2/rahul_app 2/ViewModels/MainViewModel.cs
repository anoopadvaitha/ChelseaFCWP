using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using rahul_app_2.Resources;

namespace rahul_app_2.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            // Sample data; replace with real data
            this.Items.Add(new ItemViewModel() { LineOne = "Peter Cech", LineTwo = "Goal Keeper", LineThree = "/listimg/1.png",LineFour="Petr Cech is our all-time record clean sheet holder, having surpassed Peter Bonetti’s total in January 2014, and our foreign player with the most appearances of all time." });
            this.Items.Add(new ItemViewModel() { LineOne = "Branislav Ivanovic", LineTwo = "Defender", LineThree = "/listimg/2.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Filipe Luis", LineTwo = "Defender", LineThree = "/listimg/3.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Cesc Fabregas", LineTwo = "Midfielder", LineThree = "/listimg/4.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Kurt Zouma", LineTwo = "Defender", LineThree = "/listimg/5.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Ramires", LineTwo = "Midfielder", LineThree = "/listimg/7.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Fernando Torres", LineTwo = "Striker", LineThree = "/listimg/9.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Eden Hazard", LineTwo = "Midfielder", LineThree = "/listimg/10.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Oscar", LineTwo = "Midfielder", LineThree = "/listimg/11.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "John Mikel Obi", LineTwo = "Midfielder", LineThree = "/listimg/12.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Andre Schurrle", LineTwo = "Midfielder", LineThree = "/listimg/14.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Didier Drogba", LineTwo = "Striker", LineThree = "/listimg/15.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Marco van Ginkel", LineTwo = "Midfielder", LineThree = "/listimg/16.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Mohamed Salah", LineTwo = "Midfielder", LineThree = "/listimg/17.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Diego Costa", LineTwo = "Striker", LineThree = "/listimg/19.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Nemanja Matic", LineTwo = "Midfielder", LineThree = "/listimg/21.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Willian", LineTwo = "Midfielder", LineThree = "/listimg/22.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Mark Schwarzer", LineTwo = "Goal Keeper", LineThree = "/listimg/23.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Gary Cahill", LineTwo = "Defender", LineThree = "/listimg/24.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "John Terry", LineTwo = "Defender", LineThree = "/listimg/26.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Nathan Ake", LineTwo = "Defender", LineThree = "/listimg/27.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Cesar Azpilicueta", LineTwo = "Defender", LineThree = "/listimg/28.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Jamal Blackman", LineTwo = "Goal Keeper", LineThree = "/listimg/46.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Thibaut Courtois", LineTwo = "Goal Keeper", LineThree = "/listimg/rest2.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Marko Marin", LineTwo = "Midfielder", LineThree = "/listimg/rest1.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Victor Moses", LineTwo = "Midfielder", LineThree = "/listimg/rest3.png" });
            
            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}