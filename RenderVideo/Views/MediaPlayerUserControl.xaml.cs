using System.Windows;
using System.Windows.Controls;

namespace RenderVideo.Views
{
    /// <summary>
    /// Interaction logic for MediaPlayerUserControl.xaml
    /// </summary>
    public partial class MediaPlayerUserControl : UserControl
    {
        public string UriPath
        {
            get => (string)GetValue(UriPathProperty);
            set => SetValue(UriPathProperty, value);
        }

        // Using a DependencyProperty as the backing store for UriPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UriPathProperty =
            DependencyProperty.Register("UriPath", typeof(string), typeof(MediaPlayerUserControl), new PropertyMetadata(""));

        public MediaPlayerUserControl()
        {
            InitializeComponent();
        }
    }
}