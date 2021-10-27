using System.ComponentModel;

namespace RenderVideo.Models
{
    public class MediaPlayerModel : INotifyPropertyChanged
    {
        private string _uri;
        private int _volume;
        private bool _isplayed;
        private int _duration;
        private int _position;
        private string _uriImage;
        private string _timeDisPlay;

        public string TimeDisPlay
        {
            get => _timeDisPlay;
            set
            {
                if (_timeDisPlay != value)
                {
                    _timeDisPlay = value;
                    RaisePropertyChanged(nameof(TimeDisPlay));
                }
            }
        }

        public string UriImage
        {
            get => _uriImage;
            set
            {
                if (_uriImage != value)
                {
                    _uriImage = value;
                    RaisePropertyChanged(nameof(UriImage));
                }
            }
        }

        public int Position
        {
            get => _position;
            set
            {
                if (_position != value)
                {
                    _position = value;
                    RaisePropertyChanged(nameof(Position));
                }
            }
        }

        public int Duration
        {
            get => _duration;
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    RaisePropertyChanged(nameof(Duration));
                }
            }
        }

        public bool IsPlayed
        {
            get => _isplayed;
            set
            {
                if (_isplayed != value)
                {
                    _isplayed = value;
                    RaisePropertyChanged(nameof(IsPlayed));
                }
            }
        }

        public int Volume
        {
            get => _volume;
            set
            {
                if (_volume != value)
                {
                    _volume = value;
                    RaisePropertyChanged(nameof(Volume));
                }
            }
        }

        public string Uri
        {
            get => _uri;
            set
            {
                if (_uri != value)
                {
                    _uri = value;
                    RaisePropertyChanged(nameof(Uri));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MediaPlayerModel()
        {
            _duration = 10;
            _position = 0;
            _uri = "";
            _timeDisPlay = "00:00";
        }
    }
}