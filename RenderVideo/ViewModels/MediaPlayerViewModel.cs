using RenderVideo.Utils;
using System;
using System.Windows;
using System.Windows.Threading;

namespace RenderVideo.ViewModels
{
    public class MediaPlayerViewModel
    {
        #region Model property

        public Models.MediaPlayerModel MediaPlayerModel { get; set; }
        public Models.StatusModel StatusModel { get; set; }
        public System.Windows.Controls.MediaElement MediaElement { get; set; }

        private DispatcherTimer timer = null;

        public string UriPlay => "../Images/play-button.png";
        public string UriPause => "../Images/pause-button.png";

        #endregion Model property

        #region Command property

        public MyICommandParamter PlayCommand { get; set; }
        public MyICommandParamter PauseCommand { get; set; }
        public MyICommandParamter StopCommand { get; set; }
        public MyICommandParamter ValueChanedCommand { get; set; }

        #endregion Command property

        public MediaPlayerViewModel()
        {
            StatusModel = new Models.StatusModel { Status = "Play" };
            MediaPlayerModel = new Models.MediaPlayerModel() { UriImage = UriPlay };
            PlayCommand = new MyICommandParamter(OnPlayCommand);
            StopCommand = new MyICommandParamter(OnStopCommand);
            PauseCommand = new MyICommandParamter(OnPauseCommand);
            ValueChanedCommand = new MyICommandParamter(OnValueChanedCommand);
        }

        private void OnValueChanedCommand(object obj)
        {
            if (MediaElement != null && MediaElement.Source != null)
            {
                int position = Convert.ToInt32(obj);
                MediaElement.Position = TimeSpan.FromSeconds(position);
            }
        }

        private void OnPauseCommand(object obj)
        {
        }

        private void OnStopCommand(object obj)
        {
        }

        private void OnPlayCommand(object obj)
        {
            if (obj != null)
            {
                MediaElement = obj as System.Windows.Controls.MediaElement;
                try
                {
                    MediaPlayerModel.Uri = MediaElement.Source != null ? MediaElement.Source.AbsolutePath : "";
                }
                catch (Exception)
                {
                    MediaPlayerModel.Uri = "";
                }
            }

            if (MediaPlayerModel.Uri.IsExists() && StatusModel.Status == "Play" && MediaElement != null)
            {
                SetStatus("Pause");
                MediaElement.MediaEnded += (sender, args) =>
                {
                    SetStatus("Play");
                    timer.Stop();
                    MediaElement.Close();
                    MediaElement.Position = TimeSpan.FromSeconds(0);
                    MediaPlayerModel.Position = (int)Math.Round(MediaElement.Position.TotalSeconds, 2);
                    MediaPlayerModel.TimeDisPlay = $"{MediaElement.Position.Minutes:00}:{MediaElement.Position.Seconds:00}";
                };
                timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(0.5)
                };
                timer.Tick += (_sender, _args) =>
                {
                    MediaPlayerModel.Position = (int)Math.Round(MediaElement.Position.TotalSeconds, 2);
                    MediaPlayerModel.TimeDisPlay = $"{MediaElement.Position.Minutes:00}:{MediaElement.Position.Seconds:00}";
                    SetStatus("Pause");
                };
                MediaElement.MediaOpened += (sender, args) =>
                {
                    if (MediaElement.HasAudio)
                    {
                        MediaPlayerModel.Duration = (int)Math.Round(MediaElement.NaturalDuration.TimeSpan.TotalSeconds, 2);
                    }
                    else
                    {
                        SetStatus("Play");
                        timer.Stop();
                    }
                };
                MediaElement.MediaFailed += (_, __) =>
                {
                    timer.Stop();
                    string _cannot_play_media_lan = Application.Current.Resources["Cannot_play_media"].ToString();
                    string _error_lan = Application.Current.Resources["Error"].ToString();
                    //_ = MessageBox.Show(_cannot_play_media_lan, _error_lan, MessageBoxButton.OK, MessageBoxImage.Error);
                };
                MediaElement.Play();
                timer.Start();
            }
            else if (MediaPlayerModel.Uri.IsExists() && StatusModel.Status == "Pause")
            {
                MediaElement.Pause();
                timer.Stop();
                SetStatus("Play");
            }
        }

        private void SetStatus(string _status = "Play")
        {
            switch (_status)
            {
                case "Pause":
                    {
                        StatusModel.Status = "Pause";
                        MediaPlayerModel.UriImage = UriPause;
                        break;
                    }
                default:
                    {
                        StatusModel.Status = "Play";
                        MediaPlayerModel.UriImage = UriPlay;
                        break;
                    }
            }
        }
    }
}