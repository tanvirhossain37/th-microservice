using System.Text.RegularExpressions;

namespace TH.Common.Util
{
    public static class Util
    {
        public static bool TryIsDate(string date)
        {
            bool valid = true;
            //check in 99/99/9999 format
            valid = Regex.IsMatch(date, "^[0-9]{2}/[0-9]{2}/[0-9]{4}$");

            try
            {
                if (valid)
                {
                    DateTime oDate = new DateTime();
                    oDate = Convert.ToDateTime(date);
                }
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        public static string TryClassPropertyNames(object obj)
        {

            try
            {
                if (obj is null) return string.Empty;

                Type t = typeof(object);

                foreach (var prop in t.GetProperties())
                {
                    var name=prop.Name;
                }

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}