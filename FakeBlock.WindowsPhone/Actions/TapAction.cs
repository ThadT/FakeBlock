using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace FakeBlock.Actions
{
    class TapAction : DependencyObject, IAction
    {
        #region SoundFile Dependency Property
        /// <summary> 
        /// Get or Sets the SoundFile dependency property.  
        /// </summary> 
        public string SoundFile
        {
            get { return (string)GetValue(SoundFileProperty); }
            set { SetValue(SoundFileProperty, value); }
        }

        /// <summary> 
        /// Identifies the SoundFile dependency property. This enables animation, styling, 
        /// binding, etc...
        /// </summary> 
        public static readonly DependencyProperty SoundFileProperty =
            DependencyProperty.Register("SoundFile",
                                        typeof(string),
                                        typeof(TapAction),
                                        new PropertyMetadata(string.Empty));

        #endregion Text Dependency Property

        public object Execute(object sender, object parameter)
        {
            Utils.SoundUtilities.PlaySound(this.SoundFile);

            return null;
        }
    }
}
