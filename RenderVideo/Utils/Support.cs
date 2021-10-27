using System.IO;

namespace RenderVideo.Utils
{
    public static class Support
    {
        public static bool IsExists(this string filePath)
        {
            return File.Exists(filePath);
        }
    }
}