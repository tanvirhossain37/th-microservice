using Pluralize.NET.Core;

namespace TH.Tommy;

public static class Util
{
    public static string CreateDirectory(string path)
    {
        try
        {
            return Directory.CreateDirectory(path).FullName;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static void DeleteDirectory(string path, bool recursive = false)
    {
        try
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, recursive);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static IEnumerable<string> ReadModelPropertyLines(string path, bool ignoreCommnet = true)
    {
        try
        {
            if (File.Exists(path))
            {
                var list = new List<string>();

                using (var reader = new StreamReader(path))
                {
                    var lines = File.ReadAllLines(path);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Contains("get;"))
                        {
                            if (ignoreCommnet)
                            {
                                if (lines[i].Trim().StartsWith("//")) continue;
                            }

                            list.Add(lines[i].Trim());
                        }
                    }

                    return list;
                }
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static IEnumerable<string> ReadControllerRouteLines(string path, bool ignoreCommnet = true)
    {
        try
        {
            if (File.Exists(path))
            {
                var list = new List<string>();

                using (var reader = new StreamReader(path))
                {
                    var lines = File.ReadAllLines(path);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Contains("[Route("))
                        {
                            if (ignoreCommnet)
                            {
                                if (lines[i].Trim().StartsWith("//")) continue;
                            }

                            list.Add(lines[i].Trim());
                        }
                    }

                    return list;
                }
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static IEnumerable<string> ReadControllerPrefixLines(string path, bool ignoreCommnet = true)
    {
        try
        {
            if (File.Exists(path))
            {
                var list = new List<string>();

                using (var reader = new StreamReader(path))
                {
                    var lines = File.ReadAllLines(path);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Contains("[RoutePrefix("))
                        {
                            if (ignoreCommnet)
                            {
                                if (lines[i].Trim().StartsWith("//")) continue;
                            }

                            list.Add(lines[i].Trim());
                        }
                    }

                    return list;
                }
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static IEnumerable<string> ReadObjectLines(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                var list = new List<string>();
                var returnList = new List<string>();

                using (var reader = new StreamReader(path))
                {
                    var lines = File.ReadAllLines(path);

                    //add
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if ((lines[i].Contains("[")) ||
                            (lines[i].Contains("HashSet")) ||
                            (lines[i].Contains("get;")))
                        {
                            list.Add(lines[i].Trim());
                        }
                    }

                    //remove
                    foreach (var ls in list)
                    {
                        if (ls.Contains("System.Diagnostics"))
                            continue;

                        returnList.Add(ls);
                    }

                    return returnList;
                }
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static string GetPluralOf(string value)
    {
        try
        {

            if ((value.EndsWith("s")) || (value.EndsWith("ss")) || (value.EndsWith("sh")) || (value.EndsWith("ch")) || (value.EndsWith("x")) ||
                (value.EndsWith("z")))
            {
                value = string.Concat(value, "es");
            }
            else
            {
                value = string.Concat(value, "s");
            }

            return value;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static IList<string> RemoveDuplicates(IList<string> lines)
    {
        try
        {
            if (lines == null) throw new ArgumentNullException(nameof(lines));

            var returnList = new List<string>();

            foreach (var line in lines)
            {
                var fieldName = line.Trim().Split(' ')[2];

                var firstOrDefault =
                    returnList.FirstOrDefault(e => e.Trim().Split(' ')[2].Equals(fieldName, StringComparison.InvariantCultureIgnoreCase));
                if (firstOrDefault == null)
                    returnList.Add(line);
            }

            return returnList;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static string TrySingularize(string word)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(word)) return string.Empty;

            return new Pluralizer().Singularize(word.Trim());
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public static string TryPluralize(string word)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(word)) return string.Empty;

            return new Pluralizer().Pluralize(word.Trim());
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}