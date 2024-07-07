using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TH.Tommy
{
    public static class FileManager
    {
        public static string CreateDirectories(string path)
        {
            return Directory.CreateDirectory(path).FullName;
        }

        public static void CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs = true)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    CopyDirectory(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public static FileInfo[] GetFileInfos(string dirPath, string fileName = "*.*", SearchOption searchOption = SearchOption.AllDirectories)
        {
            try
            {
                dirPath = string.IsNullOrWhiteSpace(dirPath) ? throw new ArgumentNullException(nameof(dirPath)) : dirPath.Trim();
                fileName = string.IsNullOrWhiteSpace(fileName) ? throw new ArgumentNullException(nameof(fileName)) : fileName.Trim();

                var dir = new DirectoryInfo(dirPath);
                return dir.GetFiles(fileName, searchOption);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void CopyFile(string src, string dest, bool overwrite = true)
        {
            try
            {
                if (src == null) throw new ArgumentNullException(nameof(src));
                if (dest == null) throw new ArgumentNullException(nameof(dest));

                File.Copy(src, dest, overwrite);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string[] ReadAllLines(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return File.ReadAllLines(path);
            }
        }

        public static void WriteAllLines(string path, string[] lines)
        {
            try
            {
                File.WriteAllLines(path, lines);
            }
            catch (Exception e)
            {
                ;
            }
        }

        public static string Read(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

        public static void Write(string path, string content)
        {
            using (var writer = new StreamWriter(path))
            {
                writer.Write(content);
            }
        }

        public static void Append(string path, string content)
        {
            if (!File.Exists(path))
            {
                using (var sw = File.CreateText(path))
                {
                    sw.WriteLine(content);
                }
            }
            else
            {
                using (var sw = File.AppendText(path))
                {
                    sw.WriteLine(content);
                }
            }
        }

        // Convert the string to camel case.
        public static string ToCamelCase(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                name = char.ToLower(name[0]) + name.Substring(1);
            }

            return name;
        }

        // Convert the string to camel case.
        public static string ToPascalCase(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                name = ti.ToTitleCase(name);
            }

            return name;
        }

        public static string ToComponentName(string name)
        {
            var className = string.Empty;
            //split
            var splitNames = name.Split('-');
            foreach (var splitName in splitNames)
            {
                if (!string.IsNullOrWhiteSpace(splitName))
                {
                    className = string.Concat(className, ToPascalCase(splitName));
                }
            }

            return className;
        }

        public static string ToHyphenCase(string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                    return string.Empty;

                value = ToCamelCase(value.Trim());

                string[] words = Regex.Matches(value, "(^[a-z]+|[A-Z]+(?![a-z])|[A-Z][a-z]+)")
                    .OfType<Match>()
                    .Select(m => m.Value)
                    .ToArray();

                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = FileManager.ToCamelCase(words[i]);
                }

                return string.Join("-", words);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}