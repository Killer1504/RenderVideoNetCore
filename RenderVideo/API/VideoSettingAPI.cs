using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderVideo.API
{
    public class VideoSettingAPI
    {
        public static async Task<Models.VideoSettingModel> LoadSettingAsync(int _id = 1)
        {
            Models.VideoSettingModel videoSettingModel = new Models.VideoSettingModel() { ID = -1 };
            string conStr = API.ConnectionStringAPI.GetConnectionString();
            using (SQLiteConnection cnn = new SQLiteConnection(conStr, true))
            {
                string query = $"select * from VideoSetting where ID = {_id}";
                IEnumerable<Models.VideoSettingModel> output = await cnn.QueryAsync<Models.VideoSettingModel>(query, new DynamicParameters());
                if (output != null)
                {
                    videoSettingModel = output.ToList()[0];
                    return videoSettingModel;
                }
            }
            return videoSettingModel;
        }

        public static async Task<bool> UpdateSettingAsync(Models.VideoSettingModel videoSettingModel)
        {
            string conStr = API.ConnectionStringAPI.GetConnectionString();
            using SQLiteConnection cnn = new SQLiteConnection(conStr, true);
            string query = $"update VideoSetting set VideoEncoder = \"{videoSettingModel.VideoEncoder}\", VideoBitrate = \"{videoSettingModel.VideoBitrate}\"," +
                $" Resolution = \"{videoSettingModel.Resolution}\", FrameRate = \"{videoSettingModel.FrameRate}\", " +
                $" AudioEncoder = \"{videoSettingModel.AudioEncoder}\", AudioBitrate = \"{videoSettingModel.AudioBitrate}\", " +
                $" AudioChanel = \"{videoSettingModel.AudioChanel}\", AudioSampleRate = \"{videoSettingModel.AudioSampleRate}\" " +
                $"where ID = {videoSettingModel.ID}";
            int result = await cnn.ExecuteAsync(query);
            return result == 1;
        }
    }
}
