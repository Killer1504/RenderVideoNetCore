using System.Collections.Generic;
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

        #endregion Model

        #region Command

        public MyICommandParamter ListViewSeletedCommand { get; set; }
        public MyICommandParamter LoadedCommand { get; set; }
        public MyICommandParamter ClosingCommand { get; set; }

        #endregion Command

        #region UserControl

        public Views.CommonSettingUserControl CommonSettingUserControl { get; set; }
        public Views.VideoUserControl VideoUserControl { get; set; }

        public List<UserControl> userControls = new List<UserControl>();

        #endregion UserControl

        public MainWindowViewModel()
        {

            Init_Command();
            Init_Model();
           
        }

        private void Init_Model()
        {
            MainWindowModel = new Models.MainWindowModel();
            StatusModel = new Models.StatusModel();
        }

        private void Init_Command()
        {
            ListViewSeletedCommand = new MyICommandParamter(OnListViewSelectedCommand);
            LoadedCommand = new MyICommandParamter(OnLoadedCommand);
            ClosingCommand = new MyICommandParamter(OnClosingCommand);
        }
       
        private void OnClosingCommand(object obj)
        {
            var e = obj as System.ComponentModel.CancelEventArgs;
            var result = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                e.Cancel = true;
                return;
            }
            else
            {
                e.Cancel = false;
            }
        }

        private async void OnLoadedCommand(object obj)
        {
            Grid _item = obj as Grid;
            await Init_UserControl(_item);
        }

        private async Task Init_UserControl(Grid _item)
        {
            await Task.Delay(1);

            VideoUserControl = new Views.VideoUserControl() { Visibility = Visibility.Visible , Tag = nameof(VideoUserControl) };
            CommonSettingUserControl = new Views.CommonSettingUserControl() { Visibility = Visibility.Collapsed, Tag = nameof(CommonSettingUserControl) };

            userControls.Add(VideoUserControl);
            userControls.Add(CommonSettingUserControl);

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

            foreach (UserControl item in userControls)
            {
                string tagUserControl = item.Tag.ToString();
                if (tagUserControl.ToLower() == _tag.ToLower())
                {
                    item.Visibility = visible;
                    continue;
                }
                item.Visibility = collapsed;
            }
        }
    }
}