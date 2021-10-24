using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;
using RenderVideo.Utils;
using System.Windows.Media;
using System.Windows;
using System.IO;
using System.Diagnostics;

namespace RenderVideo.ViewModels
{
    public class VideoViewModel
    {
        #region Model Property
        public Models.StatusModel StatusModel { get; set; }
        public Models.VideoModel VideoModel { get; set; }
        #endregion

        #region Command Property
        public MyICommandParamter SelectFileCommand { get; set; }
        public MyICommandParamter CreateVideoCommand { get; set; }
        public MyICommandParamter OpenFileCommand { get; set; }
        
        #endregion
        public VideoViewModel()
        {
            StatusModel = new Models.StatusModel();
            VideoModel = new Models.VideoModel();

            SelectFileCommand = new MyICommandParamter(OnSelectFileCommand);
            CreateVideoCommand = new MyICommandParamter(OnCreateVideoCommand);
            OpenFileCommand = new MyICommandParamter(OnOpenFileCommand);
        }

        private void OnOpenFileCommand(object obj)
        {
            if (File.Exists(StatusModel.FilePath))
            {
                _ = Process.Start(new ProcessStartInfo(StatusModel.FilePath) { UseShellExecute = true });
            }
        }

        private async void OnCreateVideoCommand(object obj)
        {

            if (StatusModel.IsEnable == false)
            {
                return;
            }
            StatusModel.IsEnable = false;
            StatusModel.Percent = 0;
            StatusModel.FilePath = "";

            string _output = await CreateVideo();

            StatusModel.FilePath = _output;
            StatusModel.IsEnable = true;

        }

        private async Task<string> CreateVideo(string _extension = ".mp4")
        {
            await Task.Delay(1);

            string _inputImage = VideoModel.InputImagePath;
            string _inputAudio = VideoModel.InputAudioPath;

            //check exists
            if (!_inputAudio.IsExists() || !_inputImage.IsExists())
            {
                SetStatusText("File is not exists!", Brushes.Red);
                return "";
            }

            string _dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Render_Video");
            if (!Directory.Exists(_dir))
            {
                _ = Directory.CreateDirectory(_dir);
            }

            //get argurment
            string _output1 = Path.Combine(_dir, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + _extension);
            Models.VideoSettingModel videoSetting = await API.VideoSettingAPI.LoadSettingAsync();
            string _parameter = videoSetting.ToParameter();
            string argument = $" -loop 1 -i \"{_inputImage}\" -i \"{_inputAudio}\" -filter_complex \"[0:v]scale={videoSetting.Resolution}\" -shortest " +
                $" {_parameter}   \"{_output1}\""; ;

            IConversion convertsion1 = FFmpeg.Conversions.New();
            convertsion1.OnProgress += (_, args) =>
            {
                //StatusModel.Percent = args.Percent;
                StatusModel.Percent = args.Percent;

            };
            try
            {
                SetStatusText("Create video ...", Brushes.Green);
                _ = await convertsion1.Start(argument);
                SetStatusText("Finish!", Brushes.Green);
            }
            catch (Exception)
            {
                string _cantCreateVideo = Application.Current.Resources["CannotCreateVideo"].ToString();
                SetStatusText(_cantCreateVideo, Brushes.Red);
                _output1 = "";
            }

            //loop Video
            IMediaInfo mediaInfo = await FFmpeg.GetMediaInfo(_output1);
            double _duration = mediaInfo.Duration.TotalSeconds;
            VideoModel.NumberLoop = (int)(3600 / _duration) + 1;
            string _output2 = Path.Combine(_dir, DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + _extension);
            string _argurment2 = $" -stream_loop {VideoModel.NumberLoop} -i \"{_output1}\"  -c copy \"{_output2}\" ";
            try
            {
                SetStatusText("Loop Video ...", Brushes.Green);
                _ = await convertsion1.Start(_argurment2);
                SetStatusText("Finish!", Brushes.Green);
            }
            catch (Exception)
            {
                string _cantCreateVideo = Application.Current.Resources["CannotCreateVideo"].ToString();
                SetStatusText(_cantCreateVideo, Brushes.Red);
                _output2 = "";
            }

            return _output2;

        }

        private void SetStatusText(string _status, Brush brush)
        {
            StatusModel.Status = _status;
            StatusModel.ForeGroundColor = brush;
            if (brush == Brushes.Red)
            {
                _ = MessageBox.Show(_status, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnSelectFileCommand(object obj)
        {
            string _tag = obj.ToString();

            if (_tag == nameof(VideoModel.InputAudioPath)) // select audio
            {
                string _title = "Select Audio";
                string _filter = "Audio File | *.mp3; *.wav";
                VideoModel.InputAudioPath = SelectFile(_filter, _title);
            }
            if (_tag == nameof(VideoModel.InputImagePath)) // select image
            {
                string _title = "Select Image";
                string _filter = "Image File | *.jpg; *.png";
                VideoModel.InputImagePath = SelectFile(_filter, _title);
            }
        }

        private string SelectFile(string _filter, string _title)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                RestoreDirectory = true,
                Filter = _filter,
                Title = _title,
            };
            return fileDialog.ShowDialog() == true ? fileDialog.FileName : "";
        }
    }
}
