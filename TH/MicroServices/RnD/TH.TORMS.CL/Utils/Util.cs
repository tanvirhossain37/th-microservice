using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.TORMS.CL
{
    public static class Util
    {
        public static int GetRandomTime(int min, int max)
        {
            Random rnd = new Random();

            return rnd.Next(min, max);
        }

        public static string CreateDirectories(string path)
        {
            return Directory.CreateDirectory(path).FullName;
        }

        public static void Write(string path, string content)
        {
            using (var writer = new StreamWriter(path))
            {
                writer.Write(content);
            }
        }
    }
}