using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;

namespace RenderVideo.Models
{
    public class StatusModel : INotifyPropertyChanged
    {
        private string _status;
        private bool _isEnable;
        private int _fontSize;
        private int _totalTime;
        private Brush _foreGroundColor;
        private int _executionTime;
        private string _filePath;
        private int _percent;


        public int Percent
        {
            get => _percent;
            set
            {
                if (_percent != value)
                {
                    _percent = value;
                    RaisePropertyChanged(nameof(Percent));
                }
            }
        }

        public string FilePath
        {
            get =>_filePath;
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    RaisePropertyChanged(nameof(FilePath));
                }
            }
        }

        public int ExecutionTime
        {
            get => _executionTime;
            set
            {
                if (_executionTime != value)
                {
                    _executionTime = value;
                    RaisePropertyChanged(nameof(ExecutionTime));
                }
            }
        }

        public Brush ForeGroundColor
        {
            get => _foreGroundColor;
            set
            {
                if (_foreGroundColor != value)
                {
                    _foreGroundColor = value;
                    RaisePropertyChanged(nameof(ForeGroundColor));
                }
            }
        }

        public int TotalTime
        {
            get => _totalTime;
            set
            {
                if (_totalTime != value)
                {
                    _totalTime = value;
                    RaisePropertyChanged(nameof(TotalTime));
                }
            }
        }

        public int FontSize
        {
            get => _fontSize;
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    RaisePropertyChanged(nameof(FontSize));
                }
            }
        }

        public bool IsEnable
        {
            get => _isEnable;
            set
            {
                if (_isEnable != value)
                {
                    _isEnable = value;
                    RaisePropertyChanged(nameof(IsEnable));
                }
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    RaisePropertyChanged(nameof(Status));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public StatusModel()
        {
            _isEnable = true;
            _foreGroundColor = Brushes.Green;
            _executionTime = 0;
            _fontSize = 14;
            _status = "......";
            _percent = 0;
        }
    }
}
