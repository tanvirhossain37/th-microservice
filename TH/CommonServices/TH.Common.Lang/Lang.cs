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
    public static class Lang
    {
        private static ResourceManager _rm;
        private static string _resName;

        //Static Constructor
        static Lang()
        {
            //_rm = new ResourceManager("TH.Common.Lang.Languages.StringResource", Assembly.GetExecutingAssembly());
            //_rm = new ResourceManager(".Languages.StringResource", Assembly.GetExecutingAssembly());

            //Assembly asm = Assembly.GetExecutingAssembly();
            Assembly asm = typeof(StringResource).Assembly;
            //string resName = asm.GetName().Name + ".Languages.StringResource";
            string resName = "Languages.StringResource";

            _resName = resName;
            //_rm  = new System.Resources.ResourceManager(resName, asm);
            _rm = new System.Resources.ResourceManager(typeof(StringResource));
        }

        public static string? GetString(string name)
        {
            return $"{_resName}: {_rm.GetString(name)}";
        }

        public static void ChangeLanguage(string language)
        {
            var cultureInfo = new CultureInfo(language);

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

        }
    }
}
