using System.ComponentModel;
using System.Windows;

namespace RenderVideo.ViewModels
{
    public class VideoSettingViewModel : INotifyPropertyChanged
    {
        #region Model Property

        private Models.VideoSettingModel _videoSettingModel;

        public Models.VideoSettingModel VideoSettingModel
        {
            get => _videoSettingModel;
            set
            {
                if (_videoSettingModel != value)
                {
                    _videoSettingModel = value;
                    RaisePropertyChanged(nameof(VideoSettingModel));
                }
            }
        }

        #endregion Model Property

        #region Command Property

        public MyICommandParamter UpdateVideoSettingCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string _propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }

        #endregion Command Property

        public VideoSettingViewModel()
        {
            Init_Command();
            Init_Model();
        }

        private void Init_Command()
        {
            UpdateVideoSettingCommand = new MyICommandParamter(OnUpdateSettingCommand);
        }

        private void Init_Model()
        {
            VideoSettingModel = new Models.VideoSettingModel();
        }

        public async void OnLoadData()
        {
            Models.VideoSettingModel videoSettingModel = await API.VideoSettingAPI.LoadSettingAsync();
            VideoSettingModel = videoSettingModel.ID != -1 ? videoSettingModel : new Models.VideoSettingModel();
        }

        private async void OnUpdateSettingCommand(object obj)
        {
            bool result = await API.VideoSettingAPI.UpdateSettingAsync(VideoSettingModel);
            string setting_lan = Application.Current.Resources["Setting"].ToString();
            if (result)
            {
                string dataupdate_lan = Application.Current.Resources["DataSettingUpdate"].ToString();
                _ = MessageBox.Show(dataupdate_lan + "!", setting_lan, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string datacannotupdate_lan = Application.Current.Resources["DataSettingCannotUpdate"].ToString();
                _ = MessageBox.Show(datacannotupdate_lan + "!", setting_lan, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}