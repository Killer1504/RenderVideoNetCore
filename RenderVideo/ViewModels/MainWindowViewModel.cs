using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RenderVideo.ViewModels
{
    public class MainWindowViewModel
    {
        #region Model
        public Models.MainWindowModel MainWindowModel { get; set; }
        public Models.StatusModel StatusModel { get; set; }
        #endregion

        #region Command 
        public MyICommandParamter ListViewSeletedCommand { get; set; }
        public MyICommandParamter LoadedCommand { get; set; }
        #endregion

        #region UserControl
        public Views.CommonSettingUserControl CommonSettingUserControl { get; set; }
        public Views.VideoUserControl VideoUserControl { get; set; }
        #endregion
        public MainWindowViewModel()
        {
            ListViewSeletedCommand = new MyICommandParamter(OnListViewSelectedCommand);
            LoadedCommand = new MyICommandParamter(OnLoadedCommand);

            MainWindowModel = new Models.MainWindowModel();
            StatusModel = new Models.StatusModel();


        }

        private async void OnLoadedCommand(object obj)
        {
            Grid _item = obj as Grid;
            await Init_UserControl(_item);
        }

        private async Task Init_UserControl(Grid _item)
        {
            await Task.Delay(1);

            VideoUserControl = new Views.VideoUserControl() { Visibility = Visibility.Visible };
            CommonSettingUserControl = new Views.CommonSettingUserControl() { Visibility = Visibility.Collapsed };

            _ = _item.Children.Add(VideoUserControl);
            _ = _item.Children.Add(CommonSettingUserControl);

        }

        private void OnListViewSelectedCommand(object obj)
        {
            ListBoxItem _item = obj as ListBoxItem;
            string _tag = _item.Tag.ToString();
            VisibleUserControl(_tag);
        }

        private void VisibleUserControl(string _tag)
        {
            Visibility collapsed = Visibility.Collapsed;
            Visibility visible = Visibility.Visible;

            CommonSettingUserControl.Visibility = collapsed;
            VideoUserControl.Visibility = collapsed;

            switch (_tag)
            {
                case "CommonSettingUserControl":
                    {
                        CommonSettingUserControl.Visibility = visible;
                        break;
                    }
                case "VideoUserControl":
                    {
                        VideoUserControl.Visibility = visible;
                        break;
                    }

                default:
                    break;
            }
        }
    }
}
