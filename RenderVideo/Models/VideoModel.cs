using System.ComponentModel;
using System.IO;

namespace RenderVideo.Models
{
    public class VideoModel : INotifyPropertyChanged
    {
        private int _id;
        private string _title;

        private string _inputAudioPath;

        private string _outputVideoPath;
        private string _inputImagePath;
        private int _numberLoop;

        private int _zIndex;

        public int ZIndex
        {
            get => _zIndex;
            set
            {
                if (_zIndex != value)
                {
                    _zIndex = value;
                    RaisePropertyChanged(nameof(ZIndex));
                }
            }
        }


        public int NumberLoop
        {
            get => _numberLoop;
            set
            {
                if (_numberLoop != value)
                {
                    _numberLoop = value;
                    RaisePropertyChanged(nameof(NumberLoop));
                }
            }
        }

        public string InputImagePath
        {
            get => _inputImagePath;
            set
            {
                if (_inputImagePath != value)
                {
                    _inputImagePath = value;
                    RaisePropertyChanged(nameof(InputImagePath));
                    ZIndex = File.Exists(value) ? 0 : 1;
                }
            }
        }

        public string OutputVideoPath
        {
            get => _outputVideoPath;
            set
            {
                if (_outputVideoPath != value)
                {
                    _outputVideoPath = value;
                    RaisePropertyChanged(nameof(OutputVideoPath));
                }
            }
        }

        public string InputAudioPath
        {
            get => _inputAudioPath;
            set
            {
                if (_inputAudioPath != value)
                {
                    _inputAudioPath = value;
                    RaisePropertyChanged(nameof(InputAudioPath));
                }
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }

        public int ID
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged(nameof(ID));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public VideoModel()
        {
            _numberLoop = 10;
            _zIndex = 1;
        }
    }
}