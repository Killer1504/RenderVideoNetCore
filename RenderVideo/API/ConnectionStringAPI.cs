namespace RenderVideo.API
{
    public class ConnectionStringAPI
    {
        public static string GetConnectionString()
        {
            return @"Data Source=.\mydata.db;Version=3;";
        }
    }
}