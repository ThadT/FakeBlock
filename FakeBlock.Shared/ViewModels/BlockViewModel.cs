using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace FakeBlock.ViewModels
{
    class BlockViewModel : INotifyPropertyChanged
    {
        private WoodGrain selectedGrain;
        private Mallet selectedMallet;
        private Windows.Storage.StorageFile soundFileBlock;
        private Windows.Storage.StorageFile soundFileMallet;
        private System.Collections.ObjectModel.ObservableCollection<WoodGrain> woods;
        private System.Collections.ObjectModel.ObservableCollection<Mallet> mallets;

        public BlockViewModel()
        {
            this.GetMallets();
            this.GetWood();

        }

        private void GetWood()
        {
            var woodCollection = new System.Collections.ObjectModel.ObservableCollection<WoodGrain>();
            var w = new WoodGrain() { Name = "Bamboo", ImageFilename = "Assets/Wood/bamboo.jpg", SoundFilename = "ms-appx:///Assets/Sounds/woodblock1.wav" };
            var w7 = new WoodGrain() { Name = "Birdseye Maple", ImageFilename = "Assets/Wood/birdseye-maple.jpg", SoundFilename = "ms-appx:///Assets/Sounds/woodblock1.wav" };
            var w1 = new WoodGrain() { Name = "Blood wood", ImageFilename = "Assets/Wood/bloodwood.jpg", SoundFilename = "ms-appx:///Assets/Sounds/woodblock2.wav" };
            var w2 = new WoodGrain() { Name = "Canary wood", ImageFilename = "Assets/Wood/canarywood.jpg", SoundFilename = "ms-appx:///Assets/Sounds/woodblock3.wav" };
            var w3 = new WoodGrain() { Name = "Macassar Ebony", ImageFilename = "Assets/Wood/macassar-ebony.jpg", SoundFilename = "ms-appx:///Assets/Sounds/woodblock4.wav" };
            var w9 = new WoodGrain() { Name = "Malaysian blackwood", ImageFilename = "Assets/Wood/malaysian-blackwood.jpg", SoundFilename = "ms-appx:///Assets/Sounds/woodblock3.wav" };
            var w4 = new WoodGrain() { Name = "Marble wood", ImageFilename = "Assets/Wood/marblewood.jpg", SoundFilename = "ms-appx:///Assets/Sounds/woodblock5.wav" };
            var w5 = new WoodGrain() { Name = "Shagbark Hickory", ImageFilename = "Assets/Wood/shagbark-hickory.jpg", SoundFilename = "ms-appx:///Assets/Sounds/woodblock6.wav" };
            var w8 = new WoodGrain() { Name = "Red Oak", ImageFilename = "Assets/Wood/red-oak.jpg", SoundFilename = "ms-appx:///Assets/Sounds/woodblock5.wav" };
            var w6 = new WoodGrain() { Name = "Zebra wood", ImageFilename = "Assets/Wood/zebrawood.jpg", SoundFilename = "ms-appx:///Assets/Sounds/woodblock1.wav" };
            woodCollection.Add(w);
            woodCollection.Add(w7);
            woodCollection.Add(w1);
            woodCollection.Add(w2);
            woodCollection.Add(w3);
            woodCollection.Add(w9);
            woodCollection.Add(w4);
            woodCollection.Add(w8);
            woodCollection.Add(w5);
            woodCollection.Add(w6);

            Woods = woodCollection;
        }

        private void GetMallets()
        {
            var malletCollection = new System.Collections.ObjectModel.ObservableCollection<Mallet>();
            var m = new Mallet() { Name = "Mallet", ImageFilename = "Assets/Mallet/mallet.png", SoundFilename = "" };
            var m1= new Mallet() { Name = "Bat", ImageFilename = "Assets/Mallet/bat.png", SoundFilename = "" };            
            var m2 = new Mallet() { Name = "Hammer", ImageFilename = "Assets/Mallet/hammer.png", SoundFilename = "" };
            var m3 = new Mallet() { Name = "Frozen Banana", ImageFilename = "Assets/Mallet/banana.png", SoundFilename = "" };
            var m4 = new Mallet() { Name = "Bottle", ImageFilename = "Assets/Mallet/bottle.png", SoundFilename = "ms-appx:///Assets/Sounds/break-glass2.wav" };
            var m5 = new Mallet() { Name = "Stick", ImageFilename = "Assets/Mallet/stick.png", SoundFilename = "" };
            var m6 = new Mallet() { Name = "Sock", ImageFilename = "Assets/Mallet/sock.png", SoundFilename = "ms-appx:///Assets/Sounds/whip.wav" };
            var m7 = new Mallet() { Name = "Buster's Hook", ImageFilename = "Assets/Mallet/hook.png", SoundFilename = "" };

            malletCollection.Add(m);
            //malletCollection.Add(m1);
            malletCollection.Add(m2);
            malletCollection.Add(m3);
            malletCollection.Add(m4);
            malletCollection.Add(m5);
            malletCollection.Add(m6);
            malletCollection.Add(m7);
            Mallets = malletCollection;
        }

        public System.Collections.ObjectModel.ObservableCollection<WoodGrain> Woods
        {
            get { return this.woods; }
            set 
            { 
                this.woods = value;
                NotifyPropertyChanged();
            }
        }

        public System.Collections.ObjectModel.ObservableCollection<Mallet> Mallets
        {
            get { return this.mallets; }
            set
            {
                this.mallets = value;
                NotifyPropertyChanged();
            }
        }

        public Windows.Storage.StorageFile BlockSoundFile
        {
            get { return this.soundFileBlock; }
        }

        public Windows.Storage.StorageFile MalletSoundFile
        {
            get { return this.soundFileMallet; }
        }

        private async Task OpenBlockSoundFile()
        {
            if (string.IsNullOrEmpty(this.selectedGrain.SoundFilename)) { return; }

            this.soundFileBlock = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri(this.selectedGrain.SoundFilename)); // "ms-appx:///Assets/Sounds/woodblock1.wav"
        }

        private async Task OpenMalletSoundFile()
        {
            if (string.IsNullOrEmpty(this.selectedMallet.SoundFilename)) { return; }

            this.soundFileMallet = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri(this.selectedMallet.SoundFilename)); // "ms-appx:///Assets/Sounds/woodblock1.wav"
        }

        public WoodGrain SelectedGrain
        {
            get { return this.selectedGrain; }
            set
            {
                if (this.selectedGrain != value)
                {
                    this.selectedGrain = value;
                    this.OpenBlockSoundFile();
                    NotifyPropertyChanged();
                }
            }
        }

        public Mallet SelectedMallet
        {
            get { return this.selectedMallet; }
            set
            {
                if (this.selectedMallet != value)
                {
                    this.selectedMallet = value;
                    this.OpenMalletSoundFile();
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName]string property = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    class WoodGrain
    {
        public string Name { get; set; }
        public string ImageFilename { get; set; }
        public string SoundFilename { get; set; }
    }

    class Mallet
    {
        public string Name { get; set; }
        public string ImageFilename { get; set; }
        public string SoundFilename { get; set; }
    }
}
