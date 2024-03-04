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
    }
}