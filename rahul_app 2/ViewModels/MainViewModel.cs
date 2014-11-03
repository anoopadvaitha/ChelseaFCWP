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
            this.Items.Add(new ItemViewModel() { LineOne = "Peter Cech", LineTwo = "Goal Keeper", LineThree = "/listimg/1.png", LineFour = "Jose Mourinho", LineFive = "Manager",LineSix = "/listimg/jose.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Branislav Ivanovic", LineTwo = "Defender", LineThree = "/listimg/2.png", LineFour = "Steve Holland", LineFive = "Assistant Coach", LineSix = "/listimg/steveholland.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Filipe Luis", LineTwo = "Defender", LineThree = "/listimg/3.png", LineFour = "Silvino Louro", LineFive = "Assistant Coach", LineSix = "/listimg/silvino.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Cesc Fabregas", LineTwo = "Midfielder", LineThree = "/listimg/4.png", LineFour = "Rui Faria", LineFive = "Assistant Coach", LineSix = "/listimg/ruifaria.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Kurt Zouma", LineTwo = "Defender", LineThree = "/listimg/5.png", LineFour = "Christophe Lollichon", LineFive = "Goalkeeper Coach", LineSix = "/listimg/christophe.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Ramires", LineTwo = "Midfielder", LineThree = "/listimg/7.png", LineFour = "Michael Emenalo", LineFive = "Technical director", LineSix = "/listimg/michael.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Oscar", LineTwo = "Midfielder", LineThree = "/listimg/11.png", LineFour = "Chris Jones", LineFive = "Fitness coach", LineSix = "/listimg/chris.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Eden Hazard", LineTwo = "Midfielder", LineThree = "/listimg/10.png", LineFour = "Carlos Lalin", LineFive = "Assistant Fitness coach", LineSix = "/listimg/carlos.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "John Mikel Obi", LineTwo = "Midfielder", LineThree = "/listimg/12.png", LineFour = "Paco Biosca", LineFive = "Medical director", LineSix = "/listimg/paco.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Andre Schurrle", LineTwo = "Midfielder", LineThree = "/listimg/14.png", LineFour = "Neil Bath", LineFive = "Head of youth development", LineSix = "/listimg/Neil.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Didier Drogba", LineTwo = "Striker", LineThree = "/listimg/15.png", LineFour = "Mick McGiven", LineFive = "Senior opposition scout", LineSix = "/listimg/mick.png" });
            this.Items.Add(new ItemViewModel() { LineOne = "Marco van Ginkel", LineTwo = "Midfielder", LineThree = "/listimg/16.png", LineFour = "James Melbourne", LineFive = "Head of match analysis/scout", LineSix = "/listimg/james.png" });
          
            
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