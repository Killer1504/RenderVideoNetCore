using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RenderVideo.ViewModels
{
    public class VideoSettingViewModel
    {
        #region Model Property
        public Models.VideoSettingModel VideoSettingModel { get; set; }
        #endregion
        #region Command Property
        public MyICommandParamter UpdateVideoSettingCommand { get; set; }
        #endregion

        public VideoSettingViewModel()
        {
            UpdateVideoSettingCommand = new MyICommandParamter(OnUpdateSettingCommand);
            _ = Init_Model();
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
        public async Task Init_Model()
        {

            Models.VideoSettingModel videoSettingModel = await API.VideoSettingAPI.LoadSettingAsync();
            VideoSettingModel = videoSettingModel.ID != -1 ? videoSettingModel : new Models.VideoSettingModel();

        }
    }
}
