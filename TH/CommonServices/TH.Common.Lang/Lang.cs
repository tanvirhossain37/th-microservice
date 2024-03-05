using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace TH.Common.Lang
{
    public static class Lang
    {
        private static CultureInfo _cultureInfo { get; set; }
        private static readonly Lazy<Dictionary<string, ReadOnlyDictionary<string, string>>> _container;
        private static readonly ResourceManager _resourceManager;

        static Lang()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string resName = asm.GetName().Name + ".Languages.StringResource";
            _resourceManager = new System.Resources.ResourceManager(resName, asm);


            _cultureInfo = new CultureInfo("en-us");
            _container = new Lazy<Dictionary<string, ReadOnlyDictionary<string, string>>>(GetAll);
        }

        public static void SetCultureCode(string cultureCode)
        {
            try
            {
                cultureCode = string.IsNullOrWhiteSpace(cultureCode) ? "en-us" : cultureCode.Trim();

                if (cultureCode.Equals("en-us", StringComparison.InvariantCultureIgnoreCase) ||
                    cultureCode.Equals("bn-bd", StringComparison.InvariantCultureIgnoreCase))
                    _cultureInfo = new CultureInfo(cultureCode);
                else
                    _cultureInfo = new CultureInfo("en-us");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string Find(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name)) return string.Empty;

                name = name.Trim().ToLower();

                var code = _cultureInfo;

                foreach (var container in _container.Value)
                {
                    if (container.Key.Equals(code.Name))
                    {
                        foreach (var dictionary in container.Value)
                        {
                            if (dictionary.Key.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                                return dictionary.Value;
                        }
                    }
                }

                return $"[{name}_{code.Name}]";
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static Dictionary<string, ReadOnlyDictionary<string, string>> GetAll()
        {
            try
            {
                var resourceCollection = new Dictionary<string, ReadOnlyDictionary<string, string>>();

                var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

                // get the assembly
                var assembly = Assembly.GetExecutingAssembly();

                //Find the location of the assembly
                var assemblyLocation =
                    Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(assembly.CodeBase).Path));

                //Find the file anme of the assembly
                var resourceFilename = Path.GetFileNameWithoutExtension(assembly.Location) + ".resources.dll";


                var cultureCodes = cultures.Where(cultureInfo =>
                    (assemblyLocation != null) &&
                    (Directory.Exists(Path.Combine(assemblyLocation, cultureInfo.Name)) ||
                     Directory.Exists(Path.Combine(assemblyLocation, cultureInfo.Name))) &&
                    (File.Exists(Path.Combine(assemblyLocation, cultureInfo.Name, resourceFilename)) ||
                     File.Exists(Path.Combine(assemblyLocation, resourceFilename)))
                ).ToList();

                lock (resourceCollection)
                {
                    foreach (var cultureCode in cultureCodes)
                    {
                        var dictionaryEntries = _resourceManager
                            .GetResourceSet(cultureCode, true, true).OfType<DictionaryEntry>()
                            .ToList();

                        var dictionary = dictionaryEntries.ToDictionary(entry => entry.Key.ToString(),
                            entry => entry.Value.ToString());
                        resourceCollection.Add(cultureCode.Name, new ReadOnlyDictionary<string, string>(dictionary));
                    }
                }

                return resourceCollection;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}