using Microsoft.VisualBasic;
using System.Globalization;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using Pluralize.NET.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;

namespace TH.Common.Util
{
    public static class Util
    {
        public static bool TryIsValidDate(DateTime dateTime)
        {
            try
            {
                return dateTime.Year > 1900;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool TryIsDate(string dt)
        {
            // Define the expected date format
            string dateFormat = "yyyy-MM-dd";

            // Try to parse the input string into a DateTime
            if (DateTime.TryParseExact(dt, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool TryIsDateOri(string date)
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

        public static bool TryIsValidEmail(string email)
        {
            try
            {
                email = string.IsNullOrWhiteSpace(email) ? throw new Exception() : email.Trim();
                return string.Equals(new MailAddress(email).Address, email, StringComparison.InvariantCultureIgnoreCase);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string TryClassPropertyNames(object obj)
        {

            try
            {
                if (obj is null) return string.Empty;

                Type t = typeof(object);

                foreach (var prop in t.GetProperties())
                {
                    var name = prop.Name;
                }

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string TryGenerateGuid()
        {
            try
            {
                return Guid.NewGuid().ToString().Trim();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string TryGenerateCode()
        {
            try
            {
                var str = Guid.NewGuid().ToString();
                var array = str.Split("-");
                return array.LastOrDefault().Trim();
            }
            catch (Exception)
            {
                return string.Empty;
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

        public static DateTime? TryCeilTime(DateTime dateTime)
        {
            try
            {
                var date = dateTime.Date;
                var newDate = date.AddDays(1);
                newDate = newDate.AddSeconds(-1);

                return newDate;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime? TryFloorTime(DateTime dateTime)
        {
            try
            {
                return dateTime.Date;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string TryGenerateUserName(string nameOrEmail)
        {
            try
            {
                nameOrEmail = string.IsNullOrWhiteSpace(nameOrEmail) ? string.Empty : nameOrEmail.Trim();

                if (string.IsNullOrWhiteSpace(nameOrEmail)) return string.Empty;

                var isValidEmail = TryIsValidEmail(nameOrEmail);
                var userName = string.Empty;

                if (isValidEmail)
                {
                    var words = nameOrEmail.Split("@")[0];
                    userName = string.Concat(userName, $"{words}");
                }
                else
                {
                    var words = nameOrEmail.Split(" ");
                    bool isFirst = true;
                    foreach (var word in words)
                    {
                        if (isFirst)
                        {
                            userName = string.Concat(userName, $"{word.Replace(".", "")}");
                            isFirst = false;
                        }
                        else
                        {
                            userName = string.Concat(userName, $".{word.Replace(".", "")}");
                        }
                    }
                }

                userName = string.Concat(userName, $".{TryGenerateCode()}");

                return userName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}