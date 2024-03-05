using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using TH.Common.Lang.Languages;

namespace TH.Common.Lang
{
    public static class LangObsolete
    {
        private static ResourceManager _rm;
        private static string _resName;

        //Static Constructor
        static LangObsolete()
        {
            //_rm = new ResourceManager("TH.Common.Lang.Languages.StringResource", Assembly.GetExecutingAssembly());
            //_rm = new ResourceManager(".Languages.StringResource", Assembly.GetExecutingAssembly());

            Assembly asm = Assembly.GetExecutingAssembly();
            string resName = asm.GetName().Name + ".Languages.StringResource";
            _rm = new System.Resources.ResourceManager(resName, asm);

            //var type=typeof(StringResource);
            //var assemblyname = new AssemblyName(type.GetType().Assembly.FullName);
            //_rm = new ResourceManager("Languages.StringResource", type.Assembly);

            // get the assembly
            //var assembly = Assembly.GetExecutingAssembly();

            ////Find the location of the assembly
            //var assemblyLocation =
            //    Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(assembly.CodeBase).Path));

            ////Find the file anme of the assembly
            //var resourceFilename = Path.GetFileNameWithoutExtension(assembly.Location) + ".Languages.StringResource.resources.dll";

            //_rm = new System.Resources.ResourceManager(resourceFilename, assembly);

        }

        public static string? GetString(string name)
        {
            return $"{_rm.GetString(name)?.Trim()}";
        }

        public static void ChangeLanguage(string language)
        {
            var cultureInfo = new CultureInfo(language);

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
        }
    }
}