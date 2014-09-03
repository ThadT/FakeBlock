using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FakeBlock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
           
            // pass the UI dispatcher to the view model (required for updating the ad image property from the timer)
            var vm = this.DataContext as FakeBlock.ViewModels.BlockViewModel;
            var dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;
            vm.UIDispatcher = dispatcher; 
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void LayoutRoot_Tapped(object sender, TappedRoutedEventArgs e)
        {
          
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {

          //  this.mediaElementBlock.Source = null;
          //  this.mediaElementMallet.Source = null;
            foreach (var elem in this.LayoutRoot.Children)
            {
                var me = elem as MediaElement;
                if (me != null)
                {
                    me.Source = null;
                    me.MediaEnded -= me_MediaEnded;
                    me = null;                    
                }
            }
            var rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof (SettingsPage));
        }

        private async void LayoutRoot_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                var me = new MediaElement();
                var ctx = this.DataContext as FakeBlock.ViewModels.BlockViewModel;
                var rnd = new Random();
                var r = rnd.Next(0, 40);
                var playMallet = ((!string.IsNullOrEmpty(ctx.SelectedMallet.SoundFilename)) && r > 20) || ctx.SelectedMallet.Name == "Sock";
                if (playMallet)
                {
                    var malletFile = ctx.MalletSoundFile;
                    var malletStream = await malletFile.OpenAsync(FileAccessMode.Read);
                    this.LayoutRoot.Children.Add(me);
                    me.SetSource(malletStream, string.Empty);
                    me.MediaEnded += me_MediaEnded;
                   // mediaElementMallet.SetSource(malletStream, "");
                }
                else
                {
                    var blockFile = ctx.BlockSoundFile;
                    var blockStream = await blockFile.OpenAsync(FileAccessMode.Read);
                    this.LayoutRoot.Children.Add(me);
                    me.SetSource(blockStream, string.Empty);
                    me.MediaEnded += me_MediaEnded;
                    //mediaElementBlock.SetSource(blockStream, "");
                }
            }
            catch (Exception exp)
            {
                var s = exp.Message;
            }
        }

        void me_MediaEnded(object sender, RoutedEventArgs e)
        {
            var me = sender as MediaElement;
            if (me == null) {return;}

            me.MediaEnded -= me_MediaEnded;
            this.LayoutRoot.Children.Remove(me);
        }

    }
}
