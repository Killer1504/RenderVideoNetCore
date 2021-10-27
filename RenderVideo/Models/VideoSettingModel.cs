using System.ComponentModel;

namespace RenderVideo.Models
{
    public class VideoSettingModel : INotifyPropertyChanged
    {
        private string _logoPath;

        private int _videoBitrate;
        private int _frameRate;
        private string _resolution;
        private string _videoEncoder;

        private int _audioChanel;
        private int _audioSampleRate;
        private int _audioBitrate;
        private string _audioEncoder;
        private int _id;

        public int ID
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged(nameof(ID));
                }
            }
        }

        public string AudioEncoder
        {
            get => _audioEncoder;
            set
            {
                if (_audioEncoder != value)
                {
                    _audioEncoder = value;
                    RaisePropertyChanged(nameof(AudioEncoder));
                }
            }
        }

        public string LogoPath
        {
            get => _logoPath;
            set
            {
                if (_logoPath != value)
                {
                    _logoPath = value;
                    RaisePropertyChanged(nameof(LogoPath));
                }
            }
        }

        public string VideoEncoder
        {
            get => _videoEncoder;
            set
            {
                if (_videoEncoder != value)
                {
                    _videoEncoder = value;
                    RaisePropertyChanged(nameof(VideoEncoder));
                }
            }
        }

        public string Resolution
        {
            get => _resolution;
            set
            {
                if (_resolution != value)
                {
                    _resolution = value;
                    RaisePropertyChanged(nameof(Resolution));
                }
            }
        }

        public int AudioBitrate
        {
            get => _audioBitrate;
            set
            {
                _audioBitrate = value;
                RaisePropertyChanged(nameof(AudioSampleRate));
            }
        }

        public int AudioSampleRate
        {
            get => _audioSampleRate;
            set
            {
                _audioSampleRate = value;
                RaisePropertyChanged(nameof(AudioSampleRate));
            }
        }

        public int AudioChanel
        {
            get => _audioChanel;
            set
            {
                if (_audioChanel != value)
                {
                    _audioChanel = value;
                    RaisePropertyChanged(nameof(AudioChanel));
                }
            }
        }

        public int FrameRate
        {
            get => _frameRate;
            set
            {
                if (_frameRate != value)
                {
                    _frameRate = value;
                    RaisePropertyChanged(nameof(FrameRate));
                }
            }
        }

        public int VideoBitrate
        {
            get => _videoBitrate;
            set
            {
                if (_videoBitrate != value)
                {
                    _videoBitrate = value;
                    RaisePropertyChanged(nameof(VideoBitrate));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string ToParameter()
        {
            string parameter = $"-c:a {AudioEncoder} -ac {AudioChanel} -ar {AudioSampleRate} -b:a {AudioBitrate}k -c:v {VideoEncoder} " +
                $" -r {FrameRate} -b:v {VideoBitrate}k ";
            return parameter;
        }

        public VideoSettingModel()
        {
            _videoEncoder = "h264";
            _resolution = "1280x720";
            _videoBitrate = 2500;
            _frameRate = 25;

            _audioEncoder = "aac";
            _audioBitrate = 128;
            _audioSampleRate = 44100;
            _audioChanel = 2;
        }
    }
}