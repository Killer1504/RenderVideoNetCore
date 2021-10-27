using System.ComponentModel;

namespace RenderVideo.Models
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowModel()
        {
            _title = "Render Video";
        }
    }
}