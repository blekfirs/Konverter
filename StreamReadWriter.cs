using System.IO;
using System.Text;

namespace ConsoleSharpProb
{
    //Класс для записи/чтения просто текста в/из файла
    public static class StreamReadWriter
    {
        public static string Read(FileStream fs)
        {
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer);
        }

        public static void Write(FileStream fs, string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            fs.Write(buffer, 0, buffer.Length);
        }
    }
}
