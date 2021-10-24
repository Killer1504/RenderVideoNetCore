using System;
using System.Collections.Generic;
using System.Text;

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
